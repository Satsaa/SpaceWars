


namespace SpaceGame {

  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;

  using Muc.Types.Extensions;
  using System.Linq;
  using System.Threading.Tasks;

  public partial class ClickHandler : MonoBehaviour {


    [Tooltip("LayerMask used with RayCast when clicking the primary mouse button")]
    public LayerMask mask;

    private TaskCompletionSource<GameObject> task;
    private TargetType targetType;

    void Reset() {
      if (!GetComponent<Camera>()) {
        Debug.LogWarning($"{nameof(ClickHandler)} is intended to be on a Camera GameObject");
      }
    }

    void OnDestroy() {
      if (task != null) task.SetResult(null);
    }

    void Update() {
      if (task != null && !task.Task.IsCompleted) {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
          if (Physics.Raycast(transform.position.RayTo(transform.forward), out var hit)) {

            var hitGo = hit.collider.gameObject;


          }

          task.SetResult(null);
        }
      }
    }

    public bool ValidateType() {
      // Test all types


      return false;
    }

    public void RenderTarget() {
      // Render highlights of valid targets
    }

    public async Task<GameObject> GetTarget(TargetType targetType) {
      this.targetType = targetType;
      if (task != null) task.SetResult(null);
      task = new TaskCompletionSource<GameObject>();
      return await task.Task;
    }
  }
}