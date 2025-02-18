using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Navmesh_movement : Movement
{
    [SerializeField] NavMeshAgent navMeshAgent;



    public override Vector3 Velocity 
    { 
        get => navMeshAgent.velocity; 
        set => navMeshAgent.velocity = value; 
    }

    public override Vector3 Destination 
    { 
        get => base.Destination; 
        set => base.Destination = value; 
    }

    public void Update()
    {
        navMeshAgent.speed = data.maxSpeed;
        navMeshAgent.acceleration = data.maxForce;
        navMeshAgent.angularSpeed = data.turnRate;


        //print("v:" + navMeshAgent.velocity);
        //print("s:" + navMeshAgent.speed);
        //print("stopped:" + navMeshAgent.isStopped);

        //navMeshAgent.isStopped = false;
        Debug.Log(navMeshAgent.acceleration);
        Debug.DrawLine(transform.position, Destination);
    }

    public override void ApplyForce(Vector3 force)
    {
        //
    }

    public override void MoveTowards(Vector3 position)
    {
        Destination = position;
    }

    public override void Resume()
    {
        navMeshAgent.isStopped = false;
    }

    public void Reset()
    {
        navMeshAgent.isStopped = true;
    }

}
