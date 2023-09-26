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
            if (!s.transform.parent.gameObject.activeSelf)
                continue;

            if (!s.isSwitch)
            {
                foreach (Wall wall in walls)
                {
                    if (wall.transform.parent.gameObject.activeSelf)
                    {
                        wall.ReverseMove();
                    }
                }
                return;
            }

            foreach (Wall wall in walls)
            {
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
}
