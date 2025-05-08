using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth = 5f;
    [SerializeField] private float swayMultiplier = 1f;

    private Quaternion initialRotation;
    private Quaternion recoilRotation;
    private Quaternion currentRecoil;

    [Header("Recoil Settings")]
    [SerializeField] private float recoilRecoverySpeed = 10f;

    private void Start()
    {
        initialRotation = transform.localRotation;
        recoilRotation = Quaternion.identity;
        currentRecoil = Quaternion.identity;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        // Apply sway + recoil relative to the initial rotation
        Quaternion targetRotation = initialRotation * rotationX * rotationY * currentRecoil;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);

        // Gradually reduce recoil
        currentRecoil = Quaternion.Slerp(currentRecoil, Quaternion.identity, recoilRecoverySpeed * Time.deltaTime);
    }

    public void AddRecoil(float x, float y, float z)
    {
        Quaternion recoil = Quaternion.Euler(x, y, z);
        currentRecoil *= recoil;
    }
}
