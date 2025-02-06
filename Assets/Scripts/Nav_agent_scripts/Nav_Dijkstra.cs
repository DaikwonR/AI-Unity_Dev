using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class Nav_Dijkstra
{
   public static bool Generate(Nav_node startNode, Nav_node endNode, ref List<Nav_node> path)
    {
        var nodes = new SimplePriorityQueue<Nav_node>();

        startNode.Cost = 0;
        nodes.EnqueueWithoutDuplicates(startNode, startNode.Cost);

        bool found = false;

        while (nodes.Count > 0 && !found)
        {
            var currentNode = nodes.Dequeue();
            if (currentNode == endNode)
            {
                found = true;
                break;
            }
            foreach (var neighbor in currentNode.neighbors)
            {
                float cost = currentNode.Cost + (currentNode.transform.position - neighbor.transform.position).magnitude;
                if (cost < neighbor.Cost)
                {
                    neighbor.Cost = cost;
                    neighbor.Previous = currentNode;

                    nodes.EnqueueWithoutDuplicates(neighbor, cost);
                }
            }
        }

        if (found)
        {
            Nav_node.CreatePath(endNode, ref path);
        }

        return found;
    }
    
}
