using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    public Transform center;
    public bool isDirectionFrom = true;

    // Use this for initialization
    public Vector3 GetDirection(Vector3 anchor)
    {
        if (center != null)
        {
            if (isDirectionFrom)
            {
                return anchor - center.position;
            }
            else
            {
                return center.position - anchor;
            }
        }

        if (isDirectionFrom)
        {
            var direction = transform.up * -1.0f;
            return direction;
        }
        else
        {
            return transform.up;
        }
    }
}