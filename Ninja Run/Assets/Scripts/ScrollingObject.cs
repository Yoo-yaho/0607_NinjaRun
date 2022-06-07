using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������Ʈ�� ��� �������� �����̴� ��ũ��Ʈ
public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f; // �̵� �ӵ�

    private void Update()
    {
        // ���� ������Ʈ�� �������� ���� �ӵ��� ���� �̵��ϴ� ó��
        if (!GameManager.instance.isGameover)
        {
            speed += 0.1f * Time.deltaTime;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

    }
}
