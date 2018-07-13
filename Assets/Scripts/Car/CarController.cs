using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private CameraManager cm;
    [SerializeField]
    private MeshRenderer mr;
    [SerializeField]
    private Color[] colors;
    [SerializeField]
    private float rotSpeed = 50f;
    [SerializeField]
    private string bodyMaterial = "dbl_WITHOUT_PE_002";
    [SerializeField]
    private string bonnetMaterial = "dbl_bonnet_ok_001";
    private Animator animator;

    private bool carDoorsOpen = false;
    private bool isRotating = true;

    private void OnEnable()
    {
        EventManager.OnCameraMoveAway += CloseDoors;
    }

    // Use this for initialization
    void Start()
    {
        if (!cm)
            cm = FindObjectOfType<CameraManager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ContinousRotation();

        if (Input.GetButtonDown("ToggleDoors"))
        {
            if (cm.mode == CameraMode.Near)
            {
                carDoorsOpen = !carDoorsOpen;
                if (!animator)
                    return;
                animator.SetBool("doorsOpen", carDoorsOpen);
            }
        }
        else if (Input.GetButtonDown("ChangeColor"))
        {
            ChangeColor();
        }
        else if (Input.GetButton("Rotate"))
        {
            RotateCar();
        }
        else if (Input.GetButtonDown("ToggleRotation"))
        {
            isRotating = !isRotating;
        }
    }

    private void OnDisable()
    {
        EventManager.OnCameraMoveAway -= CloseDoors;
    }

    private void CloseDoors()
    {
        carDoorsOpen = false;
        animator.SetBool("doorsOpen", carDoorsOpen);
    }

    private void ChangeColor()
    {
        var materials = mr.sharedMaterials;
        var color = GetRandomColor();
        for (int i = 0; i < materials.Length; i++)
        {
            var material = materials[i];
            if (material.name == bodyMaterial || material.name == bonnetMaterial)
            {
                material.color = color;
            }
        }
    }

    private Color GetRandomColor()
    {
        Color color;

        color = colors[Random.Range(0, colors.Length)];

        return color;
    }

    private void RotateCar()
    {
        isRotating = false;
        var rotX = Input.GetAxis("Mouse X") *  rotSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.up, -rotX);
    }

    private void ContinousRotation()
    {
        if (!isRotating)
            return;
        transform.Rotate(Vector3.up, -rotSpeed * Mathf.Deg2Rad);
    }
}
