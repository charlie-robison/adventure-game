using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    public float DistanceFromPlayer(GameObject player, GameObject other)
    {
        float distanceX = (player.transform.position.x - other.transform.position.x) * (player.transform.position.x - other.transform.position.x);
        float distanceY = (player.transform.position.y - other.transform.position.y) * (player.transform.position.y - other.transform.position.y);
        float distance = Mathf.Sqrt(distanceX + distanceY);

        return distance;
    }

    public Quaternion RotateFacingPlayer(GameObject player, GameObject other, float turnSmoothTime, float turnSmoothVelocity)
    {
        // Gets the angle of the vector from the enemy to the player in degrees.
        float angle = Mathf.Atan2((player.transform.position.x - other.transform.position.x), (player.transform.position.z - other.transform.position.z)) * Mathf.Rad2Deg;
        float smoothAngle = Mathf.SmoothDampAngle(other.transform.eulerAngles.y, angle, ref turnSmoothVelocity, turnSmoothTime);
        return Quaternion.Euler(0f, smoothAngle, 0f);
    }
}
