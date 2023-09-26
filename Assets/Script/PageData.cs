using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PageData", menuName = "Scriptable Object/Page Data", order = int.MaxValue)]

public class PageData : ScriptableObject
{
    [SerializeField] public List<GameObject> Sections;
    [SerializeField] public Vector3 RespawnPosition;
}
