using Unity.VisualScripting;
using UnityEngine;

public class AutonomousAgent : AiAgent
{
    [SerializeField] AutonomousAgent_data data;

    [Header("Wander")]
    [Range(0, 100)][SerializeField] float displacement;
    [Range(0, 100)][SerializeField] float distance;
    [Range(0, 100)][SerializeField] float radius;

    [Header("Perception")]
    [SerializeField] Perception seekPerception;
    [SerializeField] Perception fleePerception;

    float angle;

    [Range(5, 25)][SerializeField] int forceApplied = 5;



    // Update is called once per frame
    void Update()
    {
        //movement.ApplyForce(Vector3.forward * forceApplied);
        float size = 25;
        transform.position = Utilities.Wrap(transform.position, new Vector3(-size, -size, -size), new Vector3(size, size, size));

        //Debug.DrawRay(transform.position, transform.forward * seekPerception.maxDistance, Color.green);

        // SEEK
        if (seekPerception != null)
        {
            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }

        }

        // FLEE
        if (fleePerception != null)
        {
            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Flee(gameObjects[0]);
                movement.ApplyForce(force);
            }

        }

        // WANDER - if not moving (seek/flee)
        if (movement.Acceleration.sqrMagnitude == 0)
        {
            // randomly adjust angle +/- displacement
            angle += Random.Range(-displacement, displacement);

            // create rotation quaternion around y-axis (up)
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);

            // calculate point on circle radius
            Vector3 point = rotation * (Vector3.forward * radius);

            // set point in front of agent
            Vector3 forward = movement.Direction * distance;

            // apply force towards point in front
            Vector3 force = GetSteeringForce(forward + point);

            movement.ApplyForce(force);
        }

        Vector3 acceleration = movement.Acceleration;
        acceleration.y = 0;
        movement.Acceleration = acceleration;

        //foreach (var gO in gameObjects)
        //{
        //    Debug.DrawLine(transform.position, gO.transform.position, Color.red);
        //}
    }

    private Vector3 Seek(GameObject go)
    {
        Vector3 direction = go.transform.position - transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 Flee(GameObject go)
    {
        Vector3 direction = transform.position - go.transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 GetSteeringForce(Vector3 direction)
    {
        Vector3 desired = direction.normalized * movement.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.maxForce);

        return force;
    }
}
