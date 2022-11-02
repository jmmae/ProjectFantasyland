using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;

    void Update() //Update is called once per frame
    {
        float xAxis = Input.GetAxis("Horizontal");

        Vector2 movement = transform.right * xAxis;

        controller.Move(movement * speed * Time.deltaTime);
    }
}
