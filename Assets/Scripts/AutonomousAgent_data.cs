using UnityEngine;

[CreateAssetMenu(fileName = "AutonomousAgent_data", menuName = "Data/AutonomousAgent_data")]
public class AutonomousAgent_data : ScriptableObject
{
    [Range(0, 180)] public float displacement;
    [Range(0, 10)] public float distance;
    [Range(0, 10)] public float radius;
           
    [Range(0, 5)] public float cohesionWeight;

    [Range(0, 5)] public float separationWeight;
    [Range(0, 5)] public float separationRadius;

    [Range(0, 5)] public float alignmentWeight;
}
