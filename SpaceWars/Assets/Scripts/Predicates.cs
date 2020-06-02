


namespace SpaceGame {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using static Muc.BinUtil;

  public static class Predicates {

    public static Predicate<ITeamable> TeamPredicate(TeamType teams) => (teamable) => BitsOverlap(teamable.team, teams);

    public static Predicate<Ship> ShipTypePredicate(ShipType shipTypes) => (ship) => BitsOverlap(ship.shipType, shipTypes);


    public static Predicate<T> All<T>(IEnumerable<Predicate<T>> predicates) {
      var array = predicates.ToArray();
      return (T value) => {
        foreach (var predicate in array)
          if (!predicate(value)) return false;
        return true;
      };
    }


    public static Predicate<T> Any<T>(IEnumerable<Predicate<T>> predicates) {
      var array = predicates.ToArray();
      return (T value) => {
        foreach (var predicate in array)
          if (predicate(value)) return true;
        return false;
      };
    }
  }
}