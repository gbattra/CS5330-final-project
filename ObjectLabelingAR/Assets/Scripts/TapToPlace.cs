using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    [SerializeField] private GameObject placementObject;
    
    private ARRaycastManager _arRaycastManager;
    private Pose _placementPose;

    private void Start()
    {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        UpdatePlacementPose();
    }

    private void UpdatePlacementPose()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (Input.touchCount == 0)
            return;

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;
        
        if (_arRaycastManager.Raycast(
            touch.position,
            hits, 
            TrackableType.Planes))
        {
            _placementPose = hits[0].pose;
            Instantiate(placementObject, _placementPose.position, _placementPose.rotation);
        }
    }
}
