using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject visualIndicator;
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        visualIndicator = transform.GetChild(0).gameObject;
        visualIndicator.SetActive(false);
    }
    private void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if(hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            if(!visualIndicator.activeInHierarchy) 
            { 
                visualIndicator.SetActive(true);
            }
        }
    }
}
