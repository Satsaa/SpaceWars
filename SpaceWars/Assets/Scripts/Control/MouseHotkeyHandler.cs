


namespace SpaceGame.MouseInput {

  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using UnityEngine;

  using Muc.Collections;
  using Muc.Types.Extensions;
  using MIT = Hotkey;


  public partial class MouseHotkeyHandler : MonoBehaviour {

    [Tooltip("LayerMask used with RayCast when clicking the primary mouse button")]
    public LayerMask defaultMask;

    [Tooltip("Minimum pixel distance dragged before registering drag. Prevents accidental drags.")]
    public float dragMinDist = 2;

    [Tooltip("The primary button. Mostly used for selecting targets")]
    public KeyCode primary = KeyCode.Mouse0;
    [Tooltip("The secondary button. Mostly used for move targeting")]
    public KeyCode secondary = KeyCode.Mouse1;

    public KeyCode Control = KeyCode.LeftControl;
    public KeyCode Alt = KeyCode.LeftAlt;
    public KeyCode Shift = KeyCode.LeftShift;


    public IReadOnlyList<MIT> MouseHotkeys => mits;
    private OrderedList<MIT> mits = new OrderedList<MIT>(MouseHotkeyComparison);


    void Reset() {
      if (!GetComponent<Camera>()) {
        Debug.LogWarning($"{nameof(MouseHotkeyHandler)} is intended to be on a Camera GameObject");
      }
    }

    void Start() {
      if (GameObject.FindObjectsOfType<MouseHotkeyHandler>().Length > 1) {
        Destroy(gameObject);
        Debug.LogWarning($"Newly created {nameof(MouseHotkeyHandler)} destroyed because there is already another one");
        return;
      }
    }

    // Call from component
    public void Update() {
      HandleMITs(GetActive());
    }

    public void HandleMITs(IEnumerable<MIT> actives) {

      GameObject target = null;
      GameObject promoTarget = null;
      if (Physics.Raycast(transform.position.RayTo(transform.forward), out var hit)) {
        target = hit.collider.gameObject;
        promoTarget = TryPromote(target);
      }

      var primary = Input.GetKeyDown(KeyCode.Mouse0);
      var secondary = Input.GetKeyDown(KeyCode.Mouse1);

      GameObject highLightTarget = null;

      foreach (var active in actives) {

        var finalTarget = (active.promote && promoTarget) ? promoTarget : target;

        if (
          (primary && !active.specifiers.HasFlag(HotkeySpecifier.Secondary)) ||
          (secondary && active.specifiers.HasFlag(HotkeySpecifier.Secondary))
        ) {

          if (active.predicate(finalTarget)) {
            highLightTarget = null;
            active.action(finalTarget);
            break;
          }

        } else if (!highLightTarget) {
          if (active.predicate(finalTarget))
            highLightTarget = finalTarget; // The first valid target is highlighted
        }

      }

      if (highLightTarget) {
        HighlightTarget(target);
      }
    }

    public void HighlightTarget(GameObject target) {

    }

    public static int MouseHotkeyComparison(MIT mitA, MIT mitB) {
      return mitA.priorityPoints - mitB.priorityPoints;
    }

    public IEnumerable<MIT> GetActive() {

      var control = Input.GetKey(Control);
      var alt = Input.GetKey(Alt);
      var shift = Input.GetKey(Shift);

      // Gets highest priority MITS and filter for modifiers
      var prevPoints = int.MinValue;
      for (int i = mits.Count - 1; i >= 0; i--) {
        var mit = mits[i];
        if (mit.priorityPoints < prevPoints) break;

        prevPoints = mit.priorityPoints;

        var spec = mit.specifiers;

        if (!spec.HasFlag(HotkeySpecifier.AllowControl) && control != spec.HasFlag(HotkeySpecifier.Control)) continue;
        if (!spec.HasFlag(HotkeySpecifier.AllowAlt) && alt != spec.HasFlag(HotkeySpecifier.Alt)) continue;
        if (!spec.HasFlag(HotkeySpecifier.AllowShift) && shift != spec.HasFlag(HotkeySpecifier.Shift)) continue;

        yield return mits[i];
      }

    }

    public void AddMouseHotkey(MIT mit) {
      mits.Add(mit);
    }

    private GameObject TryPromote(GameObject target) {
      var cment = target.GetComponent<IPart>();
      if (cment != null && cment.owner)
        return cment.owner.gameObject;
      return null;
    }

  }
}