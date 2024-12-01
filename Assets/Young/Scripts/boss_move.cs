using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boss_move : MonoBehaviour
{
    GameObject target;  // 플레이어
    Animator anim; // 에니메이션
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject HP;

    public float NomalAttackRange = 1.5f;   // 근거리 공격 범위 설정
    public float  SkillAttackRange = 9.0f;

    public float MaxSkillAttackRange = 6.0f;

    public GameObject[] sword; // 소환할 검
    public Transform[] sword_spawn; // 검 소환 위치

    float attackCoolTime = 10.0f; // 공격 쿨타임
    float lastAttackTime = 0f; // 마지막 공격 시간
    float death_time = 0.0f;
    bool death_flag = false;

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


        if (agent.remainingDistance >= agent.stoppingDistance)  // 실제 남아있는거리 > 설정한 스탑거리
        {
            anim.SetBool("walk_flag", true);
            
        }else
        {
              anim.SetBool("walk_flag", false);
           
        }

        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToPlayer > SkillAttackRange && distanceToPlayer <= MaxSkillAttackRange && Time.time > lastAttackTime + attackCoolTime)
        {
            SkillAttack();
        }
        else if (distanceToPlayer <= NomalAttackRange && Time.time > lastAttackTime + attackCoolTime) 
        {
            NomalAttack();
        }


         if (death_flag)
        {
            death_time += Time.deltaTime;
        }
        if (death_time > 3)
        {
            SceneManager.LoadScene("WinScene");
        }

        
    }

    void NomalAttack()
    {
        agent.isStopped = true;

        anim.SetTrigger("nomalattack"); // 공격 애니메이션 실행
        lastAttackTime = Time.time; // 공격 시간 갱신

          StartCoroutine(ResumeMovementAfterAttack("nomalattack"));
        
    }
     void SkillAttack()
    {
        agent.isStopped = true;

        anim.SetTrigger("skillattack"); // 공격 애니메이션 실행

        for (int i = 0; i < 8; i++)
        {
            Instantiate(sword[i], sword_spawn[i].transform.position, sword_spawn[i].transform.rotation);
        }
        lastAttackTime = Time.time; // 공격 시간 갱신

        StartCoroutine(ResumeMovementAfterAttack("skillattack"));
        
    }


     IEnumerator ResumeMovementAfterAttack(string attackTrigger)
    {

        // 애니메이션 길이가 끝날 때까지 대기 (애니메이션의 길이에 맞춰 조절)
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        
        // 애니메이션 후 이동 재개
        agent.isStopped = false;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            HP.GetComponent<HPController>().HealthBar.value -= 0.5f;
            Canvas.ForceUpdateCanvases();
            anim.SetTrigger("gethit");
            if (HP.GetComponent<HPController>().HealthBar.value == 0.0f)
            {
                Die();
            }
        }
    }
    void Die()
    {

     anim.SetTrigger("die");
     agent.isStopped = true;
     death_flag = true;
     Destroy(gameObject, 3f);  // 3초 후 삭제
    
    }

}
