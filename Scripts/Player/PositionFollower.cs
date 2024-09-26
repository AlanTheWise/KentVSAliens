using UnityEngine;

public class PositionFollower : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        //transform.position = targetTransform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetTransform.position + offset, ref velocity, 0.2f);
    }
}
