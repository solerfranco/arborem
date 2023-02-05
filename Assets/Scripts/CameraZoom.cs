using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    private new Camera camera;
    public float zoomSpeed;
    public float maxSize;
    public float moveSpeed;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (camera.orthographicSize < maxSize)
        {
            camera.orthographicSize += zoomSpeed * Time.deltaTime;
        } else if(Time.timeSinceLevelLoad < 60)
        {
            camera.transform.position += Vector3.down * Time.deltaTime * moveSpeed;
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //Vector2 mousePos = camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            //RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            //if (hit)
            //{
            //    hit.transform.GetComponent<Trash>().OnMouseDown();
            //}
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.transform.GetComponent<Trash>() != null)
                {
                    hit.transform.GetComponent<Trash>().OnMouseDown();
                }
            }
        }
    }

}
