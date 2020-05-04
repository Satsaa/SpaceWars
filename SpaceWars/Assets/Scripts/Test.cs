


using UnityEngine;
using SpaceGame;
using System;

public class Test : MonoBehaviour {
  void Reset() {
    print($"{ShipType.None}: {Convert.ToString((int)ShipType.None, 2)}");
    print($"{ShipType.Any}: {Convert.ToString((int)ShipType.Any, 2)}");
    print($"{ShipType.Fighter}: {Convert.ToString((int)ShipType.Fighter, 2)}");

  }
}