using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateController : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMerchant()
    {
        anim.Play("SpaceStationCamera");
    }

    public void ToPlayer()
    {
        anim.Play("PlayerCamera");
    }
}
