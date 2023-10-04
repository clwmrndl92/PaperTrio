using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PageManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> leftPages = new();
    private int leftIndex;
    [SerializeField] public List<GameObject> middlePages = new();
    private int middleIndex;
    [SerializeField] public List<GameObject> rightPages = new();
    private int rightIndex;

    [SerializeField] SwitchManager switchManager;

    [SerializeField] List<GameObject> savedLeftPages = new();
    [SerializeField] List<GameObject> savedMiddlePages = new();
    [SerializeField] List<GameObject> savedRightPages = new();

    [SerializeField] private List<GameObject> dynamicObjects = new();

    [SerializeField] private bool isBetween1;
    [SerializeField] private bool isBetween2;
    private List<GameObject> between1Objects = new();
    private List<GameObject> between2Objects = new();

    [SerializeField] private int maxIndex;

    [SerializeField] GameObject ClearText;

    [SerializeField] List<SpriteRenderer> backLayer1;
    [SerializeField] List<SpriteRenderer> backLayer2;
    [SerializeField] Color red;
    [SerializeField] Color yellow;

    [SerializeField] private List<AutoFlip> leftCurlList;
    [SerializeField] private List<AutoFlip> middleCurlList;
    [SerializeField] private List<AutoFlip> rightCurlList;
    [SerializeField] private float curlWaitSeconds;
    private bool isCurling;
    private void SetColor()
    {
        if (leftIndex == 0)
        {
            backLayer1[0].color = Color.white;
            backLayer2[0].color = Color.white;
        }
        if (leftIndex == 1)
        {
            backLayer1[0].color = yellow;
            backLayer2[0].color = Color.white;
        }
        if (leftIndex == 2)
        {
            backLayer1[0].color = red;
            backLayer2[0].color = yellow;
        }

        if (rightIndex == 0)
        {
            backLayer1[2].color = Color.white;
            backLayer2[2].color = Color.white;
            }
        if (rightIndex == 1)
        {
            backLayer1[2].color = yellow;
            backLayer2[2].color = Color.white;
        }
        if (rightIndex == 2)
        {
            backLayer1[2].color = red;
            backLayer2[2].color = yellow;
        }

        if (middleIndex == 0)
        {
            backLayer1[1].color = Color.white;
            backLayer2[1].color = Color.white;
        }
        if (middleIndex == 1)
        {
            backLayer1[1].color = yellow;
            backLayer2[1].color = Color.white;
        }
        if (middleIndex == 2)
        {
            backLayer1[1].color = red;
            backLayer2[1].color = yellow;
        }
    }
    private void Start()
    {
        GameManager.Instance.initScene();
        maxIndex = GameManager.Instance.CurrentPage;
        LoadPage();
        PrevPage(leftPages, ref leftIndex);
        PrevPage(middlePages, ref middleIndex);
       // PrevPage(leftPages, ref leftIndex);
    }

    public void LoadPage()
    {
        maxIndex = GameManager.Instance.CurrentPage;
        int pageIndex = maxIndex;

        switchManager.ResetAllList();
        ClearBoxList();

        UpdateAllPages();
        GameManager.Instance.LoadPlayerSpawnPosition();
        UpdatePagesIndex(pageIndex);

        for (int i = 0; i <= pageIndex; i++)
        {
            if (i != pageIndex)
            {
                leftPages[i].SetActive(false);
                middlePages[i].SetActive(false);
                rightPages[i].SetActive(false);
            }
            else
            {
                leftPages[i].SetActive(true);
                middlePages[i].SetActive(true);
                rightPages[i].SetActive(true);
            }
        }
        SetColor();
    }

    private void ChangePage()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseViewportPos = Camera.main.ScreenToViewportPoint(mouseScreenPos);

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            CheckBetweenSection();

            if (mouseViewportPos.x < 0.33f && CheckPlayerSection() != 1)
            {
                if (isBetween1)
                {
                    BetweenCautionEffect(between1Objects);
                    return;
                }


                bool isNext=NextPage(leftPages, ref leftIndex);
                if (isNext)
                {
                    NextPageCurlingEffect(leftCurlList, ref leftIndex);
                    isCurling = true;
                   // DynamicObjectDisable(0);
                    StartCoroutine(NextPageActive(leftPages, leftIndex));
                    
                }
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

                bool isNext = NextPage(middlePages, ref middleIndex);
                if (isNext)
                {
                    NextPageCurlingEffect(middleCurlList, ref middleIndex);
                    isCurling = true;
                    //DynamicObjectDisable(1);
                    StartCoroutine(NextPageActive(middlePages, middleIndex));
                }
                return;
            }
          

            if (mouseViewportPos.x > 0.66f && CheckPlayerSection() != 3)
            {
                if (isBetween2)
                {
                    BetweenCautionEffect(between2Objects);
                    return;
                }

                bool isNext = NextPage(rightPages, ref rightIndex);
                if (isNext)
                {
                    NextPageCurlingEffect(rightCurlList, ref rightIndex);
                    isCurling = true;
                    //DynamicObjectDisable(2);
                    StartCoroutine(NextPageActive(rightPages, rightIndex));
                }
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

                if(leftIndex != 0)
                    PrevPageCurlingEffect(leftCurlList, ref leftIndex);
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

                if(middleIndex != 0)
                    PrevPageCurlingEffect(middleCurlList, ref middleIndex);
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

                if(rightIndex != 0)
                    PrevPageCurlingEffect(rightCurlList, ref rightIndex);
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

        //list[index].GetComponent<Animator>().SetTrigger("FlipPageUpTrigger");
        list[index].SetActive(false);
        index -= 1;
        list[index].SetActive(true);
        SetColor();

    }

    private void PrevPageCurlingEffect(List<AutoFlip> list, ref int index)
    {
        
        list[index].transform.parent.gameObject.SetActive(true);
        list[index].FlipRightPage();
    }

    private void NextPageCurlingEffect(List<AutoFlip> list, ref int index)
    {/*
        if (index - 1 < 0)
            return;*/
        list[index+1].transform.parent.gameObject.SetActive(true);
        list[index+1].FlipLeftPage();
    }

    private void DynamicObjectDisable(int page)
    {
        foreach(GameObject go in dynamicObjects)
        {
            if (go == null)
                return;
            if (!go.transform.parent.gameObject.activeSelf)
                return;

            Vector3 viewportPos = Camera.main.WorldToViewportPoint(go.transform.position);
            if (viewportPos.x < 0.33f&&page==0)
            {
                go.SetActive(false);
                continue;
            }
            if (viewportPos.x > 0.33f && viewportPos.x < 0.66f && page == 1)
            {
                go.SetActive(false);
                continue;
            }
            if (viewportPos.x > 0.66f && page == 2)
            {
                go.SetActive(false);
                continue;
            }
        }
    }

    private bool NextPage(List<GameObject> list, ref int index)
    {
        Debug.Log("nextPage");
        UpdateDynamicObjectParent();

        if (index == maxIndex)
        {
            return false;
        }

        index += 1;

        return true;

        
    }
    

    IEnumerator NextPageActive(List<GameObject> list, int index)
    {
        yield return new WaitForSeconds(curlWaitSeconds);
        isCurling = false;
        list[index-1].SetActive(false);
        list[index].SetActive(true);
        SetColor();
    }
    private void UpdateDynamicObjectParent()
    {
        foreach(GameObject go in dynamicObjects)
        {
            if (go == null)
                continue;

            if (isCurling)
                return;

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

    public void UpdatePagesIndex(int index)
    {
        leftIndex = index;
        middleIndex = index;
        rightIndex = index;
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
            if (go == null)
                continue;
            float widthHalf= go.GetComponentInChildren<Collider2D>().bounds.extents.x;
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
            /*Color originColor = go.GetComponent<SpriteRenderer>().color;
            go.GetComponent<SpriteRenderer>().DOColor(Color.red, 1f)
                .OnComplete(() =>
                {
                    go.GetComponent<SpriteRenderer>().DOColor(originColor, 1f);
                });*/
        }
    }

    public void SavePages()
    {
        savedLeftPages.Clear();
        savedMiddlePages.Clear();
        savedRightPages.Clear();
        GameObject obj;
        for (int i = 0; i <= maxIndex; i++)
        {
            obj = GameObject.Instantiate(leftPages[i]);
            savedLeftPages.Add(obj);
            obj.SetActive(false);
            //UpdateNewPages(obj);

            obj = GameObject.Instantiate(middlePages[i]);
            savedMiddlePages.Add(obj);
            obj.SetActive(false);
            //UpdateNewPages(obj);

            obj = GameObject.Instantiate(rightPages[i]);
            savedRightPages.Add(obj);
            obj.SetActive(false);
            //UpdateNewPages(obj);
        }
    }

    public void UpdateAllPages()
    {
        for (int i = 0; i <= maxIndex; i++)
        {
            UpdateNewPage(leftPages[i]);
            UpdateNewPage(middlePages[i]);
            UpdateNewPage(rightPages[i]);
        }
    }

    private void UpdateNewPage(GameObject obj)
    {
        switchManager.UpdateSwitchList(obj);
        switchManager.UpdateWallList(obj);
        UpdateBoxList(obj);
        obj.SetActive(false);
    }

    public void RestartPage()
    {
        for (int i = 0; i < maxIndex; i++)
        {
            leftPages[i].SetActive(false);
            Destroy(leftPages[i]);
            leftPages[i] = GameObject.Instantiate(savedLeftPages[i]);
            middlePages[i].SetActive(false);
            Destroy(middlePages[i]);
            middlePages[i] = GameObject.Instantiate(savedMiddlePages[i]);
            rightPages[i].SetActive(false);
            Destroy(rightPages[i]);
            rightPages[i] = GameObject.Instantiate(savedRightPages[i]);
        }
        int currentStage = GameManager.Instance.CurrentStage;
        int currentPage = GameManager.Instance.CurrentPage;
        var sections = GameManager.Instance.Map.Stages[currentStage].Pages[currentPage].Sections;
        Destroy(leftPages[maxIndex]);
        Destroy(middlePages[maxIndex]);
        Destroy(rightPages[maxIndex]);
        leftPages[maxIndex] = GameObject.Instantiate(sections[0]);
        leftPages[maxIndex].SetActive(false);
        //UpdateNewPages(leftPages[maxIndex]);
        middlePages[maxIndex] = GameObject.Instantiate(sections[1]);
        middlePages[maxIndex].SetActive(false);
        //UpdateNewPages(middlePages[maxIndex]);
        rightPages[maxIndex] = GameObject.Instantiate(sections[2]);
        rightPages[maxIndex].SetActive(false);
        //UpdateNewPages(rightPages[maxIndex]);
        LoadPage();
    }

    public void ClearBoxList()
    {
        dynamicObjects.Clear();
    }

    public void UpdateBoxList(GameObject obj)
    {
        foreach(Transform child in obj.transform)
        {
            if (child.CompareTag("Box"))
            {
                dynamicObjects.Add(child.gameObject);
            }
        }
    }

    public void ClearStage(){
        ClearText.SetActive(true);
    }

    private void Update()
    {
        ChangePage();
        UpdateDynamicObjectParent();
    }
}
