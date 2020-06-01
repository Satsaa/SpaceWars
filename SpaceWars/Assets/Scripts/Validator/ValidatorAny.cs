


namespace SpaceGame {

  using UnityEngine;


  public struct ValidatorAny<T> : IValidator<T> {
    IValidator<T>[] validators;

    public ValidatorAny(params IValidator<T>[] validators) {
      this.validators = validators;
    }

    public bool Validate(T target) {
      foreach (var validator in validators) {
        if (validator.Validate(target)) return true;
      }
      return false;
    }
  }
}