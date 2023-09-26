using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Object/Stage Data", order = int.MaxValue)]
public class StageData : ScriptableObject
{
    [SerializeField] public List<PageData> Pages;
}
