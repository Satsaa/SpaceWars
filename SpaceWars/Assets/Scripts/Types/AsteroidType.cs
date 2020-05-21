



namespace SpaceGame {

  using System;

  [Flags]
  public enum AsteroidType {
    None = 0,
    Any = ~0,

    Own = 1 << 0,
    Ally = 1 << 1,
    Neutral = 1 << 2,
    Enemy = 1 << 3,

  }

}