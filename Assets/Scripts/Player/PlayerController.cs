using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private InputAction _moveAction;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float acceleration = 5f;
        [SerializeField] private float deceleration = 20f;
        [SerializeField] private float maxSpeed = 15f;
        
        private Vector2 _moveVelocity;
        private Vector2 _facingRight;

        private void Start()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
        }

        private void FixedUpdate()
        {
            float hMove = _moveAction.ReadValue<Vector2>().x;
            float change = hMove == 0 ? deceleration : acceleration;

            Vector2 targetVelocity = new Vector2(hMove * maxSpeed, 0);
            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, change * Time.fixedDeltaTime);
            _rigidbody2D.velocity = new Vector2(_moveVelocity.x, _rigidbody2D.velocity.y);
        }
    }
}