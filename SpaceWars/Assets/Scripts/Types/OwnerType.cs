



namespace SpaceGame {

  [System.Flags]
  public enum OwnerType {
    None = 0,
    Any = ~0,

    Own = 1 << 0,
    Ally = 1 << 1,
    Neutral = 1 << 2,
    Enemy = 1 << 3,

    All = Own | Ally | Neutral | Enemy,
  }
}