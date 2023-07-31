using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCube : MonoBehaviour
{
    public float decreaseSpeed;

    public float initialValue;
    public float currentValue;

    private Vector3 initialPos;

    public Rigidbody rb;

    public bool isCollisionWithCharacter = false;

    private void Awake()
    {
        initialPos = this.transform.position;
        rb = GetComponent<Rigidbody>();
    }
    public void OnInit()
    {
        currentValue = initialValue;
        this.transform.position = initialPos;
        this.transform.localScale = Vector3.one;
        rb.velocity = Vector3.zero;
        isCollisionWithCharacter = false;
    }

    public void OnCharacterStay()
    {
        currentValue -= decreaseSpeed * Time.deltaTime;
        if (this.transform.localScale.z <= 0)
        {
            Player.ins.runSpeed = Player.ins.initialRunSpeed;
            Debug.Log("finish damage cube");
            this.gameObject.SetActive(false);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, currentValue/initialValue);
        }
    }
}
