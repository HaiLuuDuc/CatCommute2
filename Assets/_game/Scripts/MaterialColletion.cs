using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColletion : Singleton<MaterialColletion>
{
    public Material[] mats;

    public Material GetMat(MaterialType matType)
    {
        return mats[(int)matType];
    }
}
