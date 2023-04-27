using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraCanvas : MonoBehaviour
{
    [SerializeField]
    private Transform _camera = null;

    private void Update()
    {
        transform.LookAt(_camera);
    }
}
