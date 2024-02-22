using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    Vector3 objectPosition = Vector3.zero;   // Vector3 with all zeros (0, 0, 0)

    Vector3 direction = Vector3.zero;

    Vector3 velocity = Vector3.zero;     // velocity doesn't make sense outside of update but to debug
                                         // put it here to see in inspector
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

        // validate new calculated position
        // (not in code yet)

        transform.position = objectPosition;   // "draw" this object(the vehicle) at that position


        StopAtEdges();
    }


    void StopAtEdges()
    {
        Vector3 sq = Vector3.zero;
        sq.x = Screen.width;    // this is in pixels
        sq.y = Screen.height;   // this is in pixels

        Vector3 objPix = Camera.main.ScreenToWorldPoint(sq);  // changes pixels into coords

        if ((objectPosition.x+1 > 4))  // this one to stop before the edge
        {
            objectPosition.x = objectPosition.x - 0.1f;
            transform.position = objectPosition;
        }

        if ((objectPosition.x-1 < -objPix.x))
        {
            objectPosition.x = objectPosition.x + 0.1f;
            transform.position = objectPosition;
        }

        if ((objectPosition.y+0.8f > objPix.y))
        {
            objectPosition.y = objectPosition.y - 0.1f;
            transform.position = objectPosition;
        }

        if ((objectPosition.y-0.8f < -objPix.y))
        {
            objectPosition.y = objectPosition.y + 0.1f;
            transform.position = objectPosition;
        }

    }
}
