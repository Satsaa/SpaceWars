

namespace SpaceGame {

  using System;
  using System.Runtime.Serialization;


  [Serializable]
  public class PartOwnershipViolationException : Exception {
    public PartOwnershipViolationException() { }
    public PartOwnershipViolationException(string message) : base(message) { }
    public PartOwnershipViolationException(string message, Exception inner) : base(message, inner) { }
    protected PartOwnershipViolationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
  }

}