using KartGame.KartSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInput : BaseInput
{
    [SerializeField]
    Image joystickImage,joystickGhost;
    RectTransform imageCanvas;
    [SerializeField]
    float maxJoystickDisplacement = 5f;

    Vector2 initialTouchPosition = new Vector2(0f, 0f);

    bool pressingBreak = false;

    Vector2 joystickDirection = new Vector2(0f,0f);


    private void Start()
    {
        imageCanvas  = joystickImage.canvas.GetComponent<RectTransform>();
    }

    public override InputData GenerateInput()
    {
        return new InputData
        {
            Accelerate = joystickDirection.y > 0f ? true : false,
            Brake = joystickDirection.y < 0f ? true : false,
            TurnInput = joystickDirection.normalized.x,
            IsTouch = true
        };
    }

    public void BreakKart()
    {
        pressingBreak = true;
    }

    private void Update()
    {
        if(Input.touchCount>0)
        {
            Touch mainTouch = Input.GetTouch(0);
            if(mainTouch.phase==TouchPhase.Began)
            {
                joystickImage.gameObject.SetActive(true);
                joystickGhost.gameObject.SetActive(true);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(imageCanvas, mainTouch.position, null, out initialTouchPosition);
                joystickImage.rectTransform.anchoredPosition = initialTouchPosition;
                joystickGhost.rectTransform.anchoredPosition = initialTouchPosition;
            }
            else if(mainTouch.phase == TouchPhase.Moved)
            {
                MoveJoystickWithInput(mainTouch.position);
            }
            else if(mainTouch.phase==TouchPhase.Ended)
            {
                joystickImage.gameObject.SetActive(false);
                joystickGhost.gameObject.SetActive(false);
                joystickDirection = new Vector2(0f, 0f);
            }            
        }
    }

    void MoveJoystickWithInput(Vector2 _touchPosition)
    {
        Vector2 inputCanvasPos = new Vector2(0f, 0f);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imageCanvas, _touchPosition, null, out inputCanvasPos);

        joystickDirection = Vector2.ClampMagnitude(inputCanvasPos - initialTouchPosition, maxJoystickDisplacement);

        joystickImage.rectTransform.anchoredPosition = initialTouchPosition + joystickDirection;

    }
}
