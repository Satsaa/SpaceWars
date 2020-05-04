

namespace SpaceGame {

  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;


  public class Compartment : MonoBehaviour {

    private Ship _owner;
    public Ship owner {
      get => _owner;
      set {
        if (_owner == value) return;

        // Remove stale
        if (_owner) _owner.UnassignCompartment(this);

        // Populate new
        if (value) value.AssignCompartment(this);

        _owner = value;
      }
    }


  }

}
