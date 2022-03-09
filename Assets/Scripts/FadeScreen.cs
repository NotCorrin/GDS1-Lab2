using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut()
    {
        anim.SetBool("fade", true);
        anim.SetBool("black", true);
    }

    public void FadeIn()
    {
        anim.SetBool("black", false);
        anim.SetBool("fade", false);
    }
}
