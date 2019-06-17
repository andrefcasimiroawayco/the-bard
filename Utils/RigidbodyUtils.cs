using UnityEngine;

/// <summary>
//  A set of helper methods for common rigidbody functionalities
/// </summary>
public static class RigidbodyUtils
{

    /// <summary>
    /// Applies a direction to a movable rigidbody.
    /// Used for moving characters around the world.
    /// </summary>
    /// <param name="x">The horizontal / right input.</param>
    /// <param name="z">The vertical /forward input.</param>
    /// <param name="transform">This gameobject's transform.</param>
    /// <param name="rigidbody">This gameobject's rigidbody.</param>
    /// <param name="speed">The speed to apply to the direction.</param>
    /// <param name="maxSpeed">The maximum speed allowed after we reach maximum velocity.</param>
    public static void ApplyDirection(
        float x,
        float z,
        Transform transform,
        Rigidbody rigidbody,
        float speed = 2f,
        float maxSpeed = 3f
    )
    {
    if (x == 0 && z == 0) { return; }

    Vector3 direction = transform.right * x + transform.forward * z;
    direction *= speed;
    direction = Vector3.ClampMagnitude(direction, 1f);

    rigidbody.AddForce(direction, ForceMode.VelocityChange);

    if (rigidbody.velocity.magnitude > maxSpeed)
    {
        rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
    }
    }

    /// <summary>
    /// Checks if a rigidbody's velocity is less than zero
    /// And if true, keeps it at zero value.
    /// </summary>
    /// <param name="rigidbody">This gameobject's rigidbody.</param>
    public static void ClampNegativeVelocity(Rigidbody rigidbody)
    {
        float currentSpeed = rigidbody.velocity.magnitude;
        if (currentSpeed < 0f) { currentSpeed = 0f; }
    }
}
