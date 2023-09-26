using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Object/Map Data", order = int.MaxValue)]
public class MapData : ScriptableObject
{
    [SerializeField] public List<StageData> Stages;
}
// Stages[stageIndex].Pages[pageIndex].Sections[sectionIndex];
