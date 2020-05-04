


namespace SpaceGame {

  using UnityEngine;


  public struct ValidatorAny : IValidator<GameObject> {
    IValidator<GameObject>[] validators;

    public ValidatorAny(params IValidator<GameObject>[] validators) {
      this.validators = validators;
    }

    public bool Validate(GameObject target) {
      foreach (var validator in validators) {
        if (validator.Validate(target)) return true;
      }
      return false;
    }
  }
}