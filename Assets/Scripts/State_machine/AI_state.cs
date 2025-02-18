using UnityEngine;

public abstract class AI_state
{
    protected State_agent agent;

    public AI_state(State_agent agent)
    {
        this.agent = agent;
    }

    public string Name => GetType().Name;

    public abstract void OnEnter();

    public abstract void OnUpdate();

    public abstract void OnExit();
}
