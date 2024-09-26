using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    public Vector3 bulletDir;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, bulletDir, bulletSpeed * Time.deltaTime);

        if (transform.position == bulletDir) Destroy(gameObject);
    }
}
