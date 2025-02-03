using UnityEngine;

public class Nav_agent : AiAgent
{
    public Waypoint location { get; set; }

    public void Start()
    {
        var locations = GameObject.FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
        if (locations.Length > 0)
        {
            location = locations[Random.Range(0, locations.Length)];
        }
    }

    public void Update()
    {
        if (location != null)
        {
            movement.MoveTowards(location.transform.position);
        }

        transform.forward = movement.Direction;
    }
}
