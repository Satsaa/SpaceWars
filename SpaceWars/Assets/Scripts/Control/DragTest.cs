


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

      Plane plane = default(Plane);
      Vector3 prev = Vector3.zero;

      // Drag selected targets
      handler.AddMouseHotkey(
        new DragAction(
          specifiers: HotkeySpecifier.Persistent | HotkeySpecifier.Alt,
          predicate: (go) => selection.Contains(go),
          noPromote: false,

          start: (x, vec) => {
            plane = new Plane(Vector3.up, x.transform.position);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float enter))
              prev = ray.origin + ray.direction * enter;
          },

          drag: (x, v) => {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var current = Vector3.zero;
             if (plane.Raycast(ray, out float enter))
               current = ray.origin + ray.direction * enter;
            foreach (var item in selection)
              item.transform.position += current - prev;
            prev = current;
          },

          end: (x, vec) => {
            
          }
          
        )
      );

    }
  }
}