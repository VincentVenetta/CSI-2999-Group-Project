using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputController inputController;

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

    // Called when the script is loaded or enabled
    private void Awake()
    {
        inputController = new InputController();
        SetupInputs();
    }

    // Called when the script is enabled
    private void OnEnable()
    {
        inputController.Enable();
    }

    // Called when the script is disabled
    private void OnDisable()
    {
        inputController.Disable();
    }

    // Called once per frame
    void Update()
    {
        GetHeldInputs();
    }

    // Called towards the end of execution order
    private void LateUpdate()
    {
        ResetInputs(); //Inputs reset in LateUpdate() to allow other scripts to read them on time
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
