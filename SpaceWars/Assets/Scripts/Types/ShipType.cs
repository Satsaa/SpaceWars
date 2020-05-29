



namespace SpaceGame {

  using System;

  [Flags]
  public enum ShipType : uint {

    None = 0,

    Fighter = 1 << 1,
    Ship = 1 << 2,
    MotherShip = 1 << 3,
    Station = 1 << 4,

    Any = uint.MaxValue >> -(4),
  }

}