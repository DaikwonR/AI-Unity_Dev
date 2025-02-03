using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Nav_node : MonoBehaviour
{
    public List<Nav_node> neighbors = new List<Nav_node>();

    public Nav_node getRandomNeighbor()
    {
        return (neighbors.Count > 0) ? neighbors[Random.Range(0, neighbors.Count)] : null;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("hit");
        if (other.gameObject.TryGetComponent<Nav_agent>(out Nav_agent agent))
        {
            print("next");
            //agent.location = locations[Random.Range(0, locations.Length)];
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
        return (navNodes.Length > 0) ? null : navNodes[Random.Range(0, navNodes.Length)];
    }
    #endregion
}
