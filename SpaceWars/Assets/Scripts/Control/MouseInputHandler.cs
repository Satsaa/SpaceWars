


namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using UnityEngine;

  using Muc.Collections;
  using Muc.Types.Extensions;


  public static class MouseInputHandler {

    /* 
    
    Non-static MouseInputTypes are active before statics

    Priority MouseInputTypes are active before non-prioritys

    Modifier keys filter the currently active MouseInputTypes
    
    */


    public static InputComponent inputComponent;

    private static LayerMask defaultMask => inputComponent.defaultMask;
    private static Transform transform => inputComponent.transform;

    private static OrderedList<MouseInputType> mits;

    // Call from component
    public static void Update() {

    }

    public static void AddMouseInputType(MouseInputType mit) {

    }

    private static void PromoteIfCompartment(ref GameObject target) {
      var cment = target.GetComponent<Compartment>();
      if (cment) {
        if (!cment.owner) throw new CompartmentPromotionException($"Cannot promote a {nameof(Compartment)} with a null {nameof(Compartment.owner)}");
        target = cment.owner.gameObject;
      }
    }

    private static void TryPromote(ref GameObject target) {
      var cment = target.GetComponent<Compartment>();
      if (cment && cment.owner)
        target = cment.owner.gameObject;
    }

  }
}