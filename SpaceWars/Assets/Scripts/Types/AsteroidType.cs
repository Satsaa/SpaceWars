



namespace SpaceGame {

  using System;

  [Flags]
  public enum AsteroidType : uint {
    None = 0,

    Any = uint.MaxValue >> -(0),
  }

}