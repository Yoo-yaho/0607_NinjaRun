using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float smoothTime;
    public GameObject player; // 카메라가 따라갈 대상
    public Vector2 velocity; // 카메라가 따라갈 속도
    public Vector2 minPos, maxPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        // Mathf.SmoothDamp는 천천히 값을 증가시키는 메서드
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y,
            ref velocity.y, smoothTime);

        // 카메라로 이동
        transform.position = new Vector3(0, posY, transform.position.z);

        if (gameObject != null)
        { // Mathf.Clamp(현재값, 최대값, 최소값)
          // 현재값이 최대값까지만 반환해주고 최소값보다 작으면 그 최소값까지만 반환함.
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x),
                Mathf.Clamp(transform.position.y, minPos.y, maxPos.y),
                Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z)
                );
        }
    }
}
