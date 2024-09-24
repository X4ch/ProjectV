using UnityEngine;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System;

using UnityEngine.InputSystem;

public class CarController: MonoBehaviour {

    [SerializeField] float maxSpeed;
    [SerializeField] float speed;
    Vector2 direction;
    [SerializeField] Rigidbody2D rb;

    private void OnForward()
    {
        speed = maxSpeed;
    }

    private void OnBackward()
    {
        speed = -maxSpeed;
    }

    private void OnSteering(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        rb.position += direction * speed;
    }
}
