using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitWorkgroundToCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);

        Tuple<Vector3, Vector3> sprRendererPos = GetSpriteBounds();
        Vector3 sprBottomLeft = sprRendererPos.Item1;
        Vector3 sprTopRight = sprRendererPos.Item2;

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(Vector3.zero);
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(mainCamera.rect.width, mainCamera.rect.height));
        Vector3 screenSize = topRight - bottomLeft;
        float screenRatio = screenSize.x / screenSize.y;
        float desiredRatio = transform.localScale.x / transform.localScale.y;

        if (screenRatio > desiredRatio)
        {
            //float height = screenSize.y;
            //transform.localScale = new Vector3(height * desiredRatio, height);
            float screenY_diff = topRight.y - bottomLeft.y;
            float workgroundY_diff = sprTopRight.y - sprBottomLeft.y;
            float scaleFactor = screenY_diff / workgroundY_diff;
            transform.localScale = new Vector3(transform.localScale.x * scaleFactor, transform.localScale.y * scaleFactor);
        }
        else
        {
            //float width = screenSize.x;
            //transform.localScale = new Vector3(width, width / desiredRatio);
            float screenX_diff = topRight.x - bottomLeft.x;
            float workgroundX_diff = sprTopRight.x - sprBottomLeft.x;
            float scaleFactor = screenX_diff / workgroundX_diff;
            transform.localScale = new Vector3(transform.localScale.x * scaleFactor, transform.localScale.y * scaleFactor);
        }
    }

    Tuple<Vector3, Vector3> GetSpriteBounds()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            Vector2 spriteSize = spriteRenderer.bounds.size;

            Vector2 spritePosition = transform.position;

            Vector2 bottomLeft = new Vector2(spritePosition.x - spriteSize.x / 2, spritePosition.y - spriteSize.y / 2);
            Vector2 topRight = new Vector2(spritePosition.x + spriteSize.x / 2, spritePosition.y + spriteSize.y / 2);

            Debug.Log("Bottom Left: " + bottomLeft);
            Debug.Log("Top Right: " + topRight);

            return new Tuple<Vector3, Vector3>(bottomLeft, topRight);
        }
        else
        {
            Debug.LogError("SpriteRenderer not found on this GameObject");
        }

        return new Tuple<Vector3, Vector3>(Vector3.zero, Vector3.zero);
    }

    //void Start()
    //{
    //    // Get the main camera
    //    mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    //    // Move the workground to be aligned with the camera's position
    //    transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);

    //    // Get the world coordinates of the bottom left and top right corners of the camera's viewport
    //    Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
    //    Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

    //    // Calculate the size of the camera's view in world units
    //    Vector3 screenSize = topRight - bottomLeft;

    //    // Calculate the screen aspect ratio and the desired aspect ratio of the workground
    //    float screenRatio = screenSize.x / screenSize.y;
    //    float desiredRatio = transform.localScale.x / transform.localScale.y;

    //    // Adjust the scale of the workground based on the comparison of ratios
    //    if (screenRatio > desiredRatio)
    //    {
    //        // The screen is wider than the workground, scale by height
    //        float scaleFactor = screenSize.y / transform.localScale.y;
    //        transform.localScale = new Vector3(transform.localScale.x * scaleFactor, transform.localScale.y * scaleFactor, transform.localScale.z);
    //    }
    //    else
    //    {
    //        // The screen is taller or equal to the workground, scale by width
    //        float scaleFactor = screenSize.x / transform.localScale.x;
    //        transform.localScale = new Vector3(transform.localScale.x * scaleFactor, transform.localScale.y * scaleFactor, transform.localScale.z);
    //    }
    //}
}
