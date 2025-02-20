using UnityEngine;

[CreateAssetMenu(fileName = "Movement_data", menuName = "Data/Movement_data")]
public class Movement_data : ScriptableObject
{
    [Range(1, 50)] public float minSpeed = 5;
    [Range(1, 50)] public float maxSpeed = 10;
    [Range(1, 50)] public float maxForce = 5;
    [Range(1, 50)] public float turnRate = 5;
}
