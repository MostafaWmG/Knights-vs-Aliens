using UnityEngine;
using System.Collections;
/// <summary>
/// Elements of the plane .
/// </summary>
public class Element : IHeap<Element> {
	
	private bool canWalk;
	private Vector3 location;
	private int x;
	private int y;

	private int connectionCost;
	private int huresticCost;
	private Element parent;
	private int heapIndex;

	/// <summary>
	/// Initializes a new instance of the <see cref="Element"/> class.
	/// </summary>
	/// <param name="canWalk">If set to <c>true</c> can walk.</param>
	/// <param name="location">Location.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public Element(bool canWalk, Vector3 location, int x, int y) {
		this.canWalk = canWalk;
		this.location = location;
		this.x =  x;
		this.y = y;
	}
		
	/// <summary>
	/// Compares to.
	/// </summary>
	/// <returns>The to.</returns>
	/// <param name="nodeToCompare">Node to compare.</param>
	public int CompareTo(Element nodeToCompare) {
		int compare = FinalCost.CompareTo(nodeToCompare.FinalCost);
		if (compare == 0) {
			compare = huresticCost.CompareTo(nodeToCompare.huresticCost);
		}
		return -compare;
	}

	/// <summary>
	/// Gets the final cost.
	/// </summary>
	/// <value>The final cost.</value>
	public int FinalCost {
		get {
			return connectionCost + huresticCost;
		}
	}

	/// <summary>
	/// Gets or sets the index of the heap.
	/// </summary>
	/// <value>The index of the heap.</value>
	public int HeapIndex {
		get {
			return heapIndex;
		}
		set {
			heapIndex = value;
		}
	}

	/// <summary>
	/// Gets or sets a value indicating whether this instance can walk.
	/// </summary>
	/// <value><c>true</c> if this instance can walk; otherwise, <c>false</c>.</value>
	public bool CanWalk {
		get {
			return canWalk;
		}
		set {
			canWalk = value;
		}
	}

	/// <summary>
	/// Gets or sets the location.
	/// </summary>
	/// <value>The location.</value>
	public Vector3 Location {
		get {
			return location;
		}
		set {
			location = value;
		}
	}

	/// <summary>
	/// Gets or sets the x.
	/// </summary>
	/// <value>The x.</value>
	public int X {
		get {
			return x;
		}
		set {
			x = value;
		}
	}

	/// <summary>
	/// Gets or sets the y.
	/// </summary>
	/// <value>The y.</value>
	public int Y {
		get {
			return y;
		}
		set {
			y = value;
		}
	}

	/// <summary>
	/// Gets or sets the connection cost.
	/// </summary>
	/// <value>The connection cost.</value>
	public int ConnectionCost {
		get {
			return connectionCost;
		}
		set {
			connectionCost = value;
		}
	}

	/// <summary>
	/// Gets or sets the hurestic cost.
	/// </summary>
	/// <value>The hurestic cost.</value>
	public int HuresticCost {
		get {
			return huresticCost;
		}
		set {
			huresticCost = value;
		}
	}

	/// <summary>
	/// Gets or sets the parent.
	/// </summary>
	/// <value>The parent.</value>
	public Element Parent {
		get {
			return parent;
		}
		set {
			parent = value;
		}
	}
}
