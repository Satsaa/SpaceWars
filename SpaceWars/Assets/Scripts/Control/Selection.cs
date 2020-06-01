


namespace SpaceGame {

  using System.Collections.ObjectModel;
  using System.Collections.Specialized;
  using UnityEngine;

  public static class Selection {

    public static GameObject main {
      get => mainContained ? _main : (_all.Count == 0 ? null : _all[0]);
      set { _main = main; Rescan(); }
    }
    private static GameObject _main;

    public static ObservableCollection<GameObject> all {
      get => _all;
      set { _all = value; Rescan(); }
    }
    private static ObservableCollection<GameObject> _all = new ObservableCollection<GameObject>();

    // Whether the main selection is within the full selection
    private static bool mainContained = false;

    static Selection() {
      all.CollectionChanged += OnChange;
    }

    private static void OnChange(object sender, NotifyCollectionChangedEventArgs e) {
      if (e.Action == NotifyCollectionChangedAction.Reset) {
        Rescan();
        return;
      }
      if (mainContained) {
        // Check new items also, because main may have been added again
        mainContained = !e.OldItems.Contains(main) || e.NewItems.Contains(main); ;
      } else {
        mainContained = e.NewItems.Contains(main);
      }
    }

    private static void Rescan() {
      mainContained = all.Contains(main);
    }
  }
}