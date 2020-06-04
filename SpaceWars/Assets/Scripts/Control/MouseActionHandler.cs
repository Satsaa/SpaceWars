


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
  public class MouseActionHandler : MonoBehaviour {

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


    public IReadOnlyList<MouseAction> actions => _actions;
    private OrderedList<MouseAction> _actions = new OrderedList<MouseAction>((a, b) => a.priority - b.priority);

    private DragAction dragHotkey = null;
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
      HandleActions(WhereActive(_actions));
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
        if (dragHotkey.specifiers.HasFlag(HotkeySpecifier.Persistent)) _actions.Remove(dragHotkey);
        dragHotkey = null;
        return false;
      }

      dragHotkey.drag(dragTarget, GetDragPosition());


      return true;

      Vector3 GetDragPosition() => camera.transform.position + camera.ScreenPointToRay(Input.mousePosition).direction * dragStartDist;
    }


    public void HandleActions(IEnumerable<MouseAction> actions) {

      // Find target
      GameObject target = null;
      GameObject promoTarget = null;
      if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit)) {
        target = hit.collider.gameObject;
        promoTarget = TryPromote(target);
      }

      // Cache keys
      var primary = Input.GetKeyDown(primaryKey);
      var secondary = Input.GetKeyDown(secondaryKey);


      var enumerator = actions.GetEnumerator();
      foreach (var action in enumerator.Enumerate()) {

        var finalTarget = (action.promote && promoTarget) ? promoTarget : target;

        if (
          (primary && !action.specifiers.HasFlag(HotkeySpecifier.Secondary)) ||
          (secondary && action.specifiers.HasFlag(HotkeySpecifier.Secondary))
        ) {

          if (action.validate(finalTarget)) {

            // Handle different MouseActions
            switch (action) {

              case DragAction dragAction:
                dragIniting = true;
                dragInitScreenPos = Input.mousePosition;
                dragStartDist = Vector3.Distance(camera.transform.position, finalTarget.transform.position);
                dragTarget = finalTarget;
                break;

              case ClickAction clickAction:
                clickAction.action(finalTarget);
                if (!clickAction.specifiers.HasFlag(HotkeySpecifier.Persistent)) _actions.Remove(clickAction);
                break;
            }

            break;
          }

        }

      }

    }

    public IEnumerable<MouseAction> WhereActive() => WhereActive(_actions);
    public IEnumerable<T> WhereActive<T>(IList<T> actions) where T : MouseAction {

      var control = Input.GetKey(controlKey);
      var alt = Input.GetKey(altKey);
      var shift = Input.GetKey(shiftKey);

      // Gets highest priority MITS and filter for modifiers
      var prevPoints = int.MinValue;
      for (int i = actions.Count - 1; i >= 0; i--) {
        var mit = actions[i];
        if (mit.priority < prevPoints) break;

        prevPoints = mit.priority;

        var spec = mit.specifiers;

        if (!spec.HasFlag(HotkeySpecifier.AllowControl) && control != spec.HasFlag(HotkeySpecifier.Control)) continue;
        if (!spec.HasFlag(HotkeySpecifier.AllowAlt) && alt != spec.HasFlag(HotkeySpecifier.Alt)) continue;
        if (!spec.HasFlag(HotkeySpecifier.AllowShift) && shift != spec.HasFlag(HotkeySpecifier.Shift)) continue;

        yield return actions[i];
      }

    }

    public void AddMouseHotkey(MouseAction mit) {
      _actions.Add(mit);
    }

    private GameObject TryPromote(GameObject target) {
      var cment = target.GetComponent<IPart>();
      if (cment != null && cment.owner)
        return cment.owner.gameObject;
      return null;
    }

  }
}