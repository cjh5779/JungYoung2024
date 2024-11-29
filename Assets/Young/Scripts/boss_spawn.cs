using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_spawn : MonoBehaviour
{
    public GameObject boss;
     private bool bossSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (golem_move.deathcount >=3 && !bossSpawned) 
        {
            Instantiate(boss,this.transform.position, this.transform.rotation);
            bossSpawned = true;
        }
    }

}
