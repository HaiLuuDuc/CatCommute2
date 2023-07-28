using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
