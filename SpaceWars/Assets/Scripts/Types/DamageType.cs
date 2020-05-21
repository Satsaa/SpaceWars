



namespace SpaceGame {
  public enum DamageType {
    /// <summary> Damage caused by collisions and such. Heat if you go in to a star </summary>
    Physical,
    /// <summary> Laser </summary>
    Energy,
    /// <summary> Glowy particles, generally slower than lasers </summary>
    Plasma,
    /// <summary> Missiles etc. Missiles may also apply their own additional effects </summary>
    Explosive,
    /// <summary> Radiation related damage. Note that nuclear bombs usually also do explosive damage nearby </summary>
    Radiation,
  }

}