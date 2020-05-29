


namespace SpaceGame {

  using System.Linq;

  using UnityEngine;

  using static Muc.BinUtil;

  public readonly struct TeamValidator : IValidator<ITeamable> {

    public readonly TeamType teams;

    public TeamValidator(TeamType teams) {
      this.teams = teams;
    }

    bool IValidator<ITeamable>.Validate(ITeamable teamable) {

      return BitsOverlap(teamable.team, teams);
    }
  }

}