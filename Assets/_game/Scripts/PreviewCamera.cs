using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;


    void Update()
    {
        this.transform.position = target.transform.position + offset;
    }
}
