using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongTac : MonoBehaviour
{
    public Material blueMat;
    public Material greenMat;
    public MeshRenderer meshRenderer;
    public bool isHit = false;

    public virtual void OnInit()
    {
        meshRenderer.material = blueMat;
        isHit = false;
    }

    public virtual void OnHit()
    {
        meshRenderer.material = greenMat;
        isHit = true;
    }
}
