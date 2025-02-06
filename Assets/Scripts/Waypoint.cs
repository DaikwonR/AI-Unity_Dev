using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Waypoint[] locations;


    private void OnTriggerEnter(Collider other)
    {
        print("hit");
        if (other.gameObject.TryGetComponent<Nav_agent>(out Nav_agent agent))
        {
            print("next");
            //agent.location = locations[Random.Range(0, locations.Length)];
        }
    }
}
