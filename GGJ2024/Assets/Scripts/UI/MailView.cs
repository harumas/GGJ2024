using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MailView : MonoBehaviour
{
    [SerializeField] private RectTransform mailImage;
    [SerializeField] private float offset;
    [SerializeField] private float duration;

    private bool wasRead;

    private void Update()
    {
        if (!wasRead && Input.GetMouseButtonDown(0))
        {
            mailImage.DOLocalMoveY(offset, duration).SetRelative(true).Play();
            wasRead = true;
        }
    }
}