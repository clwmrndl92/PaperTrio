using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [SerializeField] private List<Switch> switch1List = new();
    [SerializeField] private List<Switch> switch2List = new();
    [SerializeField] private List<Switch> switch3List = new();

    [SerializeField] private List<Wall> wall1List = new();
    [SerializeField] private List<Wall> wall2List = new();
    [SerializeField] private List<Wall> wall3List = new();

    private void CheckSwitchList(List<Switch> switches, List<Wall> walls)
    {
        foreach(Switch s in switches)
        {
            if (s == null)
                continue;

            if (!s.transform.parent.transform.parent.gameObject.activeSelf)
                continue;

            if (!s.isSwitch)
            {
                foreach (Wall wall in walls)
                {
                    if (wall == null)
                        continue;
                    if (wall.transform.parent.gameObject.activeSelf)
                    {
                        wall.ReverseMove();
                    }
                }
                return;
            }

            foreach (Wall wall in walls)
            {
                if (wall == null)
                    continue;
                if (wall.transform.parent.gameObject.activeSelf)
                {
                    wall.Move();
                }
            }
        }
    }

    private void MoveWalls()
    {
        CheckSwitchList(switch1List, wall1List);
        CheckSwitchList(switch2List, wall2List);
        CheckSwitchList(switch3List, wall3List);
    }

    private void Update()
    {
        MoveWalls();
    }

    public void UpdateSwitchList(GameObject obj)
    {
        var switches = obj.GetComponentsInChildren<Switch>();
        foreach(var s in switches)
        {
            if (s.gameObject.name == "Switch1")
            {
                switch1List.Add(s);
            }
            else if (s.gameObject.name == "Switch2")
            {
                switch2List.Add(s);
            }
            else if (s.gameObject.name == "Switch3")
            {
                switch3List.Add(s);
            }
        }
    }

    public void UpdateWallList(GameObject obj)
    {
        var walls = obj.GetComponentsInChildren<Wall>();
        foreach (var w in walls)
        {
            if (w.gameObject.name == "Wall1")
            {
                wall1List.Add(w);
            }
            else if (w.gameObject.name == "Wall2")
            {
                wall2List.Add(w);
            }
            else if (w.gameObject.name == "Wall3")
            {
                wall3List.Add(w);
            }
        }
    }
}
