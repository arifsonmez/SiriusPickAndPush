using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollow : MonoBehaviour
{
    public GameObject followTarget;
    public Vector2 offset;

    new private Camera camera;

    void Start()
    {
        camera = GetComponentInParent<Canvas>().worldCamera;
    }

    void Update()
    {
        ((RectTransform)transform).anchoredPosition = camera.WorldToScreenPoint(followTarget.transform.position) + (Vector3)offset;
    }
}
