using System.Collections.Generic;
using UnityEngine;

public class State_machine
{
    public Dictionary<string, AI_state> states = new Dictionary<string, AI_state>();

    public AI_state CurrentState { get; private set; } = null;

    public void Update()
    {
        CurrentState?.OnUpdate();

        
    }

    public void AddState(string name, AI_state state)
    {
        Debug.Assert(states.ContainsKey(name), $"State has already been applied; try again + {name}");
        states[name] = state;
    }

    public void SetState(string name)
    {
        Debug.Assert(states.ContainsKey(name), $"State has not yet been applied; continue + {name}");

        var newState = states[name];
        //newState == CurrentState ? return : (Action)null;
        if (newState == CurrentState) return;

        CurrentState?.OnExit();

        CurrentState = newState;

        CurrentState.OnEnter();
    }
}
