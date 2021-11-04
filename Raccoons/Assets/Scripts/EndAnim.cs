using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndAnim : MonoBehaviour
{
    private Transform start;
    public Transform transfA;
    public Transform transB;
    public bool win;
    private float duration;
    private float delay;
    private float scaleFactor;
    // Start is called before the first frame update
    void Start()
    {
        start = transform;
        duration = 1f;
        delay = 0.6f;
        scaleFactor = 0.3f;
        //if (win)
        //{
        //    InvokeRepeating(nameof(WinAnimUp), 0f, (duration + delay) * 2);
        //    InvokeRepeating(nameof(WinAnimDown), duration + delay, (duration + delay) * 2);
        //}
        //else
        //{
        //    InvokeRepeating(nameof(LoseAnimLeft), 0f, (duration + delay) * 2);
        //    InvokeRepeating(nameof(LoseAnimRight), duration + delay, (duration + delay) * 2);
        //}
        
    }

    void WinAnimUp()
    {
        transform.DOLocalMoveY(transfA.position.y, duration);
        transform.DOScale(scaleFactor, duration);
    }

    void WinAnimDown()
    {
        transform.DOLocalMoveY(start.position.y, duration);
        transform.DOScaleY(start.transform.localScale.y, duration);
    }

    void LoseAnimLeft()
    {
        
    }

    void LoseAnimRight()
    {

    }

    public void StopAnim()
    {
        StopAllCoroutines();
        transform.position = start.position;
        transform.rotation = start.rotation;
        transform.localScale = start.localScale;
    }
}
