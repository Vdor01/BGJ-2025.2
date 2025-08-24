using UnityEngine;
using UnityEngine.InputSystem;

namespace BGJ_2025_2.Game.Players
{
    /// <summary>
    /// A játékostól érkezõ inputokat kezeli.
    /// </summary>
    /// <seealso cref="Player"/>
    [AddComponentMenu("BGJ 2025.2/Game/Players/Player input")]
    public class PlayerInput : PlayerComponent
    {
        // Fields
        private Inputs _inputs;


        // Methods
        private void Awake()
        {
            _inputs = new Inputs();

            _inputs.Player.Look.performed += callbackContext => HandleLooking(callbackContext);
            _inputs.Player.Look.canceled += callbackContext => HandleLooking(callbackContext);

            _inputs.Player.Walk.performed += callbackContext => HandleWalking(callbackContext);
            _inputs.Player.Walk.canceled += callbackContext => HandleWalking(callbackContext);

            _inputs.Player.Run.performed += callbackContext => HandleRunning(callbackContext);
            _inputs.Player.Run.canceled += callbackContext => HandleRunning(callbackContext);

            _inputs.Player.Grab.performed += callbackContext => HandleGrabbing(callbackContext);
            _inputs.Player.Grab.canceled += callbackContext => HandleGrabbing(callbackContext);

            _inputs.Player.Place.performed += callbackContext => HandlePlacing(callbackContext);
            _inputs.Player.Place.canceled += callbackContext => HandlePlacing(callbackContext);

            _inputs.Player.Throw.performed += callbackContext => HandleThrowing(callbackContext);
            _inputs.Player.Throw.canceled += callbackContext => HandleThrowing(callbackContext);

            _inputs.Player.Use.performed += callbackContext => HandleUsage(callbackContext);
            _inputs.Player.Use.canceled += callbackContext => HandleUsage(callbackContext);
        }

        private void OnEnable()
        {
            _inputs.Enable();
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }

        private void HandleLooking(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                _player.View.Look(callbackContext.ReadValue<Vector2>());
            }
        }

        private void HandleWalking(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                _player.Movement.StartWalking(callbackContext.ReadValue<Vector2>());
            }
            else if (callbackContext.canceled)
            {
                _player.Movement.StopWalking();
            }
        }

        private void HandleRunning(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                _player.Movement.StartRunning();
            }
            else if (callbackContext.canceled)
            {
                _player.Movement.StopRunning();
            }
        }

        private void HandleGrabbing(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                _player.Interaction.Grab();
            }
        }

        private void HandlePlacing(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                _player.Interaction.Place();
            }
        }

        private void HandleThrowing(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                _player.Interaction.Throw();
            }
        }

        private void HandleUsage(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                _player.Interaction.Use();
            }
        }
    }
}