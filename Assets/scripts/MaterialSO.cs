using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Material Object")]
public class MaterialSO : ScriptableObject
{
    public Material material;
    public Texture Image;
}