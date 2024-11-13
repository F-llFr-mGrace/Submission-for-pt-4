using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Scrolling speed control")]
    [SerializeField] InputAction movement;
    [SerializeField] float speed;

    float throwX;
    float throwY;

    [Header("Ship traversal clamp")]
    [SerializeField] float clampX;
    [SerializeField] float clampY;

    [Header("Ship leading factor")]
    [SerializeField] float posPitchFactor;
    [SerializeField] float ctrlPitchFactor;

    [SerializeField] float posYawFactor;
    [SerializeField] float ctrlRollFactor;

    [Header("Lasers firing setup")]
    [SerializeField] ParticleSystem[] allLasers;
    [SerializeField] InputAction shoot;
    [SerializeField] ParticleSystem lasers;

    [Header("Collision")]
    [SerializeField] BoxCollider thisCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        movement.Enable();
        shoot.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        shoot.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        MoveShip();
        RotateShip();

        //Debug.Log(shoot.ReadValue<float>());

        if (shoot.ReadValue<float>() > .5)
        {
            if (!allLasers[0].isEmitting)
            {
                foreach (var lasers in allLasers)
                {
                    lasers.Play();
                }
            }
        }
        else
        {
            foreach (var lasers in allLasers)
            {
                lasers.Stop();
            }
        }
    }

    private void RotateShip()
    {
        float pitch = transform.localPosition.y * -posPitchFactor + throwY * -ctrlPitchFactor;
        float yaw = transform.localPosition.x * posYawFactor;
        float roll = throwX * -ctrlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void MoveShip()
    {
        throwX = movement.ReadValue<Vector2>().x * speed * 10 * Time.deltaTime;
        throwY = movement.ReadValue<Vector2>().y * speed * 10 * Time.deltaTime;

        /*
        float horizontalThrow = Input.GetAxis("Horizontal");
        float verticalThrow = Input.GetAxis("Vertical");
        */

        //Debug.Log(throwX + ", " + throwY);

        transform.localPosition = new Vector3
            (
            Mathf.Clamp((transform.localPosition.x + throwX), -clampX, clampX),
            Mathf.Clamp((transform.localPosition.y + throwY), -clampY, clampY),
            transform.localPosition.z
            );
    }
}
