using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Tile : MathObject
{
    public MeshRenderer meshRenderer;
    public BoxCollider collider;
    public Tile siblingTile;

    public UnityEngine.GameObject blueGate;
    public UnityEngine.GameObject redGate;


    public void OnInit()
    {
        GetSiblingTile();
        string prev = "";
        switch (type)
        {
            case MathType.Add:
                prev = "+";
                ChangeMat(MaterialType.Blue);
                InitGateModel(GateColor.Blue);
                break;
            case MathType.Subtract:
                prev = "-";
                ChangeMat(MaterialType.Red);
                InitGateModel(GateColor.Red);
                break;
            case MathType.Multiply:
                prev = "x";
                ChangeMat(MaterialType.Blue);
                InitGateModel(GateColor.Blue);
                break;
            case MathType.Divide:
                prev = "÷";
                ChangeMat(MaterialType.Red);
                InitGateModel(GateColor.Red);
                break;
        }
        scoreText.text = prev + value.ToString();
        scoreText.outlineWidth = 0.001f;
        EnableCollider();
    }

    public void ChangeMat(MaterialType type)
    {
        meshRenderer.material = MaterialColletion.ins.GetMat(type);
    }

    public void OnHit()
    {
        siblingTile.DisableCollider();
        this.gameObject.SetActive(false);
    }

    public void GetSiblingTile()
    {
        UnityEngine.GameObject parent = this.gameObject.transform.parent.gameObject;
        siblingTile = parent.GetComponentInChildren<Tile>();
    }

    public void EnableCollider()
    {
        collider.enabled = true;
    }

    public void DisableCollider()
    {
        collider.enabled = false;
    }

    public void InitGateModel(GateColor gateColor)
    {
        UnityEngine.GameObject g = null;

        if (gateColor == GateColor.Blue)
        {
            g = Instantiate(blueGate);
            
        }
        else if (gateColor == GateColor.Red)
        {
            g = Instantiate(redGate);
        }

        g.transform.SetParent(this.transform);
        g.transform.localPosition = new Vector3(0, 0.08f, 0); //0.08
        g.transform.localScale = new Vector3(0.8f, 1 ,1);

    }
}
