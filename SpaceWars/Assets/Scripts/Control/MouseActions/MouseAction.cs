


namespace SpaceGame.MouseInput {

  using System;
  using UnityEngine;


  public abstract class MouseAction {

    /// <summary> By default any GameObject containing an IPart Component will be promoted to IPart.owner.gameObject </summary>
    public bool promote { get; private set; }

    /// <summary> When should this Validator be usable? </summary>
    public HotkeySpecifier specifiers { get => _specifiers; protected set { _specifiers = value; UpdatePoints(); } }
    private HotkeySpecifier _specifiers;

    /// <summary> Integer value which represents the priority of this kind of Validator </summary>
    public int priority { get; private set; }

    private void UpdatePoints() {
      this.priority = 0;
      if (specifiers.HasFlag(HotkeySpecifier.Persistent)) this.priority -= 0b_0001;
      if (specifiers.HasFlag(HotkeySpecifier.Priority)) this.priority += 0b_0010;
    }

    protected MouseAction(HotkeySpecifier specifiers, Predicate<GameObject> predicate, bool noPromote = false) {
      this.specifiers = specifiers;
      this.validate = predicate;
      this.promote = !noPromote;
    }

    /// <summary> Return true if the target is valid </summary>
    public Predicate<GameObject> validate { get; protected set; }

  }

}