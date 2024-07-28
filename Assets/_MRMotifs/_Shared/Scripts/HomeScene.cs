using UnityEngine;

public class HomeScene : MonoBehaviour
{
    [SerializeField] private OVRPassthroughLayer oVRPassthroughLayer;

    private Camera _mainCamera;
    private Color _skyboxBackgroundColor;

    private void Awake()
    {
        _mainCamera = Camera.main;

        if (_mainCamera != null)
        {
            _skyboxBackgroundColor = _mainCamera.backgroundColor;
        }

#if UNITY_ANDROID
        CheckIfPassthroughIsRecommended();
#endif
    }

    private void CheckIfPassthroughIsRecommended()
    {
        if (_mainCamera == null)
        {
            return;
        }

        if (OVRManager.IsPassthroughRecommended())
        {
            oVRPassthroughLayer.enabled = true;
            _mainCamera.clearFlags = CameraClearFlags.SolidColor;
            _mainCamera.backgroundColor = Color.clear;
        }
        else
        {
            oVRPassthroughLayer.enabled = false;
            _mainCamera.clearFlags = CameraClearFlags.Skybox;
            _mainCamera.backgroundColor = _skyboxBackgroundColor;
        }
    }
}
