using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public bool isInteracting;

    private static PlayerInteract instance;
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static PlayerInteract GetInstance()
    {
        return instance;
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
