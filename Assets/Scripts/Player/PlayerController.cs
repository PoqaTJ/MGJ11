using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private InputAction _moveAction;
        private InputAction _jumpAction;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _anim;

        // ground check
        [SerializeField] private BoxCollider2D _groundCollider;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundedCheckLength = 0.1f;

        // stats - move to scriptableobject for fast swap between power ups?
        [SerializeField] private float acceleration = 3f;
        [SerializeField] private float deceleration = 5f;
        [SerializeField] private float airAcceleration = 3f;
        [SerializeField] private float airDeceleration = 5f;
        [SerializeField] private float maxSpeed = 10f;

        // jump
        [SerializeField] private float jumpVelocity = 10f;
        [SerializeField] private float jumpShort = 5f;
        
        private Vector2 _moveVelocity;
        private bool _facingRight = true;
        private bool _grounded;
        
        private static readonly int _groundedParam = Animator.StringToHash("grounded");
        private static readonly int _hMoveParam = Animator.StringToHash("hMove");
        private static readonly int _yMoveParam = Animator.StringToHash("yMove");

        private void Start()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _jumpAction = InputSystem.actions.FindAction("Jump");
        }

        private void FixedUpdate()
        {
            GroundCheck();

            Move(_moveAction.ReadValue<Vector2>().x);
        }

        private void Update()
        {
            if (_jumpAction.triggered)
            {
                Jump();
            }

            if (!_grounded && _jumpAction.WasReleasedThisFrame())
            {
                JumpCancel();
            }
        }

        private void GroundCheck()
        {
            Vector2 groundedOrigin = new Vector2(_groundCollider.bounds.center.x, _groundCollider.bounds.min.y);
            Vector2 groundedSize = new Vector2(_groundCollider.bounds.size.x, _groundedCheckLength);
            var _groundHit = Physics2D.BoxCast(groundedOrigin, groundedSize, 0f, Vector2.down, _groundedCheckLength, _groundLayer);
            _grounded = _groundHit.collider != null;
        }

        private void Move(float hMove)
        {
            
            float change;
            if (_grounded)
            {
                change = hMove == 0 ? deceleration : acceleration;

            }
            else
            {
                change = hMove == 0 ? airDeceleration : airAcceleration;
            }

            if (hMove < 0)
            {
                Face(false);
            }
            else if (hMove > 0)
            {
                Face(true);
            }

            Vector2 targetVelocity = new Vector2(hMove * maxSpeed, 0);
            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, change * Time.fixedDeltaTime);
            _rigidbody2D.velocity = new Vector2(_moveVelocity.x, _rigidbody2D.velocity.y);
            
            _anim.SetBool(_groundedParam, _grounded);
            _anim.SetFloat(_hMoveParam, Mathf.Abs(hMove));
            _anim.SetFloat(_yMoveParam, _rigidbody2D.velocity.y);
        }

        private void Jump()
        {
            if (_grounded)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
            }
        }

        private void JumpCancel()
        {
            if (_rigidbody2D.velocity.y > jumpShort)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpShort);
            }
        }

        private void Face(bool right)
        {
            bool rotate = false;
            if (right && !_facingRight)
            {
                _facingRight = true;
                rotate = true;
            }
            else if (!right && _facingRight)
            {
                _facingRight = false;
                rotate = true;
            }

            if (rotate)
            {
                transform.Rotate(0, 180f, 0);
            }
        }
    }
}