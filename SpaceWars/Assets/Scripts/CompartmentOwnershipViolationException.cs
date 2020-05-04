

namespace SpaceGame {

  using System;
  using System.Runtime.Serialization;


  [Serializable]
  public class CompartmentOwnershipViolationException : Exception {
    public CompartmentOwnershipViolationException() { }
    public CompartmentOwnershipViolationException(string message) : base(message) { }
    public CompartmentOwnershipViolationException(string message, Exception inner) : base(message, inner) { }
    protected CompartmentOwnershipViolationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
  }

}