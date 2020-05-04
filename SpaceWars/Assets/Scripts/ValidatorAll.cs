


namespace SpaceGame {

  using UnityEngine;

  public struct ValidatorAll : IValidator<GameObject> {
    IValidator<GameObject>[] validators;

    public ValidatorAll(params IValidator<GameObject>[] validators) {
      this.validators = validators;
    }

    public bool Validate(GameObject target) {
      foreach (var validator in validators) {
        if (!validator.Validate(target)) return false;
      }
      return true;
    }
  }
}