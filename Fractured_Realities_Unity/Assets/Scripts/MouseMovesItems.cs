using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovesItems : MonoBehaviour
{
    public Vector3 screenPos;
    public Vector3 worldPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rare W yt tutorial   
        screenPos = Mouse.current.position.ReadValue();
        screenPos.z = Camera.main.nearClipPlane + 1;
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);


        transform.position = worldPos;
    }
}
