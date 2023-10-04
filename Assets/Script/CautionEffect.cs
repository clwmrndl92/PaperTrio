using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CautionEffect : MonoBehaviour
{
    private Color originColor;

    private void Awake()
    {
        originColor = GetComponent<SpriteRenderer>().color;
    }
    public void cautionEffectPlay()
    {
        GetComponent<SpriteRenderer>().DOColor(Color.red, 0.3f)
            .OnComplete(() =>
            {
                GetComponent<SpriteRenderer>().DOColor(originColor, 0.3f);
            });
    }
}