using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_move : MonoBehaviour
{
    GameObject target;  // 플레이어
     Animator anim; // 에니메이션
     public UnityEngine.AI.NavMeshAgent agent;

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
    }
}
