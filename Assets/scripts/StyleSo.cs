using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Style", menuName = "Style", order = 0)]
public class StyleSo : ScriptableObject
{
    public List<MaterialSO> materials = new List<MaterialSO>();
}