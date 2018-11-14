using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArrow : MonoBehaviour
{
    [SerializeField] private float minAngle = -70f;
    [SerializeField] private float maxAngle = 70f;

    private void Update()
    {
        float zRot = Input.GetAxis("Mouse X") * Time.deltaTime * 100f;
        Vector3 currentRot = transform.localRotation.eulerAngles;
        currentRot.z = Mathf.Clamp(currentRot.z + zRot, minAngle, maxAngle);
        transform.localRotation = Quaternion.Euler(currentRot);

        print(transform.localEulerAngles.z);
    }
}
