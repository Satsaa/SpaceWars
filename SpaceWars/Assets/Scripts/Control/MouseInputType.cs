


namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;

  using Muc.Types.Extensions;
  using System.Threading.Tasks;

  public class MouseInputType {

    /// <summary> Return true if the target is valid </summary>
    public Predicate<GameObject> predicate;

    /// <summary> Will be called automatically if the user activates a valid target </summary>
    public Action<GameObject> onValid;

    /// <summary> Defines the activation circumstances of this MouseInputType </summary>
    public readonly MouseInputSpecifier specifiers;

    /// <summary> Integer value which represents the priority of this kind of MouseInputType </summary>
    public readonly int priorityPoints;

    /// <summary> When calling the validator, change Compartments to Ships (if possible) </summary>
    public bool promoteCompartments;

    public MouseInputType(MouseInputSpecifier inputSpecifier, Predicate<GameObject> predicate, Action<GameObject> onValid)
      : this(inputSpecifier, predicate, false, onValid) { }

    public MouseInputType(MouseInputSpecifier inputSpecifier, Predicate<GameObject> predicate, bool promoteCompartment, Action<GameObject> onValid) {
      this.specifiers = inputSpecifier;
      this.predicate = predicate;
      this.onValid = onValid;
      this.promoteCompartments = promoteCompartment;

      this.priorityPoints = 0;
      if (inputSpecifier.HasFlag(MouseInputSpecifier.Static)) this.priorityPoints -= 0b_0001;
      if (inputSpecifier.HasFlag(MouseInputSpecifier.Priority)) this.priorityPoints += 0b_0010;
    }
  }

}