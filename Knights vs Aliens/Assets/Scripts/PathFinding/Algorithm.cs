using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// Algorithm which is used for patchfinding [A*-Dijkstra]
/// </summary>
public class Algorithm : MonoBehaviour {

	/// <summary>
	/// The start. and end point .
	/// </summary>
	public Transform start, end;

	Movement movement;
	/// <summary>
	/// The is Dijkstra or A*
	/// </summary>
	public bool isDijkstra  = false;
	public bool isAstar = false;
	public bool fillAStart = false;

	private Grid grid;
	private	List<Element> openListDraw = new List<Element>();
	/// <summary>
	/// Awake this instance.
	/// use Awake method here instead of Start method to prevent null point execption.gird need to bed set before starting of algorithm.
	/// </summary>
	void Awake() {
		movement = GetComponent<Movement> ();
		grid = GetComponent<Grid>();
	}
		
	public void FindPatch(Vector3 startPosition, Vector3 endPosition,int approach){

		if (approach == 0) {
			if (isAstar || isDijkstra) {
				StartCoroutine (Find (startPosition, endPosition));
			} 

		}

	}
	/// <summary>
	/// Traces the patch. in reverse.
	/// </summary>
	/// <param name="startElement">Start element.</param>
	/// <param name="endElement">End element.</param>
	private Vector3[] TraceThePatch(Element startElement, Element endElement) {
		List<Element> patch = new List<Element>();
		Element currentNode = endElement;

		while (currentNode != startElement) {
			patch.Add(currentNode);
			currentNode = currentNode.Parent;
		}

		Vector3[] patchPoints = smoothMap (patch);
		Array.Reverse (patchPoints);
		return patchPoints;
	}

	Vector3[] smoothMap(List<Element> patch){
		List<Vector3> patchPoints = new List<Vector3> ();
		Vector2 previousDir = Vector3.zero;

		for (int i = 0; i < patch.Count - 1; i++) {
			Vector2 newDir = new Vector2 ((patch [i].X - patch [i + 1].X), patch [i].Y - patch [i + 1].Y);
			if (newDir != previousDir) {
				patchPoints.Add (patch [i].Location);
			}
			previousDir = newDir;
		}

		return patchPoints.ToArray ();
	}

	/// <summary>
	/// Gets the distance.
	/// Note: that in fucntion we assume that cost between to direct neighbours is 10 otherwise is 14.[ 1 and 1.4 * 10 = 10 and 14]
	/// </summary>
	/// <returns>The distance.</returns>
	/// <param name="ElementStart">Element start.</param>
	/// <param name="ElementEnd">Element end.</param>
	private int GetDistance( Element startElement,Element endElement) {
		int dstX = Mathf.Abs (startElement.X - endElement.X);
		int dstY = Mathf.Abs (startElement.Y - endElement.Y);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}

	/// <summary>
	/// Find the patch from startPosition to endPosition.
	/// </summary>
	/// <param name="startPosition">Start position.</param>
	/// <param name="endPosition">End position.</param>
	IEnumerator Find(Vector3 startPosition, Vector3 endPosition) {

		Element startElement = grid.getNodeFromLocation(startPosition);
		Element EndElement = grid.getNodeFromLocation(endPosition);

		Vector3[] patchPoints = new Vector3[0];
		bool patchCheck = false;

		if (startElement.CanWalk && EndElement.CanWalk) {
			HeapDs<Element> openList = new HeapDs<Element>(grid.GirdMax);
			HashSet<Element> closedList = new HashSet<Element>();
			openList.Add(startElement);
			openListDraw.Add (startElement);

			while (openList.Count > 0) {
				Element currentNode = openList.RemoveFirst();
				closedList.Add(currentNode);

				if (currentNode == EndElement) {
					patchCheck = true;
					break;
				}

				foreach (Element neighbour in grid.GetNeighbours(currentNode)) {
					if (!neighbour.CanWalk || closedList.Contains(neighbour)) {
						continue;
					}

					int newMovementCostToNeighbour = currentNode.ConnectionCost + GetDistance(currentNode, neighbour);
					if (newMovementCostToNeighbour < neighbour.ConnectionCost || !openList.Contains(neighbour)) {
						neighbour.ConnectionCost = newMovementCostToNeighbour;

						if(!isDijkstra)
							neighbour.HuresticCost = GetDistance(neighbour, EndElement);

						neighbour.Parent = currentNode;

						if (!openList.Contains (neighbour)) {
							openList.Add (neighbour);
							openListDraw.Add (neighbour);
						}
						else {
							openList.UpdateItem(neighbour);
						}
					}
				}
			}

		}

		yield return null;

		if (patchCheck) {
			patchPoints = TraceThePatch(startElement,EndElement);
		}

		movement.Finished (patchPoints, patchCheck,0);
	}
		

	void OnDrawGizmos(){
		if (fillAStart) {
			for (int i = 0; i < openListDraw.Count; i ++) {
				Gizmos.color = Color.magenta;
				Gizmos.DrawCube (openListDraw [i].Location, Vector3.one);
			}
		}
	}
}
