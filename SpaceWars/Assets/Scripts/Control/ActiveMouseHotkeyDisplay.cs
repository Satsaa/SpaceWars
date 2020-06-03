

#if UNITY_EDITOR
namespace SpaceGame.MouseInput {

  using UnityEngine;
  using UnityEditor;
  using System.Collections.Generic;

  [CustomEditor(typeof(ActiveMouseHotkeyDisplay))]
  public class ActiveMouseHotkeyDisplayEditor : Editor {

    private HotkeySpecifier mis;

    public override void OnInspectorGUI() {
      serializedObject.Update();

      var target = this.target as ActiveMouseHotkeyDisplay;
      var handler = target.handler;

      mis = (HotkeySpecifier)EditorGUILayout.EnumFlagsField(mis);



      if (GUILayout.Button("Create new"))
        target.Add(new Hotkey(mis, (x) => true, (x) => { }));


      if (handler) {

        var actives = new List<Hotkey>(handler.GetActive());

        foreach (var mit in handler.hotkeys) {
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

  [RequireComponent(typeof(MouseHotkeyHandler))]
  public class ActiveMouseHotkeyDisplay : MonoBehaviour {

    public MouseHotkeyHandler handler;

    void Awake() {
      handler = GetComponent<MouseHotkeyHandler>();
    }

    public void Add(Hotkey mit) {
      handler.AddMouseHotkey(mit);
    }
  }
}