using UnityEngine;

[CreateAssetMenu(fileName = "AutonomousAgent_data", menuName = "Data/AutonomousAgent_data")]
public class AutonomousAgent_data : ScriptableObject
{
    [Range(0, 180)] public float displacement;
    [Range(0, 200)] public float distance;
    [Range(0, 200)] public float radius;
              
    [Range(0, 20)] public float cohesionWeight;
              
    [Range(0, 20)] public float separationWeight;
    [Range(0, 20)] public float separationRadius;
              
    [Range(0, 20)] public float alignmentWeight;
              
    [Range(0, 20)] public float obstacleWeight;
}
