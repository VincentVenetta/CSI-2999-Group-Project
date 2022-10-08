using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Reference Variables
    private InputController inputController;
    #endregion

    #region Input Variables
    public bool upPressed { get; private set; }
    public bool upHeld { get; private set; }
    public bool upReleased { get; private set; }

    public bool downPressed { get; private set; }
    public bool downHeld { get; private set; }
    public bool downReleased { get; private set; }

    public bool leftPressed { get; private set; }
    public bool leftHeld { get; private set; }
    public bool leftReleased { get; private set; }

    public bool rightPressed { get; private set; }
    public bool rightHeld { get; private set; }
    public bool rightReleased { get; private set; }
    #endregion

    private void Awake()
    {
        inputController = new InputController();
        SetupInputs();
    }

    private void OnEnable()
    {
        inputController.Enable();
    }

    private void OnDisable()
    {
        inputController.Disable();
    }

    void Update()
    {
        GetHeldInputs();
    }

    private void LateUpdate()
    {
        ResetInputs(); //Inputs reset in LateUpdate() to allow other scripts to read them in their update method
    }

    ///<Summary>Setup callback functions for retrieving player input.</Summary>
    private void SetupInputs()
    {
        inputController.PlayerActions.Up.started += ctx => upPressed = true;
        inputController.PlayerActions.Up.canceled += ctx => upReleased = true;

        inputController.PlayerActions.Down.started += ctx => downPressed = true;
        inputController.PlayerActions.Down.canceled += ctx => downReleased = true;

        inputController.PlayerActions.Left.started += ctx => leftPressed = true;
        inputController.PlayerActions.Left.canceled += ctx => leftReleased = true;

        inputController.PlayerActions.Right.started += ctx => rightPressed = true;
        inputController.PlayerActions.Right.canceled += ctx => rightReleased = true;
    }

    ///<Summary>Find whether or not a button is being held down by the player.</Summary>
    private void GetHeldInputs()
    {
        upHeld = (inputController.PlayerActions.Up.ReadValue<float>() == 1f) ? true : false;

        downHeld = (inputController.PlayerActions.Down.ReadValue<float>() == 1f) ? true : false;

        leftHeld = (inputController.PlayerActions.Left.ReadValue<float>() == 1f) ? true : false;
        
        rightHeld = (inputController.PlayerActions.Right.ReadValue<float>() == 1f) ? true : false;
    }

    ///<Summary>Reset player input booleans to false after they're read.</Summary>
    private void ResetInputs()
    {
        upPressed = false;
        upReleased = false;

        downPressed = false;
        downReleased = false;

        leftPressed = false;
        leftReleased = false;

        rightPressed = false;
        rightReleased = false;
    }
}
