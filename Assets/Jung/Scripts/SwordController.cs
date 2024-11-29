using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public float speed = 25.0f;
    float time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.75f)
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (time > 5.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
