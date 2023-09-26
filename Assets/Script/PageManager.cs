using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PageManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> leftPages = new();
    private int leftIndex;
    [SerializeField] public List<GameObject> middlePages = new();
    private int middleIndex;
    [SerializeField] public List<GameObject> rightPages = new();
    private int rightIndex;

    [SerializeField] private List<GameObject> dynamicObjects = new();

    [SerializeField] private bool isBetween1;
    [SerializeField] private bool isBetween2;
    private List<GameObject> between1Objects = new();
    private List<GameObject> between2Objects = new();

    private void ChangePage()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseViewportPos = Camera.main.ScreenToViewportPoint(mouseScreenPos);

        if (Input.GetMouseButtonDown(0))
        {
            CheckBetweenSection();

            if (mouseViewportPos.x < 0.33f && CheckPlayerSection() != 1)
            {
                if (isBetween1)
                {
                    BetweenCautionEffect(between1Objects);
                    return;
                }
                NextPage(leftPages, ref leftIndex);
                return;
            }


            if (mouseViewportPos.x < 0.66f && mouseViewportPos.x > 0.33f && CheckPlayerSection() != 2)
            {
                if (isBetween1 || isBetween2)
                {
                    BetweenCautionEffect(between1Objects);
                    BetweenCautionEffect(between2Objects);
                    return;
                }

                NextPage(middlePages, ref middleIndex);
                return;
            }
          

            if (mouseViewportPos.x > 0.66f && CheckPlayerSection() != 3)
            {
                if (isBetween2)
                {
                    BetweenCautionEffect(between2Objects);
                    return;
                }
                NextPage(rightPages, ref rightIndex);
                return;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            CheckBetweenSection();

            if (mouseViewportPos.x < 0.33f && CheckPlayerSection() != 1)
            {
                if (isBetween1)
                {
                    BetweenCautionEffect(between1Objects);
                    return;
                }

                PrevPage(leftPages, ref leftIndex);
                return;
            }

            if (mouseViewportPos.x < 0.66f && mouseViewportPos.x > 0.33f && CheckPlayerSection() != 2)
            {
                if (isBetween1 || isBetween2)
                {
                    BetweenCautionEffect(between1Objects);
                    BetweenCautionEffect(between2Objects);
                    return;
                }

                PrevPage(middlePages, ref middleIndex);
                return;
            }
            
            

            if (mouseViewportPos.x > 0.66f && CheckPlayerSection() != 3)
            {

                if (isBetween2)
                {
                    BetweenCautionEffect(between2Objects);
                    return;
                }
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

    public void ResetPages()
    {
        leftPages.Clear();
        //RemovePagesSections(leftPages);
        middlePages.Clear();
        //RemovePagesSections(middlePages);
        rightPages.Clear();
        //RemovePagesSections(rightPages);
    }
    private void RemovePagesSections(List<GameObject> pages)
    {
        foreach (var page in pages)
        {
            Destroy(page);
        }
    }
        
    private int CheckPlayerSection()
    {
        Vector3 playerPos = GameManager.Instance.GetPlayer().transform.position;
        float playerViewportPosX = Camera.main.WorldToViewportPoint(playerPos).x;
        if (playerViewportPosX <= 0.33f)
        {
            return 1;
        }
        if (playerViewportPosX > 0.33f&& playerViewportPosX<=0.66f)
        {
            return 2;
        }
        if (playerViewportPosX > 0.66f)
        {
            return 3;
        }
        return 0;
    }
    private void CheckBetweenSection()
    {
        isBetween1 = false;
        isBetween2 = false;
        between1Objects.Clear();
        between2Objects.Clear();

        foreach (GameObject go in dynamicObjects)
        {
            float widthHalf= go.GetComponent<Collider2D>().bounds.extents.x;
            float viewportWidthHalf= widthHalf / 16f;
            Debug.Log(viewportWidthHalf);

            float objectViewportPosX = Camera.main.WorldToViewportPoint(go.transform.position).x;
            if(0.33f-viewportWidthHalf<objectViewportPosX
                &&0.33f + viewportWidthHalf >objectViewportPosX)
            {
                between1Objects.Add(go);
                isBetween1 = true;
            }

            if (0.66f - viewportWidthHalf < objectViewportPosX
                && 0.66f + viewportWidthHalf > objectViewportPosX)
            {
                between2Objects.Add(go);
                isBetween2 = true;
            }
        }
    }

    private void BetweenCautionEffect(List<GameObject> objects)
    {
        foreach(GameObject go in objects)
        {
            go.GetComponent<SpriteRenderer>().DOColor(Color.red, 1f)
                .OnComplete(() =>
                {
                    go.GetComponent<SpriteRenderer>().DOColor(Color.white, 1f);
                });
        }
    }


    private void Update()
    {
        ChangePage();
    }
}
