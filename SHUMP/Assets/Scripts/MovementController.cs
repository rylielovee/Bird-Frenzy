using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    Vector3 objectPosition = Vector3.zero;

    Vector3 direction = Vector3.zero;

    Vector3 velocity = Vector3.zero;     

    [SerializeField]
    float speed;


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

        // validate new calculated position
        // (not in code yet)

        transform.position = objectPosition;   // "draw" this object(the vehicle) at that position

        if (direction.x < 0) 
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
                
    }
}
