


namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using UnityEngine;

  using Muc.Collections;
  using Muc.Types.Extensions;
  using MIT = MouseInputType;


  public static partial class MouseInputHandler {

    /* 
    
    Non-static MouseInputTypes are active before statics

    Priority MouseInputTypes are active before non-prioritys

    Modifier keys filter the currently active MouseInputTypes
    
    */


    public static InputComponent inputComponent;

    private static LayerMask defaultMask => inputComponent.defaultMask;
    private static Transform transform => inputComponent.transform;

    private static Comparison<MIT> comparison = (x, y) => MouseInputTypeComparison(x, y);
    public static OrderedList<MIT> mits = new OrderedList<MIT>(comparison);


    // Call from component
    public static void Update() {
      HandleMITs();
    }

    public static void HandleMITs() {

      var actives = GetActive();

      GameObject target = null;
      GameObject promoTarget = null;
      if (Physics.Raycast(transform.position.RayTo(transform.forward), out var hit)) {
        target = hit.collider.gameObject;
        promoTarget = TryPromote(target);
      }

      var primary = Input.GetKey(KeyCode.Mouse0);
      var secondary = Input.GetKey(KeyCode.Mouse1);

      GameObject highLightTarget = null;

      foreach (var active in actives) {


        var finalTarget = active.promoteCompartments && promoTarget ? promoTarget : target;

        if (
          (primary && !active.specifiers.HasFlag(MouseInputSpecifier.Secondary)) ||
          (secondary && active.specifiers.HasFlag(MouseInputSpecifier.Secondary))
        ) {

          if (active.predicate(finalTarget)) {
            highLightTarget = null;
            active.onValid(finalTarget);
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

    public static void HighlightTarget(GameObject target) {

    }

    public static IEnumerable<MIT> GetActive() {

      var control = Input.GetKey(inputComponent.Control);
      var alt = Input.GetKey(inputComponent.Alt);
      var shift = Input.GetKey(inputComponent.Shift);

      // Gets highest priority MITS and filter for modifiers
      var prevPoints = int.MinValue;
      for (int i = mits.Count - 1; i >= 0; i--) {
        var mit = mits[i];
        if (mit.priorityPoints < prevPoints) break;

        prevPoints = mit.priorityPoints;

        var spec = mit.specifiers;

        if (!spec.HasFlag(MouseInputSpecifier.AllowControl) && control != spec.HasFlag(MouseInputSpecifier.Control)) continue;
        if (!spec.HasFlag(MouseInputSpecifier.AllowAlt) && alt != spec.HasFlag(MouseInputSpecifier.Alt)) continue;
        if (!spec.HasFlag(MouseInputSpecifier.AllowShift) && shift != spec.HasFlag(MouseInputSpecifier.Shift)) continue;

        yield return mits[i];
      }

    }

    public static void AddMouseInputType(MIT mit) {
      mits.Add(mit);
    }

    public static int MouseInputTypeComparison(MIT mitA, MIT mitB) {
      return mitA.priorityPoints - mitB.priorityPoints;
    }

    private static void PromoteIfCompartment(ref GameObject target) {
      var cment = target.GetComponent<Compartment>();
      if (cment) {
        if (!cment.owner) throw new CompartmentPromotionException($"Cannot promote a {nameof(Compartment)} with a null {nameof(Compartment.owner)}");
        target = cment.owner.gameObject;
      }
    }

    private static GameObject TryPromote(GameObject target) {
      var cment = target.GetComponent<Compartment>();
      if (cment && cment.owner)
        return cment.owner.gameObject;
      return null;
    }

  }
}