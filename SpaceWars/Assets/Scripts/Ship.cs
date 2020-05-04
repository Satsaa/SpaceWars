

namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;


  public class Ship : MonoBehaviour {

    protected Vector2 moveTarget;

    protected List<Compartment> compartments;

    public T[] GetCompartments<T>() where T : Compartment => compartments.FindAll(v => v is T) as T[];

    public bool AssignCompartment(Compartment compartment) {
      if (compartments.Contains(compartment)) return false;
      if (compartment.owner) throw new CompartmentOwnershipViolationException("Compartment being assigned is already owned by another Ship");
      compartments.Add(compartment);
      return true;
    }

    public bool UnassignCompartment(Compartment compartment) {
      if (!compartment.owner) return false;
      if (compartment.owner != this) throw new CompartmentOwnershipViolationException("Compartment being unassigned is owned by another Ship");
      return compartments.Remove(compartment);
    }


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
  }

}
