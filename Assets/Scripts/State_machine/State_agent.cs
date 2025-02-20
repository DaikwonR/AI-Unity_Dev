using UnityEngine;

public class State_agent : AiAgent
{
    [SerializeField] Perception perception;

    public State_machine StateMachine = new State_machine();

    private void Start()
    {
        StateMachine.AddState(nameof(AI_idle_state), new AI_idle_state(this));
        StateMachine.AddState(nameof(AI_patrol_state), new AI_patrol_state(this));

        StateMachine.SetState(nameof(AI_idle_state));
    }

    public void Update()
    {
        StateMachine.Update();

        //// SEEK
        //if (perception != null)
        //{
        //    var gameObjects = perception.GetGameObjects();
        //    if (gameObjects.Length > 0)
        //    {
        //        movement.Destination = gameObjects[0].transform.position;
        //    }

        //}
    }

    private void OnGUI()
    {
        // draw label of current state above agent
        GUI.backgroundColor = Color.black;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        Rect rect = new Rect(0, 0, 100, 20);
        // get point above agent
        Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
        rect.x = point.x - (rect.width / 2);
        rect.y = Screen.height - point.y - rect.height - 20;
        // draw label with current state name
        GUI.Label(rect, StateMachine.CurrentState.Name);
    }
}
