



namespace SpaceGame {

  using System;

  [Flags]
  public enum StarType {
    None = 0,
    Any = ~0,

    Own = 1 << 0,
    Ally = 1 << 1,
    Neutral = 1 << 2,
    Enemy = 1 << 3,

    WhiteDwarf = 1 << 4,
    SequenceStar = 1 << 5,
    Giant = 1 << 6,
    SuperGiant = 1 << 7,
    NeutronStar = 1 << 8,
    BlackHole = 1 << 9,
  }

}