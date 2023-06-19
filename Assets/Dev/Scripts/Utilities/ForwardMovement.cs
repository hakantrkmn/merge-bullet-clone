using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField] PlayerMovementSettings playerSettings;
    private bool isMoveStraight = true;

    [SerializeField] private bool canControl;

    private void OnEnable()
    {
        EventManager.CanPlayerMove += canMove => canControl = canMove;
    }

    private void OnDisable()
    {
        EventManager.CanPlayerMove -= canMove => canControl = canMove;
    }

    void Update()
    {
        if (!canControl)
            return;

        if (isMoveStraight)
            transform.position += new Vector3(0f, 0f, playerSettings.forwardSpeed * Time.deltaTime);
    }


    //---------------------------------------------------------------------------------
    private void SwitchCanControl()
    {
        canControl = !canControl;
    }
}
