using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private float _radius = 1.5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] _hitColliders;
            _hitColliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (Collider collider in _hitColliders)            
                if (Physics.Raycast(transform.position, transform.forward, _radius))               
                    collider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);           
        }
    }
}
