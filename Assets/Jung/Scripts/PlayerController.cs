using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public GameObject jung_ball;
    public GameObject fireball_spawn;
    public GameObject young_ball;
    public GameObject youngball_spawn;
    public GameObject jung_hill;
    public GameObject hillzone_spawn;
    public GameObject sword;
    public GameObject sword_spawn;
    public GameObject HP;

    public bool move_flag = true;
    bool coll_flag = true;
    float coll_time = 0.0f;

    bool death_flag = false;
    float death_time = 0.0f;

    bool hiller_flag = false;
    public float jung_hillt = 0.0f;
    float jung_hilltime = 20.0f;
    bool jung_hillflag = true;


    bool jung_flag = false;
    bool jung_ballflag = true;
    float jung_balltime = 4.0f;
    public float jung_ballt = 0.0f;

    bool young_flag = false;
    bool young_ballflag = true;
    float young_balltime = 12.0f;
    public float young_ballt = 0.0f;

    bool sword_flag = false;
    float sword_time = 0.0f;
    bool jung_swordflag = true;
    public float jung_swordt = 0.0f;

    float time = 0.0f;
     float golemDamageTimer = 0.0f;
    float damageInterval = 3.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        golemDamageTimer += Time.deltaTime;

        if (death_flag)
        {
            move_flag = false;
            death_time += Time.deltaTime;
        }
        if (death_time > 4)
        {
            SceneManager.LoadScene("LoseScene");
        }
        if (!jung_ballflag)
        {
            jung_ballt -= Time.deltaTime;
            if (jung_ballt - Time.deltaTime < 0)
            {
                jung_ballt = 0;
                jung_ballflag = true;
            }
        }

        if (!young_ballflag)
        {
            young_ballt -= Time.deltaTime;
            if (young_ballt - Time.deltaTime < 0)
            {
                young_ballt = 0;
                young_ballflag = true;
            }
        }

        if (!jung_hillflag)
        {
            jung_hillt -= Time.deltaTime;
            if (jung_hillt - Time.deltaTime < 0)
            {
                jung_hillt = 0;
                jung_hillflag = true;
            }
        }

        if (!jung_swordflag)
        {
            jung_swordt -= Time.deltaTime;
            if (jung_swordt - Time.deltaTime < 0)
            {
                jung_swordt = 0;
                jung_swordflag = true;
            }
        }

        // 스킬 사용 중 시간 카운트
        if (!move_flag)
        {
            time += Time.deltaTime;
        }
        // 일정 시간이 지나면 움직임 활성화
        if (time > 2.4f && jung_flag)
        {
            time = 0.0f;
            move_flag = true;
            jung_flag = false;
        }
        // 일정 시간이 지나면 움직임 활성화
        if (time > 2.2f && young_flag)
        {
            time = 0.0f;
            move_flag = true;
            young_flag = false;
        }
        if (time > 2.2f && sword_flag)
        {
            time = 0.0f;
            move_flag = true;
            sword_flag = false;
        }
        // 일정 시간이 지나면 움직임 활성화
        if (time > 1.7f && hiller_flag)
        {
            time = 0.0f;
            move_flag = true;
            hiller_flag = false;
        }
        if (coll_time > 1.0f && !jung_flag && !young_flag && !hiller_flag && !sword_flag)
        {
            move_flag = true;
        }
        if (coll_time > 3.0f)
        {
            coll_time = 0.0f;
            coll_flag = true;

        }
        if (!coll_flag) coll_time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.Rotate(0, 180, 0);
        }
        // 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftControl) && move_flag && jung_ballflag)
        {
            GameObject tmp = Instantiate(jung_ball, fireball_spawn.transform.position, this.transform.rotation) as GameObject;
            tmp.transform.parent = tmp.transform;
            anim.SetTrigger("fireball_shoot");
            move_flag = false;
            jung_flag = true;
            jung_ballflag = false;
            jung_ballt = jung_balltime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && move_flag && young_ballflag)
        {
            GameObject tmp = Instantiate(young_ball, youngball_spawn.transform.position, this.transform.rotation) as GameObject;
            tmp.transform.parent = tmp.transform;
            anim.SetTrigger("ultra_shoot");
            move_flag = false;
            young_flag = true;
            young_ballflag = false;
            young_ballt = young_balltime;
        }

        if (Input.GetKeyDown(KeyCode.E) && move_flag && jung_hillflag)
        {
            Instantiate(jung_hill, hillzone_spawn.transform.position, jung_hill.transform.rotation);
            anim.SetTrigger("hill");
            HP.GetComponent<HPController>().HealthBar.value += 0.3f;
            move_flag = false;
            hiller_flag = true;
            jung_hillflag = false;
            jung_hillt = jung_hilltime;
        }

        if (Input.GetKeyDown(KeyCode.R) && move_flag && jung_swordflag)
        {
            Instantiate(sword, sword_spawn.transform.position, sword_spawn.transform.rotation);
            anim.SetTrigger("fireball_shoot");
            move_flag = false;
            sword_flag = true;
            jung_swordflag = false;
            jung_swordt = 40.0f;
        }
        
    }

    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == "golem"  && golemDamageTimer >= damageInterval ) {
            HP.GetComponent<HPController>().HealthBar.value -= 0.2f;
            anim.SetTrigger("gethit");
            golemDamageTimer = 0.0f;
        }
        if (HP.GetComponent<HPController>().HealthBar.value == 0.0f && !death_flag)
        {
            death_flag = true;
            anim.Rebind();
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll_flag && coll.gameObject.tag == "rock")
        {
            if (move_flag)
            {
                anim.SetTrigger("gethit");
                coll_flag = false;
                move_flag = false;
            }

            HP.GetComponent<HPController>().HealthBar.value -= 0.1f;
        }
    }
}
