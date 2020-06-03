

namespace SpaceGame.MouseInput {

  using System;
  using System.Runtime.Serialization;


  [Serializable]
  public class HotkeyTypeHandlingViolation : Exception {
    public HotkeyTypeHandlingViolation() { }
    public HotkeyTypeHandlingViolation(string message) : base(message) { }
    public HotkeyTypeHandlingViolation(string message, Exception inner) : base(message, inner) { }
    protected HotkeyTypeHandlingViolation(SerializationInfo info, StreamingContext context) : base(info, context) { }
  }

}