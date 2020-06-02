


namespace SpaceGame {

  using System;
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public static class Selection {


    public static GameObject primary { get => _primary; set { _primary = value; all.Add(value); } }
    private static GameObject _primary;

    public static IEnumerator<GameObject> enumerator => all.GetEnumerator();

    private static HashSet<GameObject> all { get; } = new HashSet<GameObject>();


    public static void SetPrimary(GameObject item) {
      all.Add(item);
      primary = item;
    }

    public static void Set(GameObject item) => Set(new GameObject[] { item });
    public static void Set(IEnumerable<GameObject> selection) {
      all.Clear();
      primary = null;
      foreach (var item in selection) {
        // First item becomes primary selection
        if (primary == null) primary = item;
        all.Add(item);
      }
    }

    public static void Add(GameObject item) => all.Add(item);
    public static void Add(IEnumerable<GameObject> selection) {
      foreach (var item in selection) {
        Add(item);
      }
    }

    public static void Remove(GameObject item) {
      if (all.Remove(item) && item == primary) ResetPrimary();
    }

    public static void RemoveWhere(Predicate<GameObject> match) {
      if (all.RemoveWhere(match) > 0 && match(primary)) ResetPrimary();
    }


    private static void ResetPrimary() {
      foreach (var item in all) {
        primary = item;
        return;
      }
    }
  }
}