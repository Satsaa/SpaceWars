


namespace SpaceGame {

  using UnityEngine;

  public struct ValidatorAll<T> : IValidator<T> {
    IValidator<T>[] validators;

    public ValidatorAll(params IValidator<T>[] validators) {
      this.validators = validators;
    }

    public bool Validate(T target) {
      foreach (var validator in validators) {
        if (!validator.Validate(target)) return false;
      }
      return true;
    }
  }
}