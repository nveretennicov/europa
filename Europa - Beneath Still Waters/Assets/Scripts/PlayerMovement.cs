using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private MouseLook mouseLookScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        var x = input.x;
        var z = input.y;
        Vector3 raw_move = transform.right * x + transform.forward * z;
        Vector3 move = Vector3.zero;
        
        move = raw_move * speed * Time.deltaTime;
        rb.velocity = move;

        if (move != Vector3.zero)
        {
            mouseLookScript.StartBobbing();
        }
        else
        {
            mouseLookScript.StopBobbing();
        }
    }
}