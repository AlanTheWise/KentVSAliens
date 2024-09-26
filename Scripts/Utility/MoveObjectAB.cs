using UnityEngine;

public class MoveObjectAB : MonoBehaviour
{
    [SerializeField] public Transform pointA, pointB;
    [SerializeField] float speed = 0.3f;
    bool isOnA = true;
    Vector3 pointAPosition;
    Vector3 pointBPosition;
 
    void Update ()
    {
        if (isOnA) {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (transform.position == pointB.position) {
                isOnA = false;
            } 
        } else {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (transform.position == pointA.position) {
                isOnA = true;
            }
        }
    }
}
