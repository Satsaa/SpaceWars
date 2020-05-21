


namespace SpaceGame {

  public interface IValidator<T> {
    bool Validate(T target, out T targetOverride);
  }

}