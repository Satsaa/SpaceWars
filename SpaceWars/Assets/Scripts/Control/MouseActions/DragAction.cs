


namespace SpaceGame.MouseInput {

  using System;
  using UnityEngine;

  using DragAction = System.Action<UnityEngine.GameObject, UnityEngine.Vector3>;


  public class DragAction : MouseAction {

    public Action<GameObject, Vector3> start;
    public Action<GameObject, Vector3> drag;
    public Action<GameObject, Vector3> end;

    public DragAction(HotkeySpecifier specifiers, Predicate<GameObject> predicate, bool noPromote, Action<GameObject, Vector3> start, Action<GameObject, Vector3> drag, Action<GameObject, Vector3> end)
      : base(specifiers, predicate, noPromote) {

      this.start = start;
      this.drag = drag;
      this.end = end;
    }

  }

}