using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour
{
    private Vector2 mousePosition;

    [SerializeField]
    private Transform aimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Aiming(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 targetDirection = mouseWorldPosition - aimer.position;
        float directionAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        aimer.rotation = Quaternion.Euler(new Vector3(0, 0, directionAngle + -90));


    }
}
