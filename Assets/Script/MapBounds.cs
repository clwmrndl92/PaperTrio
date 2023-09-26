using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance._pageManager.RestartPage();
        }
    }
}
