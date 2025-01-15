using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [Range(1, 10)][SerializeField] protected float minSpeed = 5;
    [Range(11, 50)][SerializeField] protected float maxSpeed = 5;
    [Range(10, 50)][SerializeField] protected float maxForce = 5;

    public Vector3 Velocity { get; set; }
    public Vector3 Acceleration { get; set; }
    public Vector3 Direction { get {  return Velocity.normalized; } }

    public abstract void ApplyForce(Vector3 force);
}
