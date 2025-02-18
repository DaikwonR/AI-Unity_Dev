using UnityEngine;

public class AI_idle_state : AI_state
{
    float timer;

    public AI_idle_state(State_agent agent) : base(agent)
    {
        //
    }

    public override void OnEnter()
    {
        timer = Random.Range(1, 3);
        agent.movement.Stop();
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            agent.StateMachine.SetState(nameof(AI_patrol_state));
        }
    }

    public override void OnExit()
    {
        
    }
    
}
