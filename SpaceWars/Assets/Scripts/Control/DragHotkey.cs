


namespace SpaceGame.MouseInput {

  using System;
  using UnityEngine;

  using DragAction = System.Action<UnityEngine.GameObject, UnityEngine.Vector3>;


  [Serializable]
  public class DragHotkey : AtypicalHotkey {

    private static void ThrowError(GameObject _) =>
      throw new HotkeyTypeHandlingViolation(
        $"The {nameof(Hotkey.action)} delegate of a {nameof(DragHotkey)} must not be invoked"
      );

    public DragAction start;
    public DragAction drag;
    public DragAction end;

    public DragHotkey(HotkeySpecifier specifiers, Predicate<GameObject> predicate, bool noPromote, DragAction start, DragAction drag, DragAction end)
      : base(specifiers, predicate, noPromote, ThrowError) {

      this.start = start;
      this.drag = drag;
      this.end = end;
    }

  }

}