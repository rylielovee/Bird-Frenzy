using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    Vector3 objectPosition = Vector3.zero;   // Vector3 with all zeros (0, 0, 0)

    [SerializeField]
    Vector3 direction;

    [SerializeField]
    Vector3 velocity;

    [SerializeField]
    float speed;   // whatever speed we put in inspector will override this


    public Vector3 Direction
    {
        set { direction = value.normalized; }
    }


    void Start()
    {
        objectPosition = transform.position;   // sets object to the position we put it in the editor
    }


    void Update()
    {
        velocity = direction * speed * Time.deltaTime;   // velocity is direction * speed * deltaTime

        objectPosition += velocity;   // add velocity to position

        transform.position = objectPosition;   // "draw" this object(the vehicle) at that position

    }


} 