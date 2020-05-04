


namespace SpaceGame {

  using UnityEngine;

  public struct TargetType : IValidator<GameObject> {

    public ShipType shipType;
    public StarType starType;
    public AsteroidType asteroidType;
    public PlanetType planetType;

    #region Overloads

    // https://www.mathsisfun.com/combinatorics/combinations-permutations-calculator.html

    public TargetType(ShipType shipType = 0, StarType starType = 0, PlanetType planetType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(ShipType shipType = 0, AsteroidType asteroidType = 0, StarType starType = 0, PlanetType planetType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(ShipType shipType = 0, AsteroidType asteroidType = 0, PlanetType planetType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(ShipType shipType = 0, PlanetType planetType = 0, StarType starType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(ShipType shipType = 0, PlanetType planetType = 0, AsteroidType asteroidType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(StarType starType = 0, ShipType shipType = 0, AsteroidType asteroidType = 0, PlanetType planetType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(StarType starType = 0, ShipType shipType = 0, PlanetType planetType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(StarType starType = 0, AsteroidType asteroidType = 0, ShipType shipType = 0, PlanetType planetType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(StarType starType = 0, AsteroidType asteroidType = 0, PlanetType planetType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(StarType starType = 0, PlanetType planetType = 0, ShipType shipType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(StarType starType = 0, PlanetType planetType = 0, AsteroidType asteroidType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(AsteroidType asteroidType = 0, ShipType shipType = 0, StarType starType = 0, PlanetType planetType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(AsteroidType asteroidType = 0, ShipType shipType = 0, PlanetType planetType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(AsteroidType asteroidType = 0, StarType starType = 0, ShipType shipType = 0, PlanetType planetType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(AsteroidType asteroidType = 0, StarType starType = 0, PlanetType planetType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(AsteroidType asteroidType = 0, PlanetType planetType = 0, ShipType shipType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(AsteroidType asteroidType = 0, PlanetType planetType = 0, StarType starType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(PlanetType planetType = 0, ShipType shipType = 0, StarType starType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(PlanetType planetType = 0, ShipType shipType = 0, AsteroidType asteroidType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(PlanetType planetType = 0, StarType starType = 0, ShipType shipType = 0, AsteroidType asteroidType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(PlanetType planetType = 0, StarType starType = 0, AsteroidType asteroidType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(PlanetType planetType = 0, AsteroidType asteroidType = 0, ShipType shipType = 0, StarType starType = 0) : this(shipType, starType, asteroidType, planetType) { }
    public TargetType(PlanetType planetType = 0, AsteroidType asteroidType = 0, StarType starType = 0, ShipType shipType = 0) : this(shipType, starType, asteroidType, planetType) { }

    #endregion

    public TargetType(ShipType shipType = 0, StarType starType = 0, AsteroidType asteroidType = 0, PlanetType planetType = 0) {
      this.shipType = shipType;
      this.starType = starType;
      this.asteroidType = asteroidType;
      this.planetType = planetType;
    }

    public bool Validate(GameObject gameObject) {

      return false;
    }
  }

}