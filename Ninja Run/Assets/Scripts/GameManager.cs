using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 게임 점수나 게임 오버, 사운드등을 관리하는 스크립트
// 씬에는 단 하나의 게임 매니점나 존재
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameover = false; // 게임 오버 상태
    public Text scoreText; // 점수를 출력할 UI
    public GameObject gameoverUI; // 게임 오버시 나올 UI

    private int score = 0;

    // 게임 시작과 동시에 싱글톤 구성
    private void Awake()
    {
        // 싱글톤 변수 instance가 비어있는지 확인
        if (instance == null)
        {
            // instance가 비어있다면 그곳에 자신을 할당
            instance = this;
        }
        // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
        else
        {
            // 씬에 두개 이상의 GameManger 오브젝트가 존재한다는 의미
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("두개 이상의 게임 매니저가 존재");
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gameoverUI.transform.localScale = Vector2.zero; // 게임오버 UI의 크기를 0으로 바꿈
    }

    void Update()
    {
        // 게임 오버 상태에서 재시작 할 수 있게 하는 처리
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            // 게임 오버 상태에서 클릭하면 현재 씬을 재시작
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    // 점수 증가 함수
    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            scoreText.text = "SCORE : " + score;
        }
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        isGameover = true; // 게임오버를 true로 변경
        gameoverUI.SetActive(true); // 게임오버 UI를 true로 변경
        LeanTween.scale(gameoverUI, Vector2.one, 0.8f); // 게임오버 UI를 원래 크기로 0.8초안에 되돌림
    }
}
