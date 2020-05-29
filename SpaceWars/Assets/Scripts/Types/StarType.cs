



namespace SpaceGame {

  using System;

  [Flags]
  public enum StarType : uint {
    None = 0,

    WhiteDwarf = 1 << 1,
    SequenceStar = 1 << 2,
    Giant = 1 << 3,
    SuperGiant = 1 << 4,
    NeutronStar = 1 << 5,
    BlackHole = 1 << 6,

    Any = uint.MaxValue >> -(6),
  }

}