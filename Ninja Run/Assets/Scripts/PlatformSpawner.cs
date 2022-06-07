using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // ������ ������ ������
    public int count = 3; // ������ ������ ���� ����

    private GameObject[] platforms; // �̸� ������ ���ǵ�
    private int currentIndex = 0; // ����� ���� ������ ����

    // �ʹݿ� ������ ������ ȭ�� �ۿ� ���ܵ� ��ġ
    private Vector2 firstPosition = new Vector2(0, -25);
    private float lastSpawnTime; // ������ ��ġ ����

    public float timeBetSpawnMin = 1.25f; // ���� ���� ��ġ �ð������� �ּڰ�
    public float timeBetSpawnMax = 1.25f; // ���� ���� ��ġ �ð������� �ִ�
    private float timeBetSpawn; // ���� ��ġ������ �ð� ����

    public float yMin = -2.9f; // ��ġ�� ��ġ�� �ּ� y ��
    public float yMax = 2f; // ��ġ�� ��ġ�� �ִ� y ��
    private float xPos = 20f; // ��ġ�� ��ġ�� x ��

    void Start()
    {
        // ������ �ʱ�ȭ�ϰ� ����� ������ �̸� ����
        platforms = new GameObject[count];

        // count��ŭ ����
        for (int i = 0; i < count; i++)
        {
            // platformPrefab�� �������� �� ������ firstPosition ��ġ�� �����Ͽ� ����
            // ������ ������ platform �迭�� �Ҵ�
            platforms[i] = Instantiate(platformPrefab, firstPosition, Quaternion.identity);
        }
        // ������ ��ġ ���� �ʱ�ȭ
        lastSpawnTime = 0f;
        // ������ ��ġ������ �ð� ������ 0���� �ʱ�ȭ
        timeBetSpawn = 0f;
    }

    void Update()
    {
        // ������ ���ư��� �ֱ������� ������ ��ġ
        // ���ӿ��� ���¿����� �������� ����
        if (GameManager.instance.isGameover)
        {
            return;
        }

        // ������ ��ġ �������� timeBetSpawn �̻� �ð��� �귶�ٸ�
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // ��ϵ� ������ ��ġ ������ ���� �������� ����
            lastSpawnTime = Time.time;

            // ���� ��ġ������ �ð� ������ timeBetSpawnMin, timeBetSpawnMax ���̿��� ���� ����
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            // ��ġ�� ��ġ�� ���̸� yMin�� yMax ���̿��� ���� ����
            float yPos = Random.Range(yMin, yMax);

            // ����� ���� ������ ���� ���� ������Ʈ�� ��Ȱ��ȭ�ϰ� ��� �ٽ� Ȱ��ȭ
            // �̶� ������ Platform ������Ʈ�� OnEnable �޼��尡 �����
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            // ���� ������ ������ ȭ�� �����ʿ� ���ġ
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            // ���� �ѱ��
            currentIndex++;

            // ������ ������ ���������� ���� ����
            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}
