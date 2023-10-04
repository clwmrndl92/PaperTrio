using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GetPlayer().transform.parent = null;
            this.gameObject.SetActive(false);
            GameManager.Instance.NextPage();
        }
    }
}
