


namespace SpaceGame.MouseInput {

  using System;
  using UnityEngine;

  [Serializable]
  public class ClickAction : MouseAction {


    /// <summary> Will be called automatically if the user activates a valid target </summary>
    public Action<GameObject> action;

    public ClickAction(HotkeySpecifier specifiers, Predicate<GameObject> predicate, Action<GameObject> action)
      : this(specifiers, predicate, false, action) { }

    public ClickAction(HotkeySpecifier specifiers, Predicate<GameObject> predicate, bool noPromote, Action<GameObject> action)
      : base(specifiers, predicate, noPromote) {

      this.action = action;

    }
  }

}