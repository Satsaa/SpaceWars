


namespace SpaceGame {

  using System;
  using System.Linq;
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  using MouseInput;


  [RequireComponent(typeof(MouseHotkeyHandler))]
  public class Selection : MonoBehaviour, IReadOnlyCollection<GameObject> {


    [SerializeField] GameObject primaryHighlighterPrefab;
    private GameObject primaryHighlighter;

    [SerializeField] GameObject secondaryHighlighterPrefab;
    private List<GameObject> secondaryHighlighters = new List<GameObject>();


    private GameObject primary;
    private HashSet<GameObject> all { get; } = new HashSet<GameObject>();


    private MouseHotkeyHandler handler;


    #region Interface implementation

    public IEnumerator<GameObject> GetEnumerator() => all.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => all.GetEnumerator();

    bool Contains(GameObject item) => all.Contains(item);
    public void CopyTo(GameObject[] array, int arrayIndex) => all.CopyTo(array, arrayIndex);

    public int Count => all.Count;
    public bool IsReadOnly => true;

    #endregion


    void Awake() {
      handler = GetComponent<MouseHotkeyHandler>();
    }

    void Start() {

      primaryHighlighter = Instantiate(primaryHighlighterPrefab);
      primaryHighlighter.SetActive(false);

      // Select new primary
      handler.AddMouseHotkey(
        new Hotkey(
          HotkeySpecifier.Static,
          predicate: (go) => true,
          action: (x) => {
            if (!x) Clear();
            else {
              if (Contains(x)) AddPrimary(x);
              else Set(x);
            }
          }
        )
      );

      // Add to selection
      handler.AddMouseHotkey(
        new Hotkey(
          HotkeySpecifier.Static | HotkeySpecifier.Shift,
          predicate: (go) => go,
          action: AddPrimary
        )
      );

      // Remove from selection
      handler.AddMouseHotkey(
        new Hotkey(
          HotkeySpecifier.Static | HotkeySpecifier.ControlShift,
          predicate: (go) => go,
          action: Remove
        )
      );
    }

    private void Clear() {
      all.Clear();
      primary = null;
    }

    void Update() {

    }

    void LateUpdate() {
      ShowSelection();
    }

    public void AddPrimary(GameObject item) {
      Add(item);
      primary = item;
    }

    public void Set(GameObject item) => Set(new GameObject[] { item });
    public void Set(IEnumerable<GameObject> selection) {
      all.Clear();
      primary = null;
      foreach (var item in selection) {
        // First item becomes primary selection
        if (primary == null) primary = item;
        Add(item);
      }
    }

    public void Add(GameObject item) {
      if (item is null) throw new ArgumentNullException(nameof(item));

      all.Add(item);
    }

    public void Add(IEnumerable<GameObject> selection) {
      foreach (var item in selection) {
        Add(item);
      }
    }


    public void Remove(GameObject item) {
      all.Remove(item);
      if (item == primary) ResetPrimary();
    }

    public void RemoveWhere(Predicate<GameObject> match) {
      all.RemoveWhere(match);
      if (match(primary)) ResetPrimary();
    }


    private void ResetPrimary() {
      foreach (var item in all) {
        primary = item;
        return;
      }
      primary = null;
    }

    private void ShowSelection() {

      if (!primary) {
        primaryHighlighter.SetActive(false);
      } else {
        primaryHighlighter.transform.position = primary.transform.position;
        primaryHighlighter.transform.localScale = primary.transform.lossyScale;
        primaryHighlighter.transform.rotation = transform.rotation;
        primaryHighlighter.SetActive(true);
      }

      // Allocate pool
      while (all.Count > secondaryHighlighters.Count) {
        secondaryHighlighters.Add(Instantiate(secondaryHighlighterPrefab));
      }

      // Enable highlighters
      var i = 0;
      foreach (var item in all) {


        var highlighter = secondaryHighlighters[i];

        highlighter.SetActive(primary != item);


        highlighter.transform.position = item.transform.position;
        highlighter.transform.localScale = item.transform.lossyScale;
        highlighter.transform.rotation = transform.rotation;

        i++;
      }

      // Disable the rest of the highLighters
      while (i < secondaryHighlighters.Count) {
        secondaryHighlighters[i].SetActive(false);

        i++;
      }

    }

  }
}