


namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;

  using UnityEngine;

  using Muc.Types.Extensions;
  using System.Threading.Tasks;

  public readonly struct MouseInputType {

    public readonly MouseInputSpecifier inputSpecifier;
    public readonly IValidator<GameObject> validator;

    public readonly int priorityPoints;

    public MouseInputType(MouseInputSpecifier inputSpecifier, IValidator<GameObject> validator) {
      this.inputSpecifier = inputSpecifier;
      this.validator = validator;

      this.priorityPoints = 0;
      if (inputSpecifier.HasFlag(MouseInputSpecifier.Static))
        this.priorityPoints -= 0b_0001;
      if (inputSpecifier.HasFlag(MouseInputSpecifier.Priority))
        this.priorityPoints += 0b_0010;
    }
  }

}