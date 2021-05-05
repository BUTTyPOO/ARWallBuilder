using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlanePlacement : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] GameObject placementReticle;
    [SerializeField] GameObject wallBase;

    public UnityEvent OnWallBasePlaced;

    ARRaycastManager arRayMan;
    Pose placementAim;
    GameObject spawnedWallBase;
    bool isPlacementAimValid = false;

    // Start is called before the first frame update
    void Start()
    {
        arRayMan = FindObjectOfType<ARRaycastManager>();
    }
    
    public void SpawnWallBase() // called by UI button
    {
        if (spawnedWallBase == null && isPlacementAimValid)
        {
            spawnedWallBase = Instantiate(wallBase, placementAim.position, placementAim.rotation);
            OnWallBasePlaced?.Invoke();
        }
        Destroy(this);  // Destroy this script because we don't need it anymore
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementRetical();
        UpdatePlacementAim();   
    }

    void UpdatePlacementRetical()
    {
        if (spawnedWallBase == null && isPlacementAimValid)
        {
            placementReticle.SetActive(true);

            // Set Transform
            placementReticle.transform.SetPositionAndRotation(placementAim.position, placementAim.rotation);
        }
        else
        {
            placementReticle.SetActive(false);
        }
    }

    void UpdatePlacementAim()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRayMan.Raycast(screenCenter, hits, TrackableType.Planes);

        isPlacementAimValid = hits.Count > 0;
        if (isPlacementAimValid)
            placementAim = hits[0].pose;
    }

}
