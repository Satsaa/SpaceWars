

namespace SpaceGame {

  using System;
  using System.Runtime.Serialization;


  [Serializable]
  public class PartPromotionException : Exception {
    public PartPromotionException() { }
    public PartPromotionException(string message) : base(message) { }
    public PartPromotionException(string message, Exception inner) : base(message, inner) { }
    protected PartPromotionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
  }

}