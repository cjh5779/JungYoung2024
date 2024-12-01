using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem_spawn : MonoBehaviour
{
    public GameObject[] golem = new GameObject[2];
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
           
            Instantiate(golem[Random.Range(0, 2)],this.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(Random.Range(20.0f, 30.0f));
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}