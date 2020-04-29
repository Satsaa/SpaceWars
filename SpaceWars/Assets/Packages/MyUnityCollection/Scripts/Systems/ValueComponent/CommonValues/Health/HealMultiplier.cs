

namespace ValueComponents {

  using UnityEngine;


  public class HealMultiplier : HealthModifier {

    [SerializeField]
    float multiplier;

    public override float Modify(float current, Health value) {
      if (current > 0) return current * multiplier;
      return current;
    }
  }
}