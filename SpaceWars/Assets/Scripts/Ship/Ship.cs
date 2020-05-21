

namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;


  [RequireComponent(typeof(Renderer))]
  [RequireComponent(typeof(MeshFilter))]
  public abstract class Ship : MonoBehaviour {


    public virtual bool canMove { get; protected set; } = true;
    public virtual Vector2 moveTarget { get; }

    public virtual bool canAttack { get; protected set; } = true;
    public virtual bool attackTarget { get; }

    public OwnerType ownerType => (OwnerType)shipType;
    public ShipType shipType = ShipType.Ship | ShipType.Neutral;

    protected List<Compartment> compartments;
    public T[] GetCompartments<T>() where T : Compartment => compartments.FindAll(v => v is T) as T[];


    // Start is called before the first frame update
    protected virtual void Start() {
      GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    protected virtual void Update() {

    }


    protected virtual void GenerateShip() {
      var mesh = GetComponent<MeshFilter>().mesh;
    }


    public bool AssignCompartment(Compartment compartment) {
      if (compartments.Contains(compartment)) return false;
      if (compartment.owner) throw new CompartmentOwnershipViolationException($"{nameof(Compartment)} being assigned is already assigned to another {nameof(Ship)}");
      compartments.Add(compartment);
      return true;
    }

    public bool UnassignCompartment(Compartment compartment) {
      if (!compartment.owner) return false;
      if (compartment.owner != this) throw new CompartmentOwnershipViolationException($"{nameof(Compartment)} being unassigned is assigned to another {nameof(Ship)}");
      return compartments.Remove(compartment);
    }
  }
}
