﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System;

public class SceneController : MonoBehaviour
{
    public static event Action<DetectedPlane> PlaneSelected;
    public static bool playing = false;

    private void OnEnable()
    {
        FoodConsumer.SelfAnnihilation += GameOver;
    }

    private void OnDisable()
    {
        FoodConsumer.SelfAnnihilation -= GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTouches();
    }

    void ProcessTouches()
    {
        if (Input.touchCount > 0)
        {
            Touch touch0 = Input.GetTouch(0);

            if (touch0.phase == TouchPhase.Began)
            {
                TrackableHit hit;
                TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;

                if (Frame.Raycast(touch0.position.x, touch0.position.y, raycastFilter, out hit) && !playing)
                {
                    playing = true;
                    Time.timeScale = 1;
                    PlaneSelected?.Invoke(hit.Trackable as DetectedPlane);
                }
            }
        }
    }

    void GameOver(BiteType biteType)
    {
        playing = false;
        Time.timeScale = 0;
    }

}
