using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> leftPages = new();
    private int leftIndex;
    [SerializeField] private List<GameObject> middlePages = new();
    private int middleIndex;
    [SerializeField] private List<GameObject> rightPages = new();
    private int rightIndex;

    [SerializeField] private List<GameObject> dynamicObjects = new();

    private void ChangePage()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseViewportPos = Camera.main.ScreenToViewportPoint(mouseScreenPos);

        if (Input.GetMouseButtonDown(0))
        {
            
            if (mouseViewportPos.x < 0.33f)
            {
                NextPage(leftPages, ref leftIndex);
                return;
            }
            if (mouseViewportPos.x < 0.66f&& mouseScreenPos.x > 0.33f)
            {
                NextPage(middlePages, ref middleIndex);
                return;
            }

            if (mouseViewportPos.x > 0.66f)
            {
                NextPage(rightPages, ref rightIndex);
                return;
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            if (mouseViewportPos.x < 0.33f)
            {
                PrevPage(leftPages, ref leftIndex);
                return;
            }
            if (mouseViewportPos.x < 0.66f && mouseScreenPos.x > 0.33f)
            {
                PrevPage(middlePages, ref middleIndex);
                return;
            }

            if (mouseViewportPos.x > 0.66f)
            {
                PrevPage(rightPages, ref rightIndex);
                return;
            }
        }
    }

    private void PrevPage(List<GameObject> list, ref int index)
    {
        UpdateDynamicObjectParent();
        if (index == 0)
        {
            return;
        }

        list[index].SetActive(false);
        index -= 1;
        list[index].SetActive(true);


    }

    private void NextPage(List<GameObject> list, ref int index)
    {
        UpdateDynamicObjectParent();
        if (index == list.Count-1)
        {
            return;
        }

        list[index].SetActive(false);
        index += 1;
        list[index].SetActive(true);
    }
    
    private void UpdateDynamicObjectParent()
    {
        foreach(GameObject go in dynamicObjects)
        {
            if (go.transform.parent.gameObject.activeSelf)
            {
                Vector3 viewportPos = Camera.main.WorldToViewportPoint(go.transform.position);
                if(viewportPos.x < 0.33f)
                {
                    go.transform.parent = leftPages[leftIndex].transform;
                    continue;
                }
                if(viewportPos.x > 0.33f&& viewportPos.x < 0.66f)
                {
                    go.transform.parent = middlePages[middleIndex].transform;
                    continue;
                }
                if (viewportPos.x > 0.66f)
                {
                    go.transform.parent = rightPages[rightIndex].transform;
                    continue;
                }
            }
        }
    }
    private void Update()
    {
        ChangePage();
    }
}
