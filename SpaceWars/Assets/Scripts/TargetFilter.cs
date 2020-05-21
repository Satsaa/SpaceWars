


namespace SpaceGame {

  using System.Linq;

  using UnityEngine;

  public struct TargetFilter : IValidator<GameObject> {

    public ShipType shipType;
    public StarType starType;
    public AsteroidType asteroidType;
    public PlanetType planetType;

    #region Overloads

    // https://www.mathsisfun.com/combinatorics/combinations-permutations-calculator.html

    public TargetFilter(ShipType shipType = 0, StarType starType = 0, PlanetType planetType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(ShipType shipType = 0, AsteroidType asteroidType = 0, StarType starType = 0, PlanetType planetType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(ShipType shipType = 0, AsteroidType asteroidType = 0, PlanetType planetType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(ShipType shipType = 0, PlanetType planetType = 0, StarType starType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(ShipType shipType = 0, PlanetType planetType = 0, AsteroidType asteroidType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(StarType starType = 0, ShipType shipType = 0, AsteroidType asteroidType = 0, PlanetType planetType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(StarType starType = 0, ShipType shipType = 0, PlanetType planetType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(StarType starType = 0, AsteroidType asteroidType = 0, ShipType shipType = 0, PlanetType planetType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(StarType starType = 0, AsteroidType asteroidType = 0, PlanetType planetType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(StarType starType = 0, PlanetType planetType = 0, ShipType shipType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(StarType starType = 0, PlanetType planetType = 0, AsteroidType asteroidType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(AsteroidType asteroidType = 0, ShipType shipType = 0, StarType starType = 0, PlanetType planetType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(AsteroidType asteroidType = 0, ShipType shipType = 0, PlanetType planetType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(AsteroidType asteroidType = 0, StarType starType = 0, ShipType shipType = 0, PlanetType planetType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(AsteroidType asteroidType = 0, StarType starType = 0, PlanetType planetType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(AsteroidType asteroidType = 0, PlanetType planetType = 0, ShipType shipType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(AsteroidType asteroidType = 0, PlanetType planetType = 0, StarType starType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(PlanetType planetType = 0, ShipType shipType = 0, StarType starType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(PlanetType planetType = 0, ShipType shipType = 0, AsteroidType asteroidType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(PlanetType planetType = 0, StarType starType = 0, ShipType shipType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(PlanetType planetType = 0, StarType starType = 0, AsteroidType asteroidType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(PlanetType planetType = 0, AsteroidType asteroidType = 0, ShipType shipType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetFilter(PlanetType planetType = 0, AsteroidType asteroidType = 0, StarType starType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }

    #endregion

    public TargetFilter(ShipType shipType = 0, StarType starType = 0, AsteroidType asteroidType = 0, PlanetType planetType = 0) {
      this.shipType = shipType;
      this.starType = starType;
      this.asteroidType = asteroidType;
      this.planetType = planetType;
    }

    bool IValidator<GameObject>.Validate(GameObject gameObject, out GameObject targetOverride) {
      targetOverride = gameObject;

      if (ValidateShipType(shipType, gameObject)) return true;

      return false;
    }

    public static bool ValidateTeam(OwnerType ownerType, ShipType validTeams) => ValidateTeam(ownerType, (OwnerType)validTeams);
    public static bool ValidateTeam(OwnerType ownerType, StarType validTeams) => ValidateTeam(ownerType, (OwnerType)validTeams);
    public static bool ValidateTeam(OwnerType ownerType, AsteroidType validTeams) => ValidateTeam(ownerType, (OwnerType)validTeams);
    public static bool ValidateTeam(OwnerType ownerType, PlanetType validTeams) => ValidateTeam(ownerType, (OwnerType)validTeams);
    public static bool ValidateTeam(OwnerType ownerType, OwnerType validTeams) {
      return (ownerType & validTeams) != 0;
    }


    public static bool ValidateShipType(ShipType shipType, GameObject gameObject) {
      if (shipType == 0) return false;

      var ship = gameObject.GetComponent<Ship>();
      if (!ship) return false;

      if (!ValidateTeam(ship.ownerType, shipType)) return false;

      if (shipType == ShipType.Any) {
        return ship;
      }

      // Check if unselectable
      if ((shipType & (ShipType.TypeFlags)) == 0) {
        return false;
      }


      if ((shipType & ShipType.TeamFlags) != ShipType.TeamFlags) {
        var team = ship.ownerType;
        switch (team) {
          case OwnerType.Own:
            if (!shipType.HasFlag(ShipType.Own))
              return false;
            break;
          case OwnerType.Ally:
            if (!shipType.HasFlag(ShipType.Ally))
              return false;
            break;
          case OwnerType.Neutral:
            if (!shipType.HasFlag(ShipType.Neutral))
              return false;
            break;
          case OwnerType.Enemy:
            if (!shipType.HasFlag(ShipType.Enemy))
              return false;
            break;
        }
      }

      if ((shipType & ShipType.TypeFlags) != ShipType.TypeFlags) {

      }

      return true;
    }
  }

}