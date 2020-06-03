


namespace SpaceGame.MouseInput {

  using System;
  using UnityEngine;

  using DragAction = System.Action<UnityEngine.GameObject, UnityEngine.Vector3>;


  [Serializable]
  public class AtypicalHotkey : Hotkey {
    
    public AtypicalHotkey(HotkeySpecifier specifiers, Predicate<GameObject> predicate, Action<GameObject> action)
      : base(specifiers, predicate, action) {    }

    public AtypicalHotkey(HotkeySpecifier specifiers, Predicate<GameObject> predicate, bool noPromote, Action<GameObject> action)
      : base(specifiers, predicate, noPromote, action) {    }
  }

}