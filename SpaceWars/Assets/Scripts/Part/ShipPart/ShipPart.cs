

namespace SpaceGame {

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;


  public abstract class ShipPart : MonoBehaviour, IPart {

    MonoBehaviour IPart.owner => owner;
    public Ship owner {
      get => _owner;
      set {
        if (_owner == value) return;

        // Remove stale
        if (_owner) _owner.UnassignPart(this);

        // Populate new
        if (value) value.AssignPart(this);

        _owner = value;
      }
    }
    private Ship _owner;



    /// <summary> Returns component or the owner Ship if component is a Compartment </summary>
    public static Component GetOwner(Component component) {
      if (component is ShipPart comp)
        if (comp.owner)
          return comp.owner;
      return component;
    }


  }

}
