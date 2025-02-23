using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputController: MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        private InputAction _moveAction;
        private InputAction _jumpAction;

        private void Start()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _jumpAction = InputSystem.actions.FindAction("Jump");
        }

        private void Update()
        {
            bool jumpTriggered =  _jumpAction.triggered;

            bool jumpReleased = _jumpAction.WasReleasedThisFrame();

            _playerController.OnUpdate(jumpTriggered, jumpReleased, _moveAction.ReadValue<Vector2>().x);
        }

        private void FixedUpdate()
        {
            float xDir = _moveAction.ReadValue<Vector2>().x;
            _playerController.OnFixedUpdate(xDir);
        }
    }
}