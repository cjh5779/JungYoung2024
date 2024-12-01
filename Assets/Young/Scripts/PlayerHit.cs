using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
     public Animator anim;
     bool collision_flag = true;
     float collision_time = 0;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!collision_flag) collision_time += Time.deltaTime;  // 플레이어가 공격을 받을 수 있는 시간 체크
        if(collision_time > 3.0f)
        {
            collision_time = 0.0f;
            collision_flag = true;   
        }
    }

   void OnCollisionEnter(Collision collision) 
   {
        if (collision_flag && collision.gameObject.tag == "rock")
        {
                anim.SetTrigger("gethit");
                collision_flag = false;
            
        }else if (collision_flag && collision.gameObject.tag == "golem"){

                anim.SetTrigger("gethit");
                collision_flag = false;
        }
    }

}