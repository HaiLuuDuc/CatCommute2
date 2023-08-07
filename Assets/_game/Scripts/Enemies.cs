using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Transform[] enemyPos;
    public GameObject boxCollider;

    public void OnInit()
    {
        boxCollider.gameObject.SetActive(true);
    }

    public void OnHit()
    {
        boxCollider.gameObject.SetActive(false);
    }

}
