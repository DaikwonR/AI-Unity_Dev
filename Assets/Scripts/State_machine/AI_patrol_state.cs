using UnityEngine;

public class AI_patrol_state : AI_state
{
    Vector3 destination = Vector3.zero;

    public AI_patrol_state(State_agent agent) : base(agent)
    {
        
    }

    public override void OnEnter()
    {
        destination = Nav_node.GetRandomNode().transform.position;
        agent.movement.Destination = destination;
        agent.movement.Resume();
    }

    public override void OnUpdate()
    {
        Vector3 direction = (agent.transform.position - destination);
        direction.y = 0;
        float distance = direction.magnitude;
        if (distance <= .25f)
        {
            agent.StateMachine.SetState(nameof(AI_idle_state));
        }

        //if (agent.perception != null)
        //{
        //    var gameObjects = perception.GetGameObjects();
        //    if (gameObjects.Length > 0)
        //    {
        //        movement.Destination = gameObjects[0].transform.position;
        //    }

        //}
    }

    public override void OnExit()
    {
        
    }
    
}
