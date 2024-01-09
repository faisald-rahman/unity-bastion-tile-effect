using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Vector3 targetPosition;

    [SerializeField]
    float duration = 20;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TweenCallback done = () => animator.SetBool("IsWalkDone", true);

        transform.DOMove(targetPosition, duration)
            .SetEase(Ease.Linear)
            .OnComplete(done);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
