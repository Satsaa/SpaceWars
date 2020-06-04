


namespace SpaceGame {

  using System;
  using System.Linq;
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  using MouseInput;
  using Muc.Types.Extensions;

  [RequireComponent(typeof(Selection))]
  [RequireComponent(typeof(MouseActionHandler))]
  public class DragTest : MonoBehaviour {


    void Start() {

      var selection = GetComponent<Selection>();
      var handler = GetComponent<MouseActionHandler>();

      Vector2 prev = Vector2.zero;

      // Drag selected targets
      handler.AddMouseHotkey(
        new DragAction(
          specifiers: HotkeySpecifier.Persistent | HotkeySpecifier.Alt,
          predicate: (go) => selection.Contains(go),
          noPromote: false,
          start: (x, vec) => {
            print("Start");
            prev = vec;
          },
          drag: (x, v) => {
            print("Drag");
            var dif = v.xy() - prev;
            foreach (var item in selection)
              item.transform.Translate(dif);
            prev = v;
          },
          end: (x, vec) => {
            print("End");
          }
        )
      );

    }
  }
}