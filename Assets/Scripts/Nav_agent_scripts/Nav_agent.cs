// Ensure this component requires a NavPath component
using UnityEngine;

[RequireComponent(typeof(Nav_path))]
public class Nav_agent : AiAgent
{
    // Reference to the navigation path component
    private Nav_path path;

    private void Start()
    {
        // Get the NavPath component attached to this GameObject
        path = GetComponent<Nav_path>();

        // Find the nearest navigation node based on the agent's current position
        var startNode = Nav_node.GetNearestNavNode(transform.position);

        // If a valid navigation node is found, set the path's destination to that node
        if (startNode != null)
        {
            path.Destination = startNode.transform.position;
        }
    }

    private void Update()
    {
        // If the agent has a target destination, move towards it
        if (path.HasTarget)
        {
            movement.MoveTowards(path.Destination);
        }
        else
        {
            // If there is no target, assign a random navigation node as the destination
            Nav_node destinationNode = Nav_node.GetRandomNode();

            // Ensure the node is valid before setting it as the destination
            if (destinationNode != null)
            {
                path.Destination = destinationNode.transform.position;
            }
        }

        // Ensure the agent faces the direction of movement
        transform.forward = movement.Direction;
    }
}