


namespace SpaceGame {

  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;

  using Muc.Types.Extensions;
  using System.Linq;

  public abstract class Weapon : Compartment {

    [Tooltip("The Transform that is rotated.")]
    public Transform joint;

    [Tooltip("The point at which the projectile exits. Sight Ray starts from here.")]
    public Transform exitPoint;

    [Tooltip("These GameObjects are ignored by the sight raycast.")]
    public GameObject[] ignoreInCollisionCheck;

    private Quaternion defaultRotation;

    // Start is called before the first frame update
    void Start() {
      defaultRotation = joint.rotation;
    }

    public bool Shoot(GameObject target) {


      var rotation = Quaternion.LookRotation(target.transform.position - joint.position);
      joint.rotation = rotation;


      var hits = Physics.RaycastAll(exitPoint.position.RayTo(target.transform.position));

      if (hits.Length == 0) return false;

      foreach (var hit in hits) {
        var hitGo = hit.collider.gameObject;
        var comp = hitGo.GetComponent<Compartment>();
        if (hitGo == target || (comp != null && comp.owner.gameObject == target)) break;
        else if (!ignoreInCollisionCheck.Contains(hitGo)) return false;
      }

      // Send shot

      return true;
    }
  }
}
