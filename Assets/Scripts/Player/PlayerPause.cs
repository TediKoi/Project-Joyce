using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPause : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            GameManager.GetInstance().isPaused = true;
            UIManager.GetInstance().PauseMenu();
        }
    }
}
