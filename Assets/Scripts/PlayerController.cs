using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float baseSpeed = 20f;

    [SerializeField] ParticleSystem spinEffect;
    [SerializeField] AudioClip spinSFX;

    int flipAmount = 0;

    // float startZ = 0;
    // float endZ = 0;

    // float startAngle = 0f;
    // float endAngle = 0f;

    Vector2 _previousRight;
    float _angle;
    bool _isGrounded = false;
    bool canMove = true;

    // bool isFlipping = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }


    void Update()
    {
        if (canMove) {
            RotatePlayer();
            RespondToBoost();
            if(!_isGrounded) CalculateSpins();
        }

        // Debug.Log(endAngle - startAngle);

        // Debug.Log("z: " + transform.rotation.z);
        // Debug.Log(endZ - startZ);
        // if ((endZ - startZ > 360) || (endZ - startZ < -360)) {
        //     endZ = 0;
        //     startZ = 0;
        //     flipAmount++;
        // }
        // Debug.Log("성공횟수 " + flipAmount);

        // if ((endAngle - startAngle > 360) || (endAngle - startAngle < -360)) {
        //     endAngle = 0;
        //     startAngle = 0;
        //     flipAmount++;
        // }

        // if (endAngle >= 360)
        // {
        //     flipAmount++;
        // }

        Debug.Log("성공횟수 " + flipAmount);
    }

    void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            _previousRight = transform.right;
            _angle = 0;
            _isGrounded = false;
        }
        
    }

    void CalculateSpins(){
        var currentRight = transform.right;
        _angle += Vector2.SignedAngle(_previousRight, currentRight);
        _previousRight = currentRight;
        // if (Math.Abs(_angle) >= 360f)
        // {
        //     flipAmount++;
        //     spinEffect.Play();
        //     GetComponent<AudioSource>().PlayOneShot(spinSFX);
        //     _angle -= 360f * Mathf.Sign(_angle);
        // }
        if (Math.Abs(_angle) >= 300f)
        {
            flipAmount++;
            spinEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(spinSFX);
            _angle -= 300f * Mathf.Sign(_angle);
        }

    }

    public void DisableControls()
    {
        canMove = false;
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
            // _previousRight = transform.right;

            // if (!isFlipping)
            // {
            //     isFlipping = true;
            //     // startAngle = transform.eulerAngles.z;
            //     // Debug.Log("startAngle: " + startAngle);
            // }
                
            // startZ = transform.rotation.z;

            // Debug.Log("startZ: " + startZ);
            // Debug.Log("startZ: " + startZ);
            // if ((transform.rotation.z > -40f) && (transform.rotation.z < 40f)) {
            //     flipAmount++;
            // }
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
            // _previousRight = transform.right;
            // if (!isFlipping)
            // {
            //     isFlipping = true;
            //     // startAngle = transform.eulerAngles.z;
            //     // Debug.Log("startAngle: " + startAngle);
            // }
            // startZ = transform.rotation.z;
            // Debug.Log("startZ: " + startZ);
        }
        // else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        // {
        //     // endZ = transform.rotation.z;
        //     // var currentRight = transform.right;
        //     // _angle += Vector2.SignedAngle(_previousRight, currentRight);
        //     // _previousRight = currentRight;
        //     // if (Math.Abs(_angle) >= 360f)
        //     // {
        //     //     flipAmount++;
        //     //     _angle -= 360f * Mathf.Sign(_angle);
        //     // }

        //     if (isFlipping)
        //     {
        //         isFlipping = false;
        //         // endAngle = transform.eulerAngles.z;
        //         // Debug.Log("endAngle: " + endAngle);

        //         // if (endAngle - startAngle >= 300)
        //         // {
        //         //     flipAmount++;
        //         // }
        //     }
            
        //     // Debug.Log("endZ: " + startZ);
        //     // Debug.Log(transform.eulerAngles.z);
        // }
        
    }
}
