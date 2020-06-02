


namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class SelectionControls : MonoBehaviour {

    void Start() {
      // Select new primary
      MouseInputHandler.AddMouseInputType(new MouseInputType(
        MouseInputSpecifier.Static,
        predicate: (go) => true,
        onValid: Selection.Set)
      );

      // Add to selection
      MouseInputHandler.AddMouseInputType(new MouseInputType(
        MouseInputSpecifier.Static | MouseInputSpecifier.Shift,
        predicate: (go) => true,
        onValid: Selection.Add)
      );

      // Remove from selection
      MouseInputHandler.AddMouseInputType(new MouseInputType(
        MouseInputSpecifier.Static | MouseInputSpecifier.Control,
        predicate: (go) => true,
        onValid: Selection.Remove)
      );
    }

  }
}