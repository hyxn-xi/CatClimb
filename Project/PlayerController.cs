using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                             // LoadScene 컴포넌트 사용 시 필요

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 680.0f;                                  // 점프 시 높이값
    float walkForce = 30.0f;                                   // 걸을 때의 값
    float maxWalkSpeed = 2.0f;                                 // 걷는 속도 최대값
    // float threshold = 0.2f;                                    // 스마트폰 기울기에 반응하는 변수

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
    }


    void Update()
    {   
        // 점프한다 -> y축의 값이 0 일때만 점프키가 반응한다
        // 스마트폰 기울기 반응 시 GetKeyDown 을 GetMouseButtonDown(0)으로 바꿔준다
        if ( Input.GetKeyDown(KeyCode.Space) && rigid2D.velocity.y == 0 )                  
        {
            animator.SetTrigger("JumpTrigger");                 // 걷기->점프 애니메이션 전환 시의 트리거 작동
            rigid2D.AddForce(transform.up * jumpForce);
        }

        int key = 0;                                            // 좌우이동
        // 스마트폰 기울기 반응 시 GetKey 아래를 Input.acceleration.x >(<) threshold 로 바꿔준다
        if ( Input.GetKey(KeyCode.RightArrow) ) key = 1;        // 우로 이동
        if ( Input.GetKey(KeyCode.LeftArrow) ) key = -1;        // 좌로 이동

        float speedx = Mathf.Abs(rigid2D.velocity.x);           // 플레이어의 속도

        if ( speedx < maxWalkSpeed )                            // 스피드 제한
        {
            rigid2D.AddForce( transform.right * key * walkForce );
        }

        if ( key != 0 )                                         // 움직이는 방향에 따라 플레이어 반전
        {
            transform.localScale = new Vector3( key, 1, 1);
        }

        if ( animator.velocity.y == 0 )                         // 플레이어 속도에 맞춰 애니메이션 속도 조절
        {
            animator.speed = speedx / 2.0f;
        }
        else
        {
            animator.speed = 1.0f;                     
        }

        // 플레이어가 화면 밖으로 나갔다면 처음부터
        if ( transform.position.y < -10 )
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void OnTriggerEnter2D(Collider2D other)                     // 골 도착
    {
        Debug.Log("Goal");
        SceneManager.LoadScene("ClearScene");
    }
}
