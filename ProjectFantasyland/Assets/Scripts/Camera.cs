using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform player;
    private GameObject cameraMovement;
    public float minX = -11.5f;
    public float maxX = 40;

    void Start() {
        cameraMovement = GameObject.Find("Main Camera");
    }

    void Update() {
        //Clamp camera to only move between two points in the x axis
        cameraMovement.transform.position = new Vector3(Mathf.Clamp(player.position.x, minX, maxX), transform.position.y, transform.position.z);
    }
}
