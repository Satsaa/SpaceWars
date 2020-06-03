


namespace SpaceGame.MouseInput {

  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using UnityEngine;

  using Muc.Collections;
  using Muc.Types.Extensions;
  using System.Linq;

  [RequireComponent(typeof(Camera))]
  public class MouseHotkeyHandler : MonoBehaviour {

    [Tooltip("LayerMask used with RayCast when clicking the primary mouse button")]
    public LayerMask defaultMask;

    [Tooltip("Minimum pixel distance dragged before registering drag. Prevents accidental drags.")]
    public float minDragDist = 2;

    [Tooltip("The primary button. Mostly used for selecting targets")]
    public KeyCode primaryKey = KeyCode.Mouse0;
    [Tooltip("The secondary button. Mostly used for move targeting")]
    public KeyCode secondaryKey = KeyCode.Mouse1;

    public KeyCode controlKey = KeyCode.LeftControl;
    public KeyCode altKey = KeyCode.LeftAlt;
    public KeyCode shiftKey = KeyCode.LeftShift;


    public IReadOnlyList<Hotkey> hotkeys => _hotkeys;
    private OrderedList<Hotkey> _hotkeys = new OrderedList<Hotkey>(MouseHotkeyComparison);

    private DragHotkey dragHotkey = null;
    private GameObject dragTarget;
    private bool dragIniting;
    private float dragStartDist;
    private Vector2 dragInitScreenPos;


    private new Camera camera;


    public void Awake() {
      camera = GetComponent<Camera>();
    }

    // Call from component
    public void Update() {
      if (HandleDrag()) return;
      HandleHotkeys(GetActive());
    }

    private bool HandleDrag() {
      if (dragHotkey is null) return false;

      if (dragIniting) {

        // Cancel if releases before starting drag
        if (!Input.GetKey(dragHotkey.specifiers.HasFlag(HotkeySpecifier.Secondary) ? secondaryKey : primaryKey)) {
          dragHotkey = null;
          return false;
        }

        // Check if enough movement
        var dragDist = Vector2.Distance(dragInitScreenPos, Input.mousePosition);
        if (dragDist < minDragDist) return true;

        dragIniting = false;
        dragHotkey.start(dragTarget, GetDragPosition());
      }

      // End if released
      if (!Input.GetKey(dragHotkey.specifiers.HasFlag(HotkeySpecifier.Secondary) ? secondaryKey : primaryKey)) {
        dragHotkey.end(dragTarget, GetDragPosition());
        if (dragHotkey.specifiers.HasFlag(HotkeySpecifier.Static)) _hotkeys.Remove(dragHotkey);
        dragHotkey = null;
        return false;
      }

      dragHotkey.drag(dragTarget, GetDragPosition());


      return true;

      Vector3 GetDragPosition() => camera.transform.position + camera.ScreenPointToRay(Input.mousePosition).direction * dragStartDist;
    }


    public void HandleHotkeys(IEnumerable<Hotkey> hotkeys) {

      GameObject target = null;
      GameObject promoTarget = null;
      if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit)) {
        target = hit.collider.gameObject;
        promoTarget = TryPromote(target);
      }

      var primary = Input.GetKeyDown(primaryKey);
      var secondary = Input.GetKeyDown(secondaryKey);

      GameObject highLightTarget = null;

      var enumerator = hotkeys.GetEnumerator();
      foreach (var hotkey in enumerator.Enumerate()) {

        var finalTarget = (hotkey.promote && promoTarget) ? promoTarget : target;

        if (
          (primary && !hotkey.specifiers.HasFlag(HotkeySpecifier.Secondary)) ||
          (secondary && hotkey.specifiers.HasFlag(HotkeySpecifier.Secondary))
        ) {

          if (hotkey.predicate(finalTarget)) {

            // !!! We need to check for overlaps and apply the specifiers
            // !!! We could even split the hotkey types in to their collections
            // !!! like that we dont need type checks and we don't need to inherit from Hotkey

            if (hotkey is DragHotkey dragHotkey) {

              var overlapping = enumerator.Enumerate().Any(h => !(h is DragHotkey) && h.predicate(finalTarget));

              dragIniting = true;
              dragInitScreenPos = Input.mousePosition;
              dragStartDist = Vector3.Distance(camera.transform.position, finalTarget.transform.position);
              dragTarget = finalTarget;

            } else {
              
              var overlapping = enumerator.Enumerate().Any(h => h is AtypicalHotkey && h.predicate(finalTarget));

              hotkey.action(finalTarget);
              if (!hotkey.specifiers.HasFlag(HotkeySpecifier.Static)) _hotkeys.Remove(hotkey);

            }

            highLightTarget = null;
            break;
          }

        } else if (!highLightTarget) {
          if (hotkey.predicate(finalTarget))
            highLightTarget = finalTarget; // The first valid target is highlighted
        }

      }

      if (highLightTarget) {
        HighlightTarget(target);
      }
    }

    public void HighlightTarget(GameObject target) {

    }

    public static int MouseHotkeyComparison(Hotkey mitA, Hotkey mitB) {
      return mitA.priorityPoints - mitB.priorityPoints;
    }

    public IEnumerable<Hotkey> GetActive() {

      var control = Input.GetKey(controlKey);
      var alt = Input.GetKey(altKey);
      var shift = Input.GetKey(shiftKey);

      // Gets highest priority MITS and filter for modifiers
      var prevPoints = int.MinValue;
      for (int i = _hotkeys.Count - 1; i >= 0; i--) {
        var mit = _hotkeys[i];
        if (mit.priorityPoints < prevPoints) break;

        prevPoints = mit.priorityPoints;

        var spec = mit.specifiers;

        if (!spec.HasFlag(HotkeySpecifier.AllowControl) && control != spec.HasFlag(HotkeySpecifier.Control)) continue;
        if (!spec.HasFlag(HotkeySpecifier.AllowAlt) && alt != spec.HasFlag(HotkeySpecifier.Alt)) continue;
        if (!spec.HasFlag(HotkeySpecifier.AllowShift) && shift != spec.HasFlag(HotkeySpecifier.Shift)) continue;

        yield return _hotkeys[i];
      }

    }

    public void AddMouseHotkey(Hotkey mit) {
      _hotkeys.Add(mit);
    }

    private GameObject TryPromote(GameObject target) {
      var cment = target.GetComponent<IPart>();
      if (cment != null && cment.owner)
        return cment.owner.gameObject;
      return null;
    }

  }
}