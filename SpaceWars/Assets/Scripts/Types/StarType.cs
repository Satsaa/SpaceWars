



namespace SpaceGame {

  using System;

  [Flags]
  public enum StarType : uint {
    None = 0,

    WhiteDwarf = 1 << 0,
    SequenceStar = 1 << 1,
    Giant = 1 << 2,
    SuperGiant = 1 << 3,
    NeutronStar = 1 << 4,
    BlackHole = 1 << 5,

    Any = uint.MaxValue >> -5,
  }

}