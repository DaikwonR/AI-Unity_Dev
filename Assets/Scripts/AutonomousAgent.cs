using UnityEngine;

public class AutonomousAgent : AiAgent
{
    [SerializeField] Perception perception;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * perception.maxDistance, Color.green);
        var gameObjects = perception.GetGameObjects();
        foreach (var gO in gameObjects)
        {
            Debug.DrawLine(transform.position, gO.transform.position, Color.red);
        }
    }
}
