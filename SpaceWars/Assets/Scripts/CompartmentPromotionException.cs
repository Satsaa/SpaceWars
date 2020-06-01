

namespace SpaceGame {

  using System;
  using System.Runtime.Serialization;


  [Serializable]
  public class CompartmentPromotionException : Exception {
    public CompartmentPromotionException() { }
    public CompartmentPromotionException(string message) : base(message) { }
    public CompartmentPromotionException(string message, Exception inner) : base(message, inner) { }
    protected CompartmentPromotionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
  }

}