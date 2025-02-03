using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPerception : Perception
{
    [SerializeField] int numRaycast = 2;

    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        // get array of direction using Utilities.GetDirectionsInCircle(numRaycast, maxAngle)
        Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);

        //iterate through directions array
        foreach (var direction in directions)
        {
            // create ray from transform position in the direction of (transform.roatation * direction)
            Ray ray = new Ray(transform.position, transform.rotation * direction);

            // raycast ray
            if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, layerMask))
            {
                // check if collision is self, skip if so
                if (raycastHit.collider.gameObject == this.gameObject) continue;

                // check tag, skip if tagName != "" and !CompareTag
                if (tagName != "" && !raycastHit.collider.CompareTag(tagName)) continue;

                result.Add(raycastHit.collider.gameObject);
            }
        }

        return result.ToArray();
    }

    public override bool GetOpenDirection(ref Vector3 openDirection)
    {
        // get array of directions using Utilities.GetDirectionsInCircle
        Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);

        // iterate through directions
        foreach (var direction in directions)
        {
            // cast ray from transform position in the dircetion of (transform.roatation * direction)
            Ray ray = new Ray(transform.position, transform.rotation * direction);

            // if there is NO raycast hit then that is an open direction
            if (!Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, layerMask))
            {
                // set open direction
                return true;
            }
        }
        return false;
    }
}
