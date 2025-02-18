using Unity.VisualScripting;
using UnityEngine;

public class AutonomousAgent : AiAgent
{
    [SerializeField] AutonomousAgent_data data;

    [Header("Perception")]
    public Perception seekPerception;
    public Perception fleePerception;
    public Perception flockPerception;
    public Perception obstaclePerception;

    float angle;

    [Range(5, 25)][SerializeField] int forceApplied = 5;



    // Update is called once per frame
    void Update()
    {
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

        // FlOCK
        if (flockPerception != null)
        {
            var gameObjects = flockPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                movement.ApplyForce(Cohesion(gameObjects) * data.cohesionWeight);
                movement.ApplyForce(Separation(gameObjects, data.separationRadius) * data.separationRadius);
                movement.ApplyForce(Alignment(gameObjects) * data.alignmentWeight);
            }
        }

        // OBSTACLE
        if (obstaclePerception != null)
        {
            if (obstaclePerception.CheckDirection(Vector3.forward))
            {
                Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 3, Color.red, 0.5f);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 3, Color.yellow, 0.5f);
            }
        }

        // WANDER - if not moving (seek/flee)
        if (movement.Acceleration.sqrMagnitude == 0)
        {
            Vector3 force = Wander();
            movement.ApplyForce(force);
        }

        Vector3 acceleration = movement.Acceleration;
        //acceleration.y = 0;
        movement.Acceleration = acceleration;

        if (movement.Direction.sqrMagnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement.Direction);
        }

        float size = 25;
        transform.position = Utilities.Wrap(transform.position, new Vector3(-size, -size, -size), new Vector3(size, size, size));

        //foreach (var gO in gameObjects)
        //{
        //    Debug.DrawLine(transform.position, gO.transform.position, Color.red);
        //}

        // OBSTACLE
        if (obstaclePerception != null && obstaclePerception.CheckDirection(Vector3.forward))
        {
            Vector3 direction = Vector3.zero;
            if (obstaclePerception.GetOpenDirection(ref direction))
            {
                Debug.DrawRay(transform.position, direction * 5, Color.red, 0.2f);
                movement.ApplyForce(GetSteeringForce(direction) * data.obstacleWeight);
            }
        }
    }

    private Vector3 Cohesion(GameObject[] neighbors)
    {
        Vector3 positions = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            positions += neighbor.transform.position;
        }

        Vector3 center = positions / neighbors.Length;
        Vector3 direction = center - transform.position;

        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 Separation(GameObject[] neighbors, float radius)
    {
        Vector3 separation = Vector3.zero;
        // accumulate vectors of the neighbors
        foreach (var neighbor in neighbors)
        {
            // get direction vector away from neighbor
            Vector3 direction = (transform.position - neighbor.transform.position);
            // check if within separation radius
            if (direction.magnitude < radius)
            {
                // scale separation vector inversely proportional to the direction distance
                // closer the distance the stronger the separataion
                separation += direction / direction.sqrMagnitude;
            }
        }

        // steer towards the separation point
        Vector3 force = GetSteeringForce(separation);
        return force;
    }

    private Vector3 Alignment(GameObject[] neighbors)
    {
        Vector3 velocities = Vector3.zero;
        // accumulate the velocity vectors of the neighbors
        foreach (var neighbor in neighbors)
        {
            // get the velocity from the agent movement
            velocities += neighbor.GetComponent<AiAgent>().movement.Velocity;
        }
        // get the average velocity of the neighbors
        Vector3 averageVelocity = velocities / neighbors.Length;

        // steer towards the average velocity
        Vector3 force = GetSteeringForce(averageVelocity);

        return force;
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
        Vector3 desired = direction.normalized * movement.data.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.data.maxForce);

        return force;
    }

    private Vector3 Wander()
    {
        // randomly adjust angle +/- displacement
        angle += Random.Range(-data.displacement, data.displacement);

        // create rotation quaternion around y-axis (up)
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);

        // calculate point on circle radius
        Vector3 point = rotation * (Vector3.forward * data.radius);

        // set point in front of agent
        Vector3 forward = movement.Direction * data.distance;

        // apply force towards point in front
        Vector3 force = GetSteeringForce(forward + point);

        return force;
    }
    
}
