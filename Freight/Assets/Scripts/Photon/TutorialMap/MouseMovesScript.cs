﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovesScript : MonoBehaviour
{
    Vector3 aPos;
    float movementSpeed;

    private float moveTime = 0.0f;
    private float onScreenTime = 0.0f;
    private float movementTime = 3.0f;

    private float direction = -1f;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        onScreenTime += Time.deltaTime;
        if (onScreenTime > moveTime) {
            moveTime = onScreenTime + movementTime;
            direction = direction * -1f;
        }
        transform.position = new Vector3(transform.position.x + 0.5f * direction, transform.position.y, transform.position.z);
        if(onScreenTime > 10f) {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
