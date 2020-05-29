


namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;
  using UnityEngine.Events;

  using Muc.Types.Extensions;
  using System.Linq;
  using System.Threading.Tasks;

  public class MouseHandler : MonoBehaviour {


    [Tooltip("LayerMask used with RayCast when clicking the primary mouse button")]
    public LayerMask mask;

    private TaskCompletionSource<GameObject> getTargetTask;
    private IValidator<GameObject> validator;
    private bool promoteCompartment;

    void Reset() {
      if (!GetComponent<Camera>()) {
        Debug.LogWarning($"{nameof(MouseHandler)} is intended to be on a Camera GameObject");
      }
    }

    void OnDestroy() {
      if (getTargetTask != null) getTargetTask.SetResult(null);
    }

    void Update() {

      if (getTargetTask != null && !getTargetTask.Task.IsCompleted) {
        TestForTarget();
      }
    }

    void TestForTarget() {
      if (Physics.Raycast(transform.position.RayTo(transform.forward), out var hit)) {
        // Pointing at something

        var target = hit.collider.gameObject;

        if (promoteCompartment) {
          var cment = target.GetComponent<Compartment>();
          if (cment) target = cment.owner ? cment.owner.gameObject : null;
        }

        var valid = target ? false : validator.Validate(gameObject);

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
          if (!target) getTargetTask.SetResult(null);

          if (valid) {
            // Valid target
            getTargetTask.SetResult(target);

          } else {
            // Invalid target
            getTargetTask.SetResult(null);
          }
        } else if (valid) {
          // Highlight?
        }

      } else {
        // Nothing
      }
    }

    public void RenderTarget() {
      // Rendering of highlights or something like that
    }

    public async Task<GameObject> GetTarget(IValidator<GameObject> validator, bool promoteCompartment = false) {
      this.validator = validator;
      this.promoteCompartment = promoteCompartment;
      if (getTargetTask != null) getTargetTask.SetResult(null);
      getTargetTask = new TaskCompletionSource<GameObject>();
      return await getTargetTask.Task;
    }
  }
}