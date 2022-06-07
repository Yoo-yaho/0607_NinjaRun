using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player 게임 오브젝트를 제어하는 스크립트.
public class PlayerControl : MonoBehaviour
{
    public float jumpForce = 700f; // 점프 힘
    public AudioClip death;
    public AudioClip jump;
    public AudioClip doubleJump;

    private int jumpCount = 0; // 누적 점프 횟수
    private bool isGrounded = false; // 바닥에 닿았는지 나타냄
    private bool isDead = false; // 캐릭터의 사망 여부
    private bool isDouble = false; // 캐릭터 더블 점프 여부

    private Rigidbody2D playerRigidbody; // 리지드바디 컴포넌트
    private Animator animator; // 애니메이터 컴포넌트
    private AudioSource playerAudio; // 오디오소스 컴포넌트
   
    void Start()
    {
        // 변수 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 사망 시 처리를 더 이상 진행하지 않고 종료
        if (isDead)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            // 점프 할 때마다 카운트를 추가
            jumpCount++;
            // 점프 직전에 속도를 (0, 0)으로 변경
            playerRigidbody.velocity = Vector2.zero;
            // 리지드 바디 위쪽으로 힘을 주기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));


            PlaySound("Jump");
        }
        else if (Input.GetMouseButton(0) && jumpCount == 2)
        {
            PlaySound("DoubleJump");
            animator.SetBool("Double", !isDouble);

            
        }   
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // 버튼에서 손을 떼고 y값의 속도가 0보다 크다면
            // 현재 속도를 0.8로 변경
            playerRigidbody.velocity = playerRigidbody.velocity * 0.8f;
        }
        else if (Input.GetMouseButtonDown(1) /*&& !animator.GetCurrentAnimatorClipInfo(0).IsName("Attack")*/)
        {
            animator.SetTrigger("Attack");
            //Invoke("ResetAttack", 0.5f);
        }

        animator.SetBool("Grounded", isGrounded);
    }

    private void ResetAttack()
    {
        animator.ResetTrigger("Attack");
    }

    private void PlaySound(string action)
    {
        switch (action)
        {
            case "Jump":
                playerAudio.clip = jump;
                break;
            case "DoubleJump":
                playerAudio.clip = doubleJump;
                break;
            case "Death":
                playerAudio.clip = death;
                break;
        }

        playerAudio.Play();
    }

    private void Die()
    {
        //animator.SetTrigger("Die");
        // 속도를 제로(0, 0)로 변경
        playerRigidbody.velocity = Vector2.zero;
        // 사망 상태를 true로 변경
        isDead = true;

        //playerAudio.clip = death;
        //playerAudio.Play();

        GameManager.instance.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 떨어져서 데드 존에 닿았음을 감지
        if (collision.tag == "DeadZone" && !isDead)
        {
            Die();
        }
        // 캐릭터에 있는 BoxCollider가 Platform Collider에 충돌하면
        // 캐릭터에 있는 CircleCollider가 True로 체크
        if (collision.tag == "Platform")
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 충돌이 끝나면 CircleCollider는 false로 바뀜
        GetComponent<CircleCollider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았음을 감지
        if (collision.contacts[0].normal.y > 0.7f)
        {
            // isGrounded를 true로 변경, isDouble은 false로 변경하고 점프 횟수를 0으로 리셋
            isGrounded = true;
            animator.SetBool("Double", isDouble);
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
    }
}
