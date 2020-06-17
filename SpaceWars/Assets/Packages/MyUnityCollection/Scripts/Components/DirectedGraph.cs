﻿

namespace Muc.Components {

  using System.Collections.Generic;
  using UnityEngine;
  using Muc.Types;

  /// <summary>
  /// Cyclic Directed Graph. Useful for creating predefined path or networks
  /// </summary> 
  public class DirectedGraph : MonoBehaviour {

    public List<DirectedNode> nodes = new List<DirectedNode>();

    void Reset() {
      var node1 = (DirectedNode)DirectedNode.CreateInstance(typeof(DirectedNode));
      var node2 = (DirectedNode)ScriptableObject.CreateInstance(typeof(DirectedNode));
      var node3 = (DirectedNode)ScriptableObject.CreateInstance(typeof(DirectedNode));
      node1.position = new Vector3(1, 1, 1);
      node2.position = new Vector3(0, 0, 0);
      node3.position = new Vector3(-1, -1, -1);

      node2.AddOutbound(node1);
      node2.AddOutbound(node3);
      nodes = new List<DirectedNode>() { node1, node2, node3 };
    }
  }
}

#if UNITY_EDITOR
namespace Muc.Components.Editor {

  using System.Collections.Generic;

  using UnityEngine;
  using UnityEditor;

  using Muc.Types.Extensions;
  using Muc.Types;

  /// <summary>
  /// Relaxed DirectedGraph editor
  /// </summary>
  [CustomEditor(typeof(DirectedGraph), true)]
  public class DirectedGraphEditor : Editor {

    private DirectedGraph t { get => (DirectedGraph)target; }

    private Camera cam { get => Camera.current; }
    private Quaternion cameraLook { get => Quaternion.LookRotation(cam.transform.forward, cam.transform.up); }

    private List<DirectedNode> selection = new List<DirectedNode>();
    private Vector3 selectionAveragePos;

    private bool clickUsed;
    private bool mouse = false;
    private Vector2 mousePos;
    private bool shift { get => Event.current.shift; }

    private bool control { get => controlRight || controlLeft; }
    private bool controlRight = false;
    private bool controlLeft = false;

    private bool alt { get => altRight || altLeft; }
    private bool altRight = false;
    private bool altLeft = false;


    DirectedGraphEditor() {
      Undo.undoRedoPerformed += UpdateAveragePosition;
    }

    void OnSceneGUI() {
      switch (Event.current.type) {
        case EventType.Repaint:
          break;

        case EventType.MouseMove:
          mousePos = Event.current.mousePosition;
          break;

        case EventType.MouseDown:
          if (Event.current.button == 0) mouse = true;
          break;

        case EventType.MouseUp:
          if (Event.current.button == 0) {
            clickUsed = false;
            mouse = false;
            soloCreating = false;
          }
          break;

        case EventType.KeyDown:
          if (Event.current.keyCode == KeyCode.RightAlt) altRight = true;
          else if (Event.current.keyCode == KeyCode.LeftAlt) altLeft = true;

          if (Event.current.keyCode == KeyCode.RightControl) controlRight = true;
          else if (Event.current.keyCode == KeyCode.LeftControl) controlLeft = true;
          break;

        case EventType.KeyUp:
          if (Event.current.keyCode == KeyCode.RightAlt) altRight = false;
          else if (Event.current.keyCode == KeyCode.LeftAlt) altLeft = false;

          if (Event.current.keyCode == KeyCode.RightControl) controlRight = false;
          else if (Event.current.keyCode == KeyCode.LeftControl) controlLeft = false;
          break;
      }
      Draw();
    }

    public DirectedNode FindClosestNode(DirectedNode node, out float dist) {
      DirectedNode minNode = null;
      var minDistSq = float.PositiveInfinity;
      foreach (var other in t.nodes) {
        if (other == node) continue;
        var distSq = (node.position - other.position).sqrMagnitude;
        if (distSq < minDistSq) {
          minNode = other;
          minDistSq = distSq;
        }
      }
      dist = Mathf.Sqrt(minDistSq);
      return minNode;
    }
    public DirectedNode FindClosestNode(Vector3 position, out float dist) {
      DirectedNode minNode = null;
      var minDistSq = float.PositiveInfinity;
      foreach (var node in t.nodes) {
        var distSq = (position - node.position).sqrMagnitude;
        if (distSq < minDistSq) {
          minNode = node;
          minDistSq = distSq;
        }
      }
      dist = Mathf.Sqrt(minDistSq);
      return minNode;
    }
    public DirectedNode FindClosestNodeToLine(Line line, out float dist) {
      DirectedNode minNode = null;
      var distsq = float.PositiveInfinity;
      foreach (var node in t.nodes) {
        var ndistsq = (node.position - line.ClampPoint(node.position)).sqrMagnitude;
        if (ndistsq < distsq) {
          minNode = node;
          distsq = ndistsq;
        }
      }
      dist = Mathf.Sqrt(distsq);
      return minNode;
    }
    public DirectedNode FindClosestNodeToRay(Ray ray, out float dist) {
      DirectedNode minNode = null;
      dist = float.PositiveInfinity;
      foreach (var node in t.nodes) {
        var nodeDist = DistanceToRay(ray, node.position);
        if (nodeDist < dist) {
          minNode = node;
          dist = nodeDist;
        }
      }
      return minNode;
    }
    public Vector3 FindClosestPointToLine(Line line, out float dist) {
      Vector3 minPos = Vector3.zero;
      var distsq = float.PositiveInfinity;
      foreach (var node in t.nodes) {
        foreach (var outNode in node.outbound) {
          var outLine = node.LineTo(outNode);
          var distLine = line.ShortestConnectingLine(outLine);
          if (distLine.lengthsq < distsq) {
            minPos = distLine.end;
            distsq = distLine.lengthsq;
          }
        }
      }
      if (distsq == float.PositiveInfinity) {
        dist = Vector3.Distance(Vector3.zero, line.ClampPoint(Vector3.zero));
        return Vector3.zero;
      }
      dist = Mathf.Sqrt(distsq);
      return minPos;
    }

    void Draw() {
      var defaultColor = Handles.color;
      foreach (var node in t.nodes) {
        Handles.color = defaultColor;
        SelectionMoveHandle(node);
      }
      if (shift)
        if (selection.Count == 0)
          DrawNewSoloPair();
        else
          DrawNewConnectedNode();
      else
        soloCreating = false;
      DrawAveragePositionHandle();

      foreach (var node in t.nodes) {
        DrawConnections(node);
      }
    }

    void DrawNewConnectedNode() {
      if (Event.current.isMouse) Event.current.Use();
      var ray = cam.ScreenPointToRay(new Vector2(mousePos.x, cam.pixelHeight - mousePos.y));
      var plane = new Plane(-cam.transform.forward, selectionAveragePos);
      if (plane.Raycast(ray, out var distance)) {
        var target = ray.GetPoint(distance);
        foreach (var node in selection) {
          Handles.DrawDottedLine(node.position, target, 3);
          DrawDirArrow(node.position, target);
        }
        Handles.Button(target, cameraLook, 0.1f, 0.1f, Handles.RectangleHandleCap);
        if (mouse && !clickUsed) {
          var node = CreateNode(target);
          node.AddInbound(selection);
          foreach (var selNode in selection)
            Undo.RegisterCompleteObjectUndo(selNode, "Create node");
          Select(node);
          Undo.RegisterCompleteObjectUndo(node, "Create node");
          clickUsed = true;
          Dirty();
        }
      }
    }

    Vector3 soloStartPos;
    bool soloCreating;
    void DrawNewSoloPair() {
      if (Event.current.isMouse) Event.current.Use();
      var ray = cam.ScreenPointToRay(new Vector2(mousePos.x, cam.pixelHeight - mousePos.y));
      var closestPoint = FindClosestPointToLine(new Line(ray.origin, ray.origin + ray.direction * 5), out var dist);
      Handles.SphereHandleCap(0, closestPoint, Quaternion.identity, 0.1f, EventType.Repaint);
      var plane = new Plane(-cam.transform.forward, closestPoint);
      if (!soloCreating) {
        if (plane.Raycast(ray, out var distance)) {
          soloStartPos = ray.GetPoint(distance);
          soloCreating = true;
        }
      } else {
        if (plane.Raycast(ray, out var distance)) {
          var target = ray.GetPoint(distance);
          Handles.DrawDottedLine(soloStartPos, target, 3);
          DrawDirArrow(soloStartPos, target);

          Handles.Button(target, cameraLook, 0.1f, 0.1f, Handles.RectangleHandleCap);
          if (mouse && !clickUsed) {
            var node1 = CreateNode(soloStartPos);
            var node2 = CreateNode(target);
            node1.AddOutbound(node2);
            Undo.RegisterCompleteObjectUndo(t, "Create node pair");
            clickUsed = true;
            Select(node2);
            Dirty();
          }
        }
      }
    }

    void DrawAveragePositionHandle() {
      if (selection.Count == 0) return;
      EditorGUI.BeginChangeCheck();
      Vector3 newPos = Handles.PositionHandle(selectionAveragePos, Quaternion.identity);
      if (EditorGUI.EndChangeCheck()) {
        foreach (var node in selection) {
          node.position += newPos - selectionAveragePos;
          Undo.RegisterCompleteObjectUndo(node, "Move node selection");
        }
        UpdateAveragePosition();
        Dirty();
      }
    }

    void SelectionMoveHandle(DirectedNode node) {
      if (selection.Contains(node)) {
        Handles.color = Color.green;
        if (Handles.Button(node.position, cameraLook, 0.1f, 0.1f, Handles.RectangleHandleCap)) {
          selection.Remove(node);
          UpdateAveragePosition();
        }
      } else {
        if (Handles.Button(node.position, cameraLook, 0.1f, 0.1f, Handles.RectangleHandleCap)) {
          selection.Add(node);
          UpdateAveragePosition();
        }
      }
    }

    void Select(DirectedNode node) {
      selection.Clear();
      selection.Add(node);
      UpdateAveragePosition();
    }
    void Select(List<DirectedNode> nodes) {
      selection.Clear();
      selection.AddRange(nodes);
      UpdateAveragePosition();
    }

    void DrawMoveHandle(DirectedNode node) {
      EditorGUI.BeginChangeCheck();
      Vector3 newPos = Handles.PositionHandle(node.position, Quaternion.identity);
      if (EditorGUI.EndChangeCheck()) {
        node.position = newPos;
        Undo.RegisterCompleteObjectUndo(node, "Move node");
        Dirty();
      }
    }

    void DrawConnections(DirectedNode node) {
      foreach (var outNode in node.outbound) {
        DrawDirArrow(node.position, outNode.position);
        Handles.DrawLine(node.position, outNode.position);
      }
    }

    void DrawDirArrow(Vector3 sourcePoint, Vector3 endPoint) {
      var dist = Vector3.Distance(endPoint, sourcePoint);
      if (dist < 0.0001f) return;
      var maxSize = 0.1f;
      var size = Mathf.Min(maxSize, dist / 10);
      Handles.ConeHandleCap(0, endPoint - (endPoint - sourcePoint).SetLen(size) * 0.7f, Quaternion.LookRotation(endPoint - sourcePoint), size, EventType.Repaint);
    }

    DirectedNode CreateNode(Vector3 position) {
      var node = (DirectedNode)ScriptableObject.CreateInstance(typeof(DirectedNode));
      t.nodes.Add(node);
      node.position = position;
      return node;
    }

    void Dirty() {
      if (!Application.isPlaying) {
        EditorUtility.SetDirty(t);
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
      }
    }

    void UpdateAveragePosition() {
      Vector3 average = Vector3.zero;
      foreach (var node in selection)
        average += node.position;
      average /= selection.Count;
      selectionAveragePos = average;
    }
    public static float DistanceToRay(Ray ray, Vector3 point) {
      return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
    }
  }

}
#endif