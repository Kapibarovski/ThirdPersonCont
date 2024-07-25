using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerDirection : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private float _rotSpeed = 15.0f;
    private CharacterController _controller;
    [SerializeField] private float _speedValue;
    private float _speedAnimatorValue;
    [SerializeField] private float _gravitySpeed;
    private Vector3 _dir;
    private Vector3 _move;
    private float _maxSpeed;
    [SerializeField] private float _pushForce = 3.0f;
    private bool _cursorIs = false;

    private ControllerColliderHit _contact;

    [SerializeField] private float _jumpSpeed = 15.0f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _terminalVelocity = -10.0f;
    [SerializeField] private float _fall = -0.3f;

    private float _vertSpeed;

    private bool _isPersonOnGround;

    private RaycastHit _hit;

    private Animator _animController;

    private float TimeGravity;

    private void Start()
    {
        _vertSpeed = 0;

        _controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _maxSpeed = _speedValue += 5;

        _animController = GetComponent<Animator>();
    }

    private void Update()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        if (horInput != 0 || verInput != 0 && _cursorIs == false)
        {
            Vector3 right = _target.right;
            Vector3 forward = Vector3.Cross(right, Vector3.up);
            _dir = (right * horInput) + (forward * verInput);
            _move = (right * horInput) + (forward * verInput);
            _move = Vector3.ClampMagnitude(_dir, _speedValue);
            Quaternion direction = Quaternion.LookRotation(_dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, _rotSpeed * Time.deltaTime);
            _speedAnimatorValue = 1;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _speedAnimatorValue = 2;
                _speedValue += 3;
                TimeGravity = _gravity;
                if (_speedValue > _maxSpeed)
                {
                    _speedValue = _maxSpeed;
                }
            }
            else
            {
                _speedValue = 5;
            }
        }
        else
        {
            _speedAnimatorValue = 0;
        }

        _animController.SetFloat("Speed", _speedAnimatorValue);

        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out _hit))
        {
            float check = (_controller.height + _controller.radius) / 1.95f;
            _isPersonOnGround = _hit.distance <= check;
        }

        if (_isPersonOnGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = _jumpSpeed;
                _animController.SetBool("Jumping", true);           
            }
            else
            {
                _vertSpeed += _fall;
                _animController.SetBool("Jumping", false);
            }
        }
        else
        {
            _vertSpeed += _gravity * Time.deltaTime;
            if (_vertSpeed < _terminalVelocity)
            {
                _vertSpeed = _terminalVelocity;
            }

            if (_controller.isGrounded)
            {
                if (Vector3.Dot(_move, _contact.normal) < 0)
                {
                    _move = _contact.normal * _speedValue;
                }
                else
                {
                    _move += _contact.normal * _speedValue;
                }
            }
        }
        _move *= _speedValue;
        _move.y = _vertSpeed * _gravitySpeed;
        _move *= Time.deltaTime;
        _controller.Move(_move);

        if(Input.GetKeyDown(KeyCode.O))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _cursorIs = false;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            _cursorIs = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;

        if(body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * _pushForce;
        }
    }
}

