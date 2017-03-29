using UnityEngine;
using System.Collections;

/// <summary>
/// Heap. use this data struct inorder to improve performance in patchfinding algorithm.
/// </summary>
public class HeapDs<T> where T : IHeap<T> {
	
	private T[] nodes;
	private int currentNodeCount;

	/// <summary>
	/// Sorts from top to down.
	/// </summary>
	/// <param name="node">Node.</param>
	private void SortTopDown(T node) {
		while (true) {
			int childIndexLeft = node.HeapIndex * 2 + 1;
			int childIndexRight = node.HeapIndex * 2 + 2;
			int swapIndex = 0;

			if (childIndexLeft < currentNodeCount) {
				swapIndex = childIndexLeft;

				if (childIndexRight < currentNodeCount) {
					if (nodes[childIndexLeft].CompareTo(nodes[childIndexRight]) < 0) {
						swapIndex = childIndexRight;
					}
				}

				if (node.CompareTo(nodes[swapIndex]) < 0) {
					Swap (node,nodes[swapIndex]);
				}
				else {
					return;
				}

			}
			else {
				return;
			}

		}
	}

	/// <summary>
	/// Sorts from down to top.
	/// </summary>
	/// <param name="node">Node.</param>
	void SortDownTop(T node) {
		int parentIndex = (node.HeapIndex-1)/2;

		while (true) {
			T parentItem = nodes[parentIndex];
			if (node.CompareTo(parentItem) > 0) {
				Swap (node,parentItem);
			}
			else {
				break;
			}

			parentIndex = (node.HeapIndex-1)/2;
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Heap`1"/> class.
	/// </summary>
	/// <param name="maxSize">Max size.</param>
	public HeapDs(int maxSize) {
		nodes = new T[maxSize];
	}

	/// <summary>
	/// Add the specified node.
	/// </summary>
	/// <param name="node">Node.</param>
	public void Add(T node) {
		node.HeapIndex = currentNodeCount;
		nodes[currentNodeCount] = node;
		SortDownTop(node);
		currentNodeCount++;
	}
		
	/// <summary>
	/// Updates the item.
	/// </summary>
	/// <param name="node">Node.</param>
	public void UpdateItem(T node) {
		SortDownTop(node);
	}

	/// <summary>
	/// Removes the first.
	/// </summary>
	/// <returns>The first.</returns>
	public T RemoveFirst() {
		T firstItem = nodes[0];
		currentNodeCount--;
		nodes[0] = nodes[currentNodeCount];
		nodes[0].HeapIndex = 0;
		SortTopDown(nodes[0]);
		return firstItem;
	}

	/// <summary>
	/// Contains the specified node.
	/// </summary>
	/// <param name="node">Node.</param>
	public bool Contains(T node) {
		return Equals(nodes[node.HeapIndex], node);
	}

	/// <summary>
	/// Swap the specified nodeA and nodeB.
	/// </summary>
	/// <param name="nodeA">Node a.</param>
	/// <param name="nodeB">Node b.</param>
	void Swap(T nodeA, T nodeB) {
		nodes[nodeA.HeapIndex] = nodeB;
		nodes[nodeB.HeapIndex] = nodeA;
		int itemAIndex = nodeA.HeapIndex;
		nodeA.HeapIndex = nodeB.HeapIndex;
		nodeB.HeapIndex = itemAIndex;
	}

	/// <summary>
	/// Gets the count.
	/// </summary>
	/// <value>The count.</value>
	public int Count {
		get {
			return currentNodeCount;
		}
	}

}


