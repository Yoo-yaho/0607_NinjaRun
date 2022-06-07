using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player ���� ������Ʈ�� �����ϴ� ��ũ��Ʈ.
public class PlayerControl : MonoBehaviour
{
    public float jumpForce = 700f; // ���� ��
    public AudioClip death;
    public AudioClip jump;
    public AudioClip doubleJump;

    private int jumpCount = 0; // ���� ���� Ƚ��
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ��Ÿ��
    private bool isDead = false; // ĳ������ ��� ����
    private bool isDouble = false; // ĳ���� ���� ���� ����

    private Rigidbody2D playerRigidbody; // ������ٵ� ������Ʈ
    private Animator animator; // �ִϸ����� ������Ʈ
    private AudioSource playerAudio; // ������ҽ� ������Ʈ
   
    void Start()
    {
        // ���� �ʱ�ȭ
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ��� �� ó���� �� �̻� �������� �ʰ� ����
        if (isDead)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            // ���� �� ������ ī��Ʈ�� �߰�
            jumpCount++;
            // ���� ������ �ӵ��� (0, 0)���� ����
            playerRigidbody.velocity = Vector2.zero;
            // ������ �ٵ� �������� ���� �ֱ�
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
            // ��ư���� ���� ���� y���� �ӵ��� 0���� ũ�ٸ�
            // ���� �ӵ��� 0.8�� ����
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
        // �ӵ��� ����(0, 0)�� ����
        playerRigidbody.velocity = Vector2.zero;
        // ��� ���¸� true�� ����
        isDead = true;

        //playerAudio.clip = death;
        //playerAudio.Play();

        GameManager.instance.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �������� ���� ���� ������� ����
        if (collision.tag == "DeadZone" && !isDead)
        {
            Die();
        }
        // ĳ���Ϳ� �ִ� BoxCollider�� Platform Collider�� �浹�ϸ�
        // ĳ���Ϳ� �ִ� CircleCollider�� True�� üũ
        if (collision.tag == "Platform")
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �浹�� ������ CircleCollider�� false�� �ٲ�
        GetComponent<CircleCollider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ������� ����
        if (collision.contacts[0].normal.y > 0.7f)
        {
            // isGrounded�� true�� ����, isDouble�� false�� �����ϰ� ���� Ƚ���� 0���� ����
            isGrounded = true;
            animator.SetBool("Double", isDouble);
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �ٴڿ��� ������� �����ϴ� ó��
        isGrounded = false;
    }
}
