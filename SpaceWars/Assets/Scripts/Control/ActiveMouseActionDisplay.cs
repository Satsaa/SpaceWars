

#if UNITY_EDITOR
namespace SpaceGame.MouseInput {

  using UnityEngine;
  using UnityEditor;
  using System.Collections.Generic;

  [CustomEditor(typeof(ActiveMouseActionDisplay))]
  public class ActiveMouseActionDisplayEditor : Editor {

    private HotkeySpecifier mis;

    public override void OnInspectorGUI() {
      serializedObject.Update();

      var target = this.target as ActiveMouseActionDisplay;
      var handler = target.handler;

      mis = (HotkeySpecifier)EditorGUILayout.EnumFlagsField(mis);



      if (GUILayout.Button("Create new"))
        target.Add(new ClickAction(mis, (x) => true, (x) => { }));


      if (handler) {

        var actives = new List<MouseAction>(handler.WhereActive());

        foreach (var mit in handler.actions) {
          using (new EditorGUI.DisabledScope(!actives.Contains(mit))) {
            EditorGUILayout.LabelField(mit.specifiers.ToString());
          }
        }

      }

      serializedObject.ApplyModifiedProperties();
    }
  }
}
#endif


namespace SpaceGame.MouseInput {

  using UnityEngine;

  [RequireComponent(typeof(MouseActionHandler))]
  public class ActiveMouseActionDisplay : MonoBehaviour {

    public MouseActionHandler handler;

    void Awake() {
      handler = GetComponent<MouseActionHandler>();
    }

    public void Add(ClickAction mit) {
      handler.AddMouseHotkey(mit);
    }
  }
}