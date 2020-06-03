



namespace SpaceGame {

  using System;

  [Flags]
  public enum AsteroidType : uint {
    None = 0,

    Any = 0, // uint.MaxValue >> -(1),
  }

}