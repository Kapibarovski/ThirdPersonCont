using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _sensitivety = 5;
    private float _rotY;
    private float _rotX;
    private Vector3 offset;
    [SerializeField] private PlayerDirection _dontJump;

    private void Start()
    {
        _rotY = transform.localScale.y;
        _rotX = transform.localScale.x;
        offset = transform.position -= _target.position;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {      
        _sensitivety = PlayerPrefs.GetFloat("Sensitivety") * 10;

        transform.LookAt(_target);
        _rotY += Input.GetAxis("Mouse X") * _sensitivety;
        _rotX += -Input.GetAxis("Mouse Y") * _sensitivety;
        _rotX = Mathf.Clamp(_rotX, -75, 35);
        Quaternion rotation = Quaternion.Euler(_rotX, _rotY, -60);
        transform.position = _target.position - (rotation * offset);
    }
}
