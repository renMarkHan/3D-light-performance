using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class AircraftController : MonoBehaviour
{
    float circleRadius;
    float circleSpeed;
    float circleAngle;
    float selfRotationSpeed;
    int rotationDirection;
    bool gameOn;
    bool canForward;
    bool inGap;
    public GameController gameController;
    public GameObject directionalLight;
    public GameObject greenLight;
    public GameObject blueLight;
    Vector3 lastDirection;

    void Start()
    {

        gameOn = true;
        blueLight.SetActive(false);
        greenLight.SetActive(false);
        RenderSettings.ambientSkyColor = Color.black;
        canForward = true;
        inGap = false;
        circleRadius = 30;
        circleSpeed = 0.5f;
        circleAngle = 0;
        selfRotationSpeed = 100;

        rotationDirection = 0;

        lastDirection = new Vector3(1, 0, 0);
        lastDirection.Normalize();
        Move();
        Move();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
      
        if (gameOn)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (RenderSettings.ambientSkyColor == Color.black)
                    RenderSettings.ambientSkyColor = Color.red;
                else if (RenderSettings.ambientSkyColor == Color.red)
                    RenderSettings.ambientSkyColor = Color.black;

            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                directionalLight.SetActive(!directionalLight.activeSelf);

            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                greenLight.SetActive(!greenLight.activeSelf);

            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                blueLight.SetActive(!blueLight.activeSelf);

            }

            if (Input.GetKey("right"))
            {
                rotationDirection = -1;
                transform.Rotate(Vector3.forward, selfRotationSpeed * rotationDirection * Time.deltaTime, Space.Self);
            }

            else if (Input.GetKey("left"))
            {
                rotationDirection = 1;
                transform.Rotate(Vector3.forward, selfRotationSpeed * rotationDirection * Time.deltaTime, Space.Self);
            }
            if (canForward)
            {
                if (Input.GetKey("up"))
                {
                    Move();

                }
            }
        }
        
    }
    void Move()
    {
        circleAngle += circleSpeed * Time.deltaTime;

        circleAngle = (circleAngle + 360) % 360;

        float newPositionX = circleRadius * (float)Mathf.Cos(circleAngle);
        float newPositionZ = circleRadius * (float)Mathf.Sin(circleAngle);

        Vector3 newPosition = new Vector3(newPositionX, transform.position.y, newPositionZ);
        Vector3 newDirection = newPosition - transform.position;
        float rotationAngle = -Vector3.Angle(lastDirection, newDirection);
        newDirection.Normalize();
        transform.Rotate(Vector3.up, rotationAngle, Space.World);
        transform.position = newPosition;

        lastDirection = newDirection;
    }

    void OnTriggerStay(Collider c)
    {
   
            canForward = false;
         
        if (c.gameObject.tag != "Obstacle")
        {
     
            canForward = true;
            inGap = true;

        }


    }

    void OnTriggerExit(Collider c)
    {
        canForward = true;
        if (inGap == true) {
            inGap = false;

            gameController.Score(c.gameObject,gameObject);
        }
    }

    public void Stop()
    {
        gameOn = false;
    }
}
