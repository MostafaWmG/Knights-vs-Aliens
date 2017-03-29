using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Grid.
/// split our plane object into grid squres with rigard to nodeSize( = nodeSide * 2 ).
/// </summary>
public class Grid : MonoBehaviour {
	/// <summary>
	/// show the patch in screen.
	/// </summary>
	public bool showGrid;

	/// <summary>
	/// what is the layer name of the obstacles located in plane.
	/// </summary>
	[SerializeField]
	private LayerMask obstacle;
	[SerializeField]
	private Vector2 size;
	[SerializeField]
	private float nodeSide;

	private Element[,] grid;
	private float nodeSize;
	private int gridSizeX, gridSizeY;

	public GameObject prefabGround;

	void Awake() {
		nodeSize = nodeSide * 2;
		gridSizeX = Mathf.RoundToInt(size.x/nodeSize);
		gridSizeY = Mathf.RoundToInt(size.y/nodeSize);
		Create();
	}

	/// <summary>
	/// Gets the neighbours.
	/// </summary>
	/// <returns>The neighbours.</returns>
	/// <param name="node">Node.</param>
	public List<Element> GetNeighbours(Element node) {
		List<Element> neighbours = new List<Element>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;
				
				int xBoundary = node.X + x;
				int YBoundary = node.Y + y;

				if ((xBoundary >= 0 && xBoundary < gridSizeX) && (YBoundary >= 0 && YBoundary < gridSizeY)) {
					neighbours.Add(grid[xBoundary,YBoundary]);
				}
			}
		}

		return neighbours;
	}
		
	/// <summary>
	/// Create the grid.
	/// </summary>
	public IEnumerator UpdateGrid(Element mousPosNode) {
		bool canWalk;
//		Debug.Log ("enterd update");
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
//				if (x == 0 && y == 0)
//					continue;
//				Debug.Log ("enterd update loop");	
				int xBoundary = mousPosNode.X + x;
				int YBoundary = mousPosNode.Y + y;

				if ((xBoundary >= 0 && xBoundary < gridSizeX) && (YBoundary >= 0 && YBoundary < gridSizeY)) {
					if (Physics.CheckSphere (grid[xBoundary,YBoundary].Location, nodeSide, obstacle)) {
//						Debug.Log ("update cant walk");
						canWalk = false;
					} else {
//						Debug.Log ("update can walk");
						canWalk = true;
					}

					grid [xBoundary, YBoundary].CanWalk = canWalk;
				}
			}
			yield return null;
		}
	}

	/// <summary>
	/// Create the grid.
	/// </summary>
	private void Create() {
		bool canWalk;
		Vector3 downLeft = transform.position - (Vector3.right * size.x/2) - (Vector3.forward * size.y/2);
		grid = new Element[gridSizeX,gridSizeY];

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				Vector3 location = downLeft + Vector3.right * (x * nodeSize + nodeSide) + Vector3.forward * (y * nodeSize + nodeSide);

				if (Physics.CheckSphere (location, nodeSide, obstacle)) {
					canWalk = false;
				} else {
					canWalk = true;
				}

				grid[x,y] = new Element(canWalk,location, x,y);
//				Instantiate (prefabGround, location, Quaternion.identity);
			}
		}
	}

	/// <summary>
	/// Gets the node from location.
	/// </summary>
	/// <returns>The node </returns>
	/// <param name="location">Location of node in world cordinate.</param>
	public Element getNodeFromLocation(Vector3 location) {
		float percentX = (location.x + size.x/2) / size.x;
		float percentY = (location.z + size.y/2) / size.y;

		// we are clamping in case if we go out of plane we dont get errors.
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y];
	}

	/// <summary>
	/// Raises the draw gizmos event.
	/// it is for showing the patch in the plane.
	/// the patch color is black.
	/// </summary>
	void OnDrawGizmos() {

		if (showGrid) {
			
//			Gizmos.DrawWireCube(transform.position,new Vector3(size.x,1,size.y));

			if (grid != null) {
				foreach (Element n in grid) {
					Gizmos.color = (n.CanWalk)?Color.white:Color.red;
					Gizmos.DrawCube(n.Location, Vector3.one * (nodeSize-.1f));
				}
			}
		}
	}

	/// <summary>
	/// Gets the obstacle.
	/// </summary>
	/// <value>The obstacle.</value>
	public LayerMask Obstacle {
		get {
			return obstacle;
		}
	}

	/// <summary>
	/// Gets the size.
	/// </summary>
	/// <value>The size.</value>
	public Vector2 Size {
		get {
			return size;
		}
	}

	/// <summary>
	/// Gets the node side.
	/// </summary>
	/// <value>The node side.</value>
	public float NodeSide {
		get {
			return nodeSide;
		}
	}
		
	/// <summary>
	/// Gets the maximum size of grid .
	/// </summary>
	/// <value>The max size.</value>
	public int GirdMax {
		get {
			return gridSizeX * gridSizeY;
		}
	}
}