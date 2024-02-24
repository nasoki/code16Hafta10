using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImagePlacing : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;
    public GameObject[] ARPrefabs;
    private readonly Dictionary<string, GameObject> instantiatedPrefabs = new Dictionary<string, GameObject>();
    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;

    }
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            var imageName = trackedImage.referenceImage.name;
            foreach (var curPrefab in ARPrefabs)
            {
                if (string.Compare(curPrefab.name,imageName, System.StringComparison.OrdinalIgnoreCase) == 0 && !instantiatedPrefabs.ContainsKey(imageName))
                {
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    //instantiatedPrefabs.Add(imageName, newPrefab);
                    instantiatedPrefabs[imageName] = newPrefab;
                }
            }
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }
        foreach (var trackedImage in eventArgs.removed)
        {
            Destroy(instantiatedPrefabs[trackedImage.referenceImage.name]);
            instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
        }
    }
}
