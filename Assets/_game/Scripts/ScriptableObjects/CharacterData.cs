using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "character data", menuName = "character data")]
public class CharacterData : ScriptableObject
{
    public UnityEngine.GameObject model;
    public Vector3 offsetPos;

    public AnimatorController controller;
}
