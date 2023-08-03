using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MathObject : MonoBehaviour
{
    public MathType type;
    public int value;
    public TextMeshProUGUI scoreText;

    public bool isRandom;

    private void Awake()
    {
        if(isRandom)
        {
            RandomPrevAndValue();
        }
    }

    public void RandomPrevAndValue()
    {
        int randomType = Random.Range(0, 4);
        type = (MathType)randomType;

        if (type == MathType.Add || type == MathType.Subtract)
        {
            int randomValue = Random.Range(1, 8) * 4;
            value = randomValue;
        }
        else
        {
            int randomValue = 2;
            value = randomValue;
        }

    }

    public int CalculateWith(int result)
    {
        switch (type)
        {
            case MathType.Add:
                return result + value;
            case MathType.Subtract:
                return result - value;
            case MathType.Multiply:
                return result * value;
            case MathType.Divide:
                return result / value;
            default: 
                return 0;  
        }
    }

    public void DisplayValue()
    {
        switch (type)
        {
            case MathType.Add:
                scoreText.text = "+" + value.ToString();
                break;
            case MathType.Subtract:
                scoreText.text = "-" + value.ToString();
                break;
            case MathType.Multiply:
                scoreText.text = "x" + value.ToString();
                break;
            case MathType.Divide:
                scoreText.text = "÷" + value.ToString();
                break;
            default:
                scoreText.text = "+" + value.ToString();
                break;
        }
    }
}
