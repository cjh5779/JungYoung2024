using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_spawn : MonoBehaviour
{
    public GameObject boss;              // 보스 프리팹 참조
    public string bossHPBarName = "BossHPBar"; // 하이어라키에서 HP 바의 이름 (이름을 기준으로 찾음)
    private bool bossSpawned = false;     // 보스가 생성되었는지 여부를 체크하는 플래그
    public GameObject bossHPBar;

    // Start는 한 번만 호출됩니다.
    void Start()
    {
        // 초기화가 필요하면 이곳에 추가
    }

    // Update는 매 프레임마다 호출됩니다.
    void Update()
    {
        if (golem_move.deathcount >= 3 && !bossSpawned) // golem_move의 deathcount가 3 이상이고, 보스가 생성되지 않았다면
        {
            bossSpawned = true; // 보스가 생성되었음을 표시
            GameObject spawnedBoss = Instantiate(boss, this.transform.position, this.transform.rotation); // 보스 생성

            // 보스 생성 후 HP 바 활성화
            if (spawnedBoss != null)
            {
                if (bossHPBar != null)
                {
                    bossHPBar.SetActive(true); // HP 바를 활성화
                }
            }
        }
    }
}
