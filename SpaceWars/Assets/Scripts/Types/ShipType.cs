



namespace SpaceGame {

  using System;

  [Flags]
  public enum ShipType : uint {

    None = 0,

    Fighter = 1 << 0,
    Ship = 1 << 1,
    MotherShip = 1 << 2,
    Station = 1 << 3,

    Any = uint.MaxValue >> -3,
  }

}