



namespace SpaceGame {

  [System.Flags]
  public enum TeamType : uint {
    None = 0,

    Own = 1 << 0,
    Ally = 1 << 1,
    Unassigned = 1 << 2,
    Neutral = 1 << 3,
    Enemy = 1 << 4,

    Any = uint.MaxValue >> -4,
  }
}