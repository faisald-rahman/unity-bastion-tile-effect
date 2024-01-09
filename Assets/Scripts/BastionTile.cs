using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Unity.VisualScripting;
using System;
using UnityEngine.Events;

public class BastionTile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private bool isShow = false;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color tempColor = spriteRenderer.color;
        tempColor.a = 0;
        spriteRenderer.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setupShow()
    {
        isShow = true;

        Color tempColor = spriteRenderer.color;
        tempColor.a = 1;
        spriteRenderer.color = tempColor;
    }

    public void Show(float distanceY = 1, float showDuration = 0.25f, Ease ease = Ease.Linear, bool isScaleAnimation = false, UnityAction onAnimationEnd = null)
    {
        if (isShow) return;

        setupShow();

        Vector3 targetPos = transform.position;
        Vector3 beforePos = targetPos;
        beforePos.y += distanceY;

        TweenCallback callback = onAnimationEnd != null ? () => onAnimationEnd(): () => { };

        transform.position = beforePos;
        transform.DOMoveY(targetPos.y, showDuration).SetEase(ease).OnComplete(callback);

        if (isScaleAnimation)
        {
            transform.localScale = new Vector2(0, 0);
            transform.DOScale(1, showDuration);
        }
    }
}
