using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "character data", menuName = "character data")]
public class CharacterData : ScriptableObject
{
    public UnityEngine.GameObject model;
    public Vector3 offsetPos;

    public RuntimeAnimatorController controller;

}
