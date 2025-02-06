using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Nav_node : MonoBehaviour
{
    public List<Nav_node> neighbors = new List<Nav_node>();

    public float Cost { get; set; } = float.MaxValue;
    public Nav_node Previous { get; set; } = null;

    public Nav_node getRandomNeighbor()
    {
        return (neighbors.Count > 0) ? neighbors[Random.Range(0, neighbors.Count)] : null;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        print("hit");
        if (other.gameObject.TryGetComponent<Nav_path>(out Nav_path path))
        {
            if (path.TargetNode == this)
            {
                path.TargetNode = path.GetNextNavNode(this);
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        print("hit");
        if (other.gameObject.TryGetComponent<Nav_path>(out Nav_path path))
        {
            if (path.TargetNode == this)
            {
                print("next");
                path.TargetNode = path.GetNextNavNode(this);
            }
        }
    }

    #region Helpers
    public static Nav_node[] GetNodes()
    {
        return FindObjectsByType<Nav_node>(FindObjectsSortMode.None);
    }
    
    public static Nav_node[] GetNodes(string tag)
    {
        List<Nav_node> result = new List<Nav_node>();

        var gameObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var gO in gameObjects)
        {            
            if (gO.TryGetComponent<Nav_node>(out Nav_node navNode))
            {
                result.Add(navNode);
            }
        }

        return result.ToArray();
    }

    public static Nav_node GetRandomNode()
    {
        var navNodes = GetNodes();
        return (navNodes.Length == null) ? null : navNodes[Random.Range(0, navNodes.Length)];
    }

    /// <summary>
	/// Finds the nearest NavNode to a given position based on squared distance.
	/// </summary>
	public static Nav_node GetNearestNavNode(Vector3 position)
    {
        Nav_node nearestNode = null;
        float nearestDistance = float.MaxValue;

        var nodes = Nav_node.GetNodes();
        foreach (var node in nodes)
        {
            float distance = (position - node.transform.position).sqrMagnitude; // Use sqrMagnitude for efficiency
            if (distance < nearestDistance)
            {
                nearestNode = node;
                nearestDistance = distance;
            }
        }

        return nearestNode;
    }

    /// <summary>
    /// Reconstructs the path from the given node back to the start node using the Previous references.
    /// </summary>
    public static void CreatePath(Nav_node node, ref List<Nav_node> path)
    {
        // Traverse backward through the previous nodes to reconstruct the shortest path
        while (node != null)
        {
            path.Add(node); // Add current node to the path
            node = node.Previous; // Move to the previous node in the path
        }

        // Reverse the path to ensure it follows the correct order (start to destination)
        path.Reverse();
    }

    /// <summary>
    /// Resets all NavNodes, clearing pathfinding data (Cost and Previous references).
    /// </summary>
    public static void ResetNodes()
    {
        var nodes = GetNodes();
        foreach (var node in nodes)
        {
            node.Previous = null;
            node.Cost = float.MaxValue; // Reset cost to a high value (infinity equivalent)
        }
    }
    #endregion
}
