using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMove : MonoBehaviour
{
    private float moveSpeed = 2.0f;
    public Animator animatorController;
    private float currentTime = 0.0f;
    private Vector3 movementDirection = Vector3.left;
    public AudioSource mySound;
    private float lastPlayTime;

    private void Start()
    {
        lastPlayTime = Time.time;
    }
    void Update()
    {
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);

        currentTime += Time.deltaTime;
        if (currentTime >= 1.25f)
        {
            animatorController.SetTrigger("TurnUp");
        }
        if (currentTime >= 3.25f)
        {
            animatorController.ResetTrigger("TurnUp");
            animatorController.SetTrigger("TurnRight");
        }
        if (currentTime >= 5.75f)
        {
            animatorController.ResetTrigger("TurnRight");
            animatorController.SetTrigger("TurnDown");
        }
        if (currentTime >= 7.75f)
        {
            animatorController.ResetTrigger("TurnDown");
            animatorController.SetTrigger("TurnLeft");
        }
        if (currentTime >= 9.0f)
        {
            animatorController.ResetTrigger("TurnLeft");
            currentTime = 0.0f;
        }

        if (Time.time - lastPlayTime >= 0.5f)
        {
            mySound.Play();
            lastPlayTime = Time.time;
        }
    }
}
