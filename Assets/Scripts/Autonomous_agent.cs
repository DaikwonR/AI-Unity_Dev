using UnityEngine;

public class AutonomousAgent : AiAgent
{
    [SerializeField] AutonomousAgent_data data;

    [SerializeField] Perception perception;
    [Range(5, 25)][SerializeField] int forceApplied = 5;



    // Update is called once per frame
    void Update()
    {
        movement.ApplyForce(Vector3.forward * forceApplied);
        transform.position = Utilities.Wrap(transform.position, new Vector3(-5, -5, -5), new Vector3(5, 5, 5));

        Debug.DrawRay(transform.position, transform.forward * perception.maxDistance, Color.green);
        var gameObjects = perception.GetGameObjects();
        foreach (var gO in gameObjects)
        {
            Debug.DrawLine(transform.position, gO.transform.position, Color.red);
        }
    }
}
