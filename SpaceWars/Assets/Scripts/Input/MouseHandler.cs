


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

    private TaskCompletionSource<GameObject> task;

    private TargetFilter targetFilter;

    void Reset() {
      if (!GetComponent<Camera>()) {
        Debug.LogWarning($"{nameof(MouseHandler)} is intended to be on a Camera GameObject");
      }
    }

    void OnDestroy() {
      if (task != null) task.SetResult(null);
    }

    void Update() {

      if (task != null && !task.Task.IsCompleted) {


        if (Physics.Raycast(transform.position.RayTo(transform.forward), out var hit)) {
          // Pointing at something

          var target = hit.collider.gameObject;

          if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (!target) task.SetResult(null);

            targetFilter.Validate(target, out var overrideTarget);



            task.SetResult(null);
          }

        } else {
          // Mouse not pointing to anything

        }
      }
    }

    public bool ValidateType() {
      // Test all types


      return false;
    }

    public void RenderTarget() {
      // Rendering of highlights or something like that
    }

    public async Task<GameObject> GetTarget(TargetFilter targetType) {
      this.targetFilter = targetType;
      if (task != null) task.SetResult(null);
      task = new TaskCompletionSource<GameObject>();
      return await task.Task;
    }
  }
}