using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPlacer : MonoBehaviour
{
    [SerializeField] GameObject brickPrefab;
    [SerializeField] GameObject brickReticle;
    [SerializeField] Color brickColor;

    GameObject brickBase;
    bool brickPlacerEnabled = false;

    bool canPlaceBrick = true;

    public void EnableBrickPlacer()
    {
        brickBase = GameObject.Find("WallBase");
        brickPlacerEnabled = true;
        InitBrickReticle();
    }

    void InitBrickReticle()
    {
        brickReticle.SetActive(true);
        brickReticle.transform.parent = brickBase.transform;
        brickReticle.transform.localPosition = Vector3.zero;
    }

    public void ChangeSize(float x)
    {
        Vector3 newScale = brickReticle.transform.localScale;
        newScale.y = x;
        newScale.x = x;

        Vector3 newPos = brickReticle.transform.localPosition;
        newPos.y += newScale.y;
        brickReticle.transform.localPosition = newPos;
    }

    public void ChangeColor()
    {
        brickColor = Random.ColorHSV();
        Renderer renderer = brickReticle.GetComponent<Renderer>();
        renderer.material.color = brickColor;
    }

    public void UpdateReticalPosition(float x)
    {
        Vector3 newPos = brickReticle.transform.localPosition;
        newPos.x += x;
    }

    public void Place()
    {
        if (canPlaceBrick)
        {
            GameObject spawnedBrick = Instantiate(brickPrefab, brickReticle.transform.localPosition, brickReticle.transform.rotation, brickReticle.transform);
            Renderer renderer = spawnedBrick.GetComponent<Renderer>();
            renderer.material.color = brickColor;
        }
    }
}
