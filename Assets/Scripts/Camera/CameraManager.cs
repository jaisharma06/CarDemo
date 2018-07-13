using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour {
    [SerializeField]
    private float cameraMoveSpeed = 2f;
    [SerializeField]
    private float cameraFarView = -15f;
    [SerializeField]
    private float cameraNearView = -10f;
    [SerializeField]
    private Camera mainCamera;

    public CameraMode mode { get; set; }

    private Coroutine cameraMoveCoroutine;

    private void Start()
    {
        if (!mainCamera)
            mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleCameraMode"))
        {
            StartMovingCamera();
        }
    }

    private void StartMovingCamera()
    {
        if (cameraMoveCoroutine != null)
        {
            StopCoroutine(cameraMoveCoroutine);
        }
        cameraMoveCoroutine = StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        var tarPos = new Vector3(0, transform.position.y, 1);

        if (mode == CameraMode.Far)
        {
            tarPos.z = cameraNearView;
            mode = CameraMode.Near;
        }
        else
        {
            tarPos.z = cameraFarView;
            mode = CameraMode.Far;
            EventManager.CameraMovedAway();
        }

        while (transform.position != tarPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, tarPos, Time.deltaTime * cameraMoveSpeed);
            yield return 0;
        }
    }

}
