using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_Test : MonoBehaviour
{
    public ImgsFillDynamic ImgsFD;
    public GameObject ll;
    void Start()
    {
        this.ImgsFD.SetValue(0, true, 0.5f);
    }
    private void Update()
    {

        this.ImgsFD.SetValue(ll.GetComponent<PlayerController>().jung_swordt / 40.0f, true, 0.5f);

    }
}
