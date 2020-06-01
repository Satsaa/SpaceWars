


namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;
  using UnityEngine.Events;

  using Muc.Types.Extensions;
  using System.Linq;
  using System.Threading.Tasks;

  [ExecuteInEditMode]
  public class InputComponent : MonoBehaviour {

    [Tooltip("LayerMask used with RayCast when clicking the primary mouse button")]
    public LayerMask defaultMask;

    [Tooltip("Minimum pixel distance dragged before registering drag. Prevents accidental drags.")]
    public float dragMinDist = 2;

    [Tooltip("The primary button. Mostly used for selecting targets")]
    public KeyCode primary = KeyCode.Mouse0;
    [Tooltip("The secondary button. Mostly used for move targeting")]
    public KeyCode secondary = KeyCode.Mouse1;

    public KeyCode Control = KeyCode.LeftControl;
    public KeyCode Alt = KeyCode.LeftAlt;
    public KeyCode Shift = KeyCode.LeftShift;


    void Reset() {
      if (!GetComponent<Camera>()) {
        Debug.LogWarning($"{nameof(InputComponent)} is intended to be on a GameObject with a Camera Component");
      }
    }

    void Start() {
      if (GameObject.FindObjectsOfType<InputComponent>().Length > 1) {
        Destroy(gameObject);
        Debug.LogWarning($"Newly created {nameof(InputComponent)} destroyed because there is already another one");
        return;
      }
      // MouseInputHandler.inputComponent = this;
    }

    void Update() => MouseInputHandler.Update();
  }
}