

namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;


  [RequireComponent(typeof(Renderer))]
  [RequireComponent(typeof(MeshFilter))]
  public abstract class Ship : MonoBehaviour, ITeamable {


    public virtual bool canMove { get; protected set; } = true;
    public virtual Vector2 moveTarget { get; }

    public virtual bool canAttack { get; protected set; } = true;
    public virtual bool attackTarget { get; }

    public virtual TeamType team { get; set; } = TeamType.Unassigned;

    public ShipType shipType = ShipType.Ship;

    protected List<ShipPart> parts;
    public T[] GetShipParts<T>() where T : ShipPart => parts.FindAll(v => v is T) as T[];


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


    public bool AssignPart(ShipPart compartment) {
      if (parts.Contains(compartment)) return false;
      if (compartment.owner) throw new PartOwnershipViolationException($"{nameof(ShipPart)} being assigned is already assigned to another {nameof(Ship)}");
      parts.Add(compartment);
      return true;
    }

    public bool UnassignPart(ShipPart compartment) {
      if (!compartment.owner) return false;
      if (compartment.owner != this) throw new PartOwnershipViolationException($"{nameof(ShipPart)} being unassigned is assigned to another {nameof(Ship)}");
      return parts.Remove(compartment);
    }
  }
}
