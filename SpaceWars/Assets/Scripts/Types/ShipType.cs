



namespace SpaceGame {

  using System;

  [Flags]
  public enum ShipType {

    None = 0,
    Any = ~0,

    Own = 1 << 0,
    Ally = 1 << 1,
    Neutral = 1 << 2,
    Enemy = 1 << 3,
    TeamFlags = Own | Ally | Neutral | Enemy,

    Fighter = 1 << 4,
    Ship = 1 << 5,
    MotherShip = 1 << 6,
    Station = 1 << 7,
    TypeFlags = Fighter | Ship | MotherShip | Station,
  }

}