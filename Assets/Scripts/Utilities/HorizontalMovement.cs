using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementSettings playerSettings;
    [SerializeField] private bool canControl;

    private float _xGoal;

    private void OnEnable()
    {
        EventManager.CanPlayerMove += canMove => canControl = canMove;
    }

    private void OnDisable()
    {
        EventManager.CanPlayerMove -= canMove => canControl = canMove;
    }

  
    private void Start()
    {
        _xGoal = transform.position.x;
        playerSettings.minXClampValue = playerSettings.defaultMinXClampValue;
        playerSettings.maxXClampValue = playerSettings.defaultMaxXClampValue;
    }


    //---------------------------------------------------------------------------------
    private void Update()
    {
        if (canControl == false)
            return;

        _xGoal += EventManager.GetInputDelta().x * playerSettings.horizontalSpeed;
        _xGoal = Mathf.Clamp(_xGoal, playerSettings.minXClampValue, playerSettings.maxXClampValue);

        if (playerSettings.horizontalDamping < float.Epsilon)
            transform.position = new Vector3(_xGoal, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, _xGoal, (1f - playerSettings.horizontalDamping) * Time.deltaTime * 30f), transform.position.y, transform.position.z);
    }


    
}
