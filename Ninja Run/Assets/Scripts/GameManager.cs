using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���� ������ ���� ����, ������� �����ϴ� ��ũ��Ʈ
// ������ �� �ϳ��� ���� �Ŵ����� ����
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱����� �Ҵ��� ���� ����

    public bool isGameover = false; // ���� ���� ����
    public Text scoreText; // ������ ����� UI
    public GameObject gameoverUI; // ���� ������ ���� UI

    private int score = 0;

    // ���� ���۰� ���ÿ� �̱��� ����
    private void Awake()
    {
        // �̱��� ���� instance�� ����ִ��� Ȯ��
        if (instance == null)
        {
            // instance�� ����ִٸ� �װ��� �ڽ��� �Ҵ�
            instance = this;
        }
        // instance�� �̹� �ٸ� GameManager ������Ʈ�� �Ҵ�Ǿ� �ִ� ���
        else
        {
            // ���� �ΰ� �̻��� GameManger ������Ʈ�� �����Ѵٴ� �ǹ�
            // �̱��� ������Ʈ�� �ϳ��� �����ؾ� �ϹǷ� �ڽ��� ���� ������Ʈ�� �ı�
            Debug.LogWarning("�ΰ� �̻��� ���� �Ŵ����� ����");
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gameoverUI.transform.localScale = Vector2.zero; // ���ӿ��� UI�� ũ�⸦ 0���� �ٲ�
    }

    void Update()
    {
        // ���� ���� ���¿��� ����� �� �� �ְ� �ϴ� ó��
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            // ���� ���� ���¿��� Ŭ���ϸ� ���� ���� �����
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    // ���� ���� �Լ�
    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            scoreText.text = "SCORE : " + score;
        }
    }

    // �÷��̾� ĳ���Ͱ� ����� ���� ������ �����ϴ� �޼���
    public void OnPlayerDead()
    {
        isGameover = true; // ���ӿ����� true�� ����
        gameoverUI.SetActive(true); // ���ӿ��� UI�� true�� ����
        LeanTween.scale(gameoverUI, Vector2.one, 0.8f); // ���ӿ��� UI�� ���� ũ��� 0.8�ʾȿ� �ǵ���
    }
}
