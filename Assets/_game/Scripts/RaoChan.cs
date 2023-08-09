using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaoChan : MonoBehaviour
{
    public Transform child;


    public void OnInit()
    {
        child.localPosition = Vector3.zero;
        child.localRotation = Quaternion.identity;
    }

    public void OnHit()
    {

    }
}
