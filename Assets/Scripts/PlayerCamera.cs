using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public float xRotation;
    public float yRotation;

    public Vector2 lookInput;
    public GameObject weaponHolder;

    public float step = 0.01f;
    public float rotationStep = 4f;
    public float maxRotationStep = 5f;
    public float maxStepDist = 0.06f;
    Vector3 swayPos;
    Vector3 swayEulerRot;
    public float smooth = 3f;
    public float smoothRot = 3f;
    public PlayerMovement bobValues;

    private void Update()
    {
        sensX = StaticData.sens;
        sensY = StaticData.sens;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        lookInput.x = mouseX;
        lookInput.y = mouseY;

        Sway();
        SwayRotation();
        CompositePositionRotation();

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    void Sway()
    {
        Vector3 invertLook = lookInput * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDist, maxStepDist);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDist, maxStepDist);

        swayPos = invertLook;
    }
    void SwayRotation()
    {
        Vector2 invertLook = lookInput * -rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);

        swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }
    void CompositePositionRotation()
    {
        weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, swayPos + bobValues.bobPosition, Time.deltaTime * smooth);
        weaponHolder.transform.localRotation = Quaternion.Slerp(weaponHolder.transform.localRotation, Quaternion.Euler(swayEulerRot) * Quaternion.Euler(bobValues.bobEulerRotation), Time.deltaTime * smoothRot);
    }
}
