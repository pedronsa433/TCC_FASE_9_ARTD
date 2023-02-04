using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class InsertObjectByTouch : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private GameObject placeIndicator;
    [SerializeField] private Button placeButton;
    [SerializeField] private Camera cameraWorld;


    ARRaycastManager arOrigin;
    Pose place;
    bool placeIsValid;
    GameObject gameSpawned;


    private void Start() => arOrigin = GetComponent<ARRaycastManager>();

    private void Update()
    {
        UpdatePlace();
        UpdatePlaceIndicator();
    }
    
    public void InsertObject()
    {
        if (!placeIsValid)
             return;

        if (gameSpawned == null)
        {
            GameObject newObject = Instantiate(objectPrefab, place.position, place.rotation);
            gameSpawned = newObject;
        }
        else
        {
            gameSpawned.transform.position = place.position;
        }

        Canvas[] huds = gameSpawned.GetComponentsInChildren<Canvas>();

        for (int i = 0; i < huds.Length; i++) 
            huds[i].worldCamera = cameraWorld;

        Destroy(placeIndicator);
        placeButton.gameObject.SetActive(false);
    }

    void UpdatePlaceIndicator()
    {
        if (placeIsValid)
        {
            if (placeIndicator != null) 
                placeIndicator.SetActive(true);

            placeIndicator.transform.SetPositionAndRotation(place.position, place.rotation);
            placeButton.interactable = true;
        }
        else
        {
            if (placeIndicator != null) 
                placeIndicator.SetActive(false);
            
            placeButton.interactable = false;
        }
    }

    void UpdatePlace()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        var hitList = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hitList, TrackableType.Planes);

        placeIsValid = hitList.Count > 0;

        if (placeIsValid)
        {
            place = hitList[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraNomalized = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            place.rotation = Quaternion.LookRotation(cameraNomalized);
        }
    }
}