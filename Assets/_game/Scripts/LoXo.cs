using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoXo : MonoBehaviour
{
    public Animator anim;

    public void OnHit()
    {
        anim.SetTrigger("bounce");
    }
}
