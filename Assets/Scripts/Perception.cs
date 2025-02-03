using UnityEngine;

public abstract class Perception : MonoBehaviour
{

    public string tagName;
    [Multiline]public string description;

    [Range(1, 10)] public float maxDistance;
    [Range(0, 360)] public float maxAngle;

    public LayerMask layerMask = Physics.AllLayers;

    public abstract GameObject[] GetGameObjects();
    public bool CheckDirection(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, transform.rotation * direction);

        return Physics.Raycast(ray, maxDistance, layerMask);
    }

    public virtual bool GetOpenDirection(ref Vector3 openDirection)
    {
        return false;
    }
}
