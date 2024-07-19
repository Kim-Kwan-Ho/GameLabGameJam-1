using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerPolymorph : MonoBehaviour
{
    public GameObject[] shapeOptions;
    private int currentShapeIndex = 0;

    public GameObject currentShape; 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchShape();
        }
    }

    void SwitchShape()
    {
        // Destroy the current shape if it exists
        if (currentShape != null)
        {
            Destroy(currentShape);
        }

        currentShapeIndex = (currentShapeIndex + 1) % 3;

        // Instantiate the new shape based on the current shape index
        switch (currentShapeIndex)
        {
            case 0:
                currentShape = Instantiate(shapeOptions[0], transform.position, transform.rotation);
                break;
            case 1:
                currentShape = Instantiate(shapeOptions[1], transform.position, transform.rotation);
                break;
            case 2:
                currentShape = Instantiate(shapeOptions[2], transform.position, transform.rotation);
                break;
        }

        currentShape.transform.parent = transform;
    }
}
