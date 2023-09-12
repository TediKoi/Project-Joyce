using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public bool isInteracting;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isInteracting = true;
        }
        else if(context.canceled)
        {
            isInteracting = false;
        }
    }
}
