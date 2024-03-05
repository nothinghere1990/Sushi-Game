using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] private RectTransform uiHandleRectTransform;
    [SerializeField] private TMP_Text offText;
    [SerializeField] private TMP_Text onText;
    
    private Toggle toggle;
    private Vector2 handlePos;
    
    
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        handlePos = uiHandleRectTransform.anchoredPosition;
        toggle.onValueChanged.AddListener(OnSwitch);
        OnSwitch(toggle.isOn);
    }

    void OnSwitch(bool on)
    {
        if (on)
        {
            offText.DOFade(0, .2f);
            onText.DOFade(1, .2f);
        }
        else
        {
            onText.DOFade(0, .2f);
            offText.DOFade(1, .2f);
        }
        uiHandleRectTransform.DOAnchorPos(on ? handlePos * -1 : handlePos, .3f).SetEase(Ease.OutBack);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
