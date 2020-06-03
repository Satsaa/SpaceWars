


namespace SpaceGame.MouseInput {

  using System;
  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;

  using Muc.Types.Extensions;
  using System.Threading.Tasks;

  public class Hotkey {

    /// <summary> Defines the activation circumstances of this MouseHotkey </summary>
    public readonly HotkeySpecifier specifiers;

    /// <summary> Integer value which represents the priority of this kind of MouseHotkey </summary>
    public readonly int priorityPoints;

    /// <summary> By default any GameObject containing an IPart Component will be promoted to IPart.owner </summary>
    public bool promote;

    /// <summary> Return true if the target is valid </summary>
    public Predicate<GameObject> predicate;

    /// <summary> Will be called automatically if the user activates a valid target </summary>
    public Action<GameObject> action;

    public Hotkey(HotkeySpecifier specifiers, Predicate<GameObject> predicate, Action<GameObject> action)
      : this(specifiers, predicate, false, action) { }

    public Hotkey(HotkeySpecifier specifiers, Predicate<GameObject> predicate, bool noPromote, Action<GameObject> action) {
      this.specifiers = specifiers;
      this.predicate = predicate;
      this.action = action;
      this.promote = !noPromote;

      this.priorityPoints = 0;
      if (specifiers.HasFlag(HotkeySpecifier.Static)) this.priorityPoints -= 0b_0001;
      if (specifiers.HasFlag(HotkeySpecifier.Priority)) this.priorityPoints += 0b_0010;
    }
  }

}