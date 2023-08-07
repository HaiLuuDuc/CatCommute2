using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongTac : MonoBehaviour
{
    public Material blueMat;
    public Material greenMat;
    public MeshRenderer meshRenderer;

    public virtual void OnInit()
    {
        meshRenderer.material = blueMat;
    }

    public virtual void OnHit()
    {
        meshRenderer.material = greenMat;
    }
}
