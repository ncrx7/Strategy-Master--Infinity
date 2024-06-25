using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StatUILocomotion : MonoBehaviour
{
    [SerializeField] private RectTransform _statUIObject; 
    [SerializeField] private Button _centerButton;     
    [SerializeField] private Button _bottomButton;     

    [SerializeField] private RectTransform _canvasRectTransform; 

    private Vector2 _bottomTargetPosition;
    private Vector2 _centerTargetPosition;

    void Start()
    {
        _centerButton.onClick.AddListener(MoveToCenter);
        _bottomButton.onClick.AddListener(MoveToBottom);

        _bottomTargetPosition = CalculateBottomTargetPosition();
        _centerTargetPosition = CalculateCenterTargetPosition();

        _statUIObject.anchoredPosition = _bottomTargetPosition;
    }

    void MoveToCenter()
    {
        
        _statUIObject.DOAnchorPos(_centerTargetPosition, 0.5f).SetEase(Ease.InOutQuad);
    }

    void MoveToBottom()
    {
        _statUIObject.DOAnchorPos(_bottomTargetPosition, 0.5f).SetEase(Ease.InOutQuad);
    }

    private Vector2 CalculateBottomTargetPosition()
    {
        float halfHeight = _canvasRectTransform.rect.height / 2;
        float bottomYPosition = halfHeight + _statUIObject.rect.height / 2;
        Vector2 bottomTargetPosition = new Vector2(0, -bottomYPosition);
        return bottomTargetPosition;
    }

    private Vector2 CalculateCenterTargetPosition()
    {
        return new Vector2(0, 0);
    }
}
