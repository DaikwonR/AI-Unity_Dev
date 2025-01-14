using UnityEngine;

public abstract class Perception : MonoBehaviour
{
    public string tagName;
    [Range(1, 10)] public float maxDistance;
    [Range(0, 360)] public float maxAngle;

    public abstract GameObject[] GetGameObjects();
}
