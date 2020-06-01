


namespace SpaceGame {

  using System;

  [System.Flags]
  public enum MouseInputSpecifier : uint {

    None = 0,

    /// <summary>
    /// Static InputTypes will not be removed automatically.
    /// Static InputTypes will be ignored before any non-static InputTypes are settled.
    /// </summary>
    Static = 1 << 1,

    /// <summary> Priority InputTypes will be settled before any non-priority InputTypes </summary>
    Priority = 1 << 2,


    /// <summary> Require modifier1 key </summary>
    Control = 1 << 3,
    /// <summary> Require modifier2 key </summary>
    Alt = 1 << 4,
    /// <summary> Require modifier3 key </summary>
    Shift = 1 << 5,

  }
}