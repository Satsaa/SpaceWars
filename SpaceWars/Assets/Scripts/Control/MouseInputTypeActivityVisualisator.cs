
namespace SpaceGame {

  //C# Example (LookAtPointEditor.cs)
  using UnityEngine;
  using UnityEditor;
  using System.Collections.Generic;

  [CustomEditor(typeof(MouseInputTypeActivityVisualisator))]
  [CanEditMultipleObjects]
  public class LookAtPointEditor : Editor {

    private MouseInputSpecifier mis;

    public override void OnInspectorGUI() {


      var target = this.target as MouseInputTypeActivityVisualisator;

      mis = (MouseInputSpecifier)EditorGUILayout.EnumFlagsField(mis);


      if (MouseInputHandler.inputComponent) {

        if (GUILayout.Button("Create new"))
          target.Add(new MouseInputType(mis, (x) => true, (x) => { }));


        var actives = new List<MouseInputType>(MouseInputHandler.GetActive());

        serializedObject.Update();
        foreach (var mit in MouseInputHandler.mits) {
          using (new EditorGUI.DisabledScope(!actives.Contains(mit))) {
            EditorGUILayout.LabelField(mit.specifiers.ToString());
          }
        }

      } else {
        GUILayout.Label($"{nameof(MouseInputType)}s will be shown here in play mode");
      }

      serializedObject.ApplyModifiedProperties();
    }
  }
}

namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;
  using UnityEngine.Events;

  using Muc.Types.Extensions;
  using System.Linq;
  using System.Threading.Tasks;

  public class MouseInputTypeActivityVisualisator : MonoBehaviour {


    void Start() {

    }

    public void Add(MouseInputType mit) {
      MouseInputHandler.AddMouseInputType(mit);
    }
  }
}