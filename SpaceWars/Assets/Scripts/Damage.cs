


namespace SpaceGame {

  using System;


  [Serializable]
  public struct Damage {

    public float damage;
    public DamageType type;

    public Damage(float damage, DamageType type) {
      this.damage = damage;
      this.type = type;
    }

  }

}