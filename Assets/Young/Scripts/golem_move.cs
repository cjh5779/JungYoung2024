using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem_move : MonoBehaviour
{
     GameObject target;  // 플레이어
     Animator anim; // 에니메이션
     public UnityEngine.AI.NavMeshAgent agent;
     bool move_flag = true;    // 이동 가능 상태 체크 ..? 

     public GameObject rock;  // 던질 돌
     public GameObject rockSpawnPoint;  // 돌이 나오는 위치

    public float ThrowAttackRange = 3.0f; // 원거리 공격 범위 설정
    public float NomalAttackRange = 1.5f;   // 근거리 공격 범위 설정

    public float maxThrowAttackRange = 10.0f;  // 돌을 던질 수 있는 최대 범위

    public int health = 2;



     float attackCoolTime = 10.0f; // 공격 쿨타임
     float lastAttackTime = 0f; // 마지막 공격 시간



    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("unitychan_dynamic");
         anim = GetComponent<Animator>();
         
    }

    // Update is called once per frame
    void Update()
    {   
         agent.SetDestination(target.transform.position);


        if (agent.remainingDistance > agent.stoppingDistance)  // 실제 남아있는거리 > 설정한 스탑거리
        {
            anim.SetBool("walk_flag", true);
            move_flag = true;  // 움직일 수 있는 상태
        }else
        {
              anim.SetBool("walk_flag", false);
              move_flag = false; // 정지 상태
        }

        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToPlayer > NomalAttackRange && distanceToPlayer <= maxThrowAttackRange && Time.time > lastAttackTime + attackCoolTime)
        {
            ThrowAttack();
        }
        else if (distanceToPlayer <= NomalAttackRange && Time.time > lastAttackTime + attackCoolTime) 
        {
            NomalAttack();
        }
       
    }

    void ThrowAttack()
    {
        agent.isStopped = true;
        anim.SetTrigger("throwattack");
        lastAttackTime = Time.time;
        GameObject thrownRock = Instantiate(rock, rockSpawnPoint.transform.position, rockSpawnPoint.transform.rotation) as GameObject;
        Rigidbody rb = thrownRock.GetComponent<Rigidbody>();
        Vector3 direction =  thrownRock.transform.TransformDirection(Vector3.forward);
        rb.AddForce(direction * 500);  // 힘을 주어 돌을 던지기
        StartCoroutine(ResumeMovementAfterAttack("throwattack"));
    }

    void NomalAttack()
    {
        agent.isStopped = true;

        anim.SetTrigger("nomalattack"); // 공격 애니메이션 실행
        lastAttackTime = Time.time; // 공격 시간 갱신

          StartCoroutine(ResumeMovementAfterAttack("nomalattack"));
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
    {
        health--;
        anim.SetTrigger("get_hit");
            if (health <= 0)
            {
                Die();
            }
    }
    }

    void Die()
    {
    anim.SetTrigger("die");
    agent.isStopped = true;
    Destroy(gameObject, 10f);  // 3초 후 삭제
    }









      IEnumerator ResumeMovementAfterAttack(string attackTrigger)
    {
        // 애니메이션 길이가 끝날 때까지 대기 (애니메이션의 길이에 맞춰 조절)
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        
        // 애니메이션 후 이동 재개
        agent.isStopped = false;
    }
}

          
    