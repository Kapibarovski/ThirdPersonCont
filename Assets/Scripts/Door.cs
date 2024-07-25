using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 upd;

    private bool _isOpen = false;

    public void Operate()
    {
        if (_isOpen)
        {
            Vector3 pos = transform.position - upd;
            transform.position = pos;
        }
        else
        {
            Vector3 pos = transform.position + upd;
            transform.position = pos;
        }
        _isOpen = !_isOpen;
    }

    public void Activate()
    {
        if (!_isOpen)
        {
            Vector3 pos = transform.position + upd;
            transform.position = pos;
            _isOpen = true;
        }
    }
    public void Disactivate()
    {
        if(_isOpen)
        {
            Vector3 pos = transform.position - upd;
            transform.position = pos;
            _isOpen = false;
        }     
    }
}
