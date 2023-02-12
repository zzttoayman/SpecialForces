using UnityEngine;

public class InvisibleSphere : MonoBehaviour
{
    public float radius = 10.0f;
    public LayerMask layerMask;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        if (_camera == null)
        {
            _camera = gameObject.AddComponent<Camera>();
        }

        _camera.cullingMask = layerMask;
        _camera.nearClipPlane = 0;
        _camera.farClipPlane = radius;
        _camera.orthographic = true;
        _camera.orthographicSize = radius;
        _camera.clearFlags = CameraClearFlags.Nothing;
        _camera.backgroundColor = Color.clear;
        _camera.depth = -1;
        _camera.renderingPath = RenderingPath.Forward;
    }
}
