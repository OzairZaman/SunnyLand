using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float movementSpeed = 10f;
    public float gravity = -20f;
    public CharacterController2D controller;
    private Vector3 motion; //
    private void Reset()
    {
        controller = GetComponent<CharacterController2D>();
    }



    // Update is called once per frame
    void Update()
    {
        float inoutH = Input.GetAxis("Horizontal");
        motion.x = inoutH * movementSpeed;
        if (controller.isGrounded)
        {
            motion.y = 0f;
        }
        motion.y += gravity * Time.deltaTime;
        controller.move(motion * Time.deltaTime);
    }
}
