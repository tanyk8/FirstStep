using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script acts as a single point for all other scripts to get
// the current input from. It uses Unity's new Input System and
// functions should be mapped to their corresponding controls
// using a PlayerInput component with Unity Events.

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;
    private bool interactPressed = false;
    private bool submitPressed = false;
    private bool menuPressed = false;
    private bool switchPressed = false;

    private static InputManager instance;

    private void Awake()
    {
        //if (instance != null)
        //{
        //    Debug.LogWarning("Found more than one Input Manager in the scene.");
        //    //Destroy(gameObject);
        //}
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static InputManager getInstance()
    {
        return instance;
    }

    public void movePressed(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
    }

    public void interactButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

    public void submitButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    public void menuButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            menuPressed = true;
        }
        else if (context.canceled)
        {
            menuPressed = false;
        }
    }

    public void switchButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switchPressed = true;
        }
        else if (context.canceled)
        {
            switchPressed = false;
        }
    }

    public Vector2 getMoveDirection()
    {
        return moveDirection;
    }

    // for any of the below 'Get' methods, if we're getting it then we're also using it,
    // which means we should set it to false so that it can't be used again until actually
    // pressed again.

    public bool getInteractPressed()
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool getSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public bool getMenuPressed()
    {
        bool result = menuPressed;
        menuPressed = false;
        return result;
    }

    public bool getSwitchPressed()
    {
        bool result = switchPressed;
        switchPressed = false;
        return result;
    }

    public void registerSubmitPressed()
    {
        submitPressed = false;
    }

}
