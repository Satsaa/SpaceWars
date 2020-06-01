


namespace SpaceGame {

  using static Muc.BinUtil;

  public readonly struct ShipValidator : IValidator<Ship> {

    public readonly ShipType shipType;

    public ShipValidator(ShipType shipType) {
      this.shipType = shipType;
    }

    bool IValidator<Ship>.Validate(Ship ship) {
      if (!ship) return false;
      return BitsOverlap(ship.shipType, shipType);
    }
  }

}