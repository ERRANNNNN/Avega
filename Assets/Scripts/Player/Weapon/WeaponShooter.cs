using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    [SerializeField] private Transform ammoTF;
    [SerializeField] private float shootForce;
    [SerializeField] private Material currentAmmoMaterial;
    private Camera mainCamera;
    private Vector3 ammoOffset = new Vector3(0, 0, 0);

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void ShootAmmo()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        } else
        {
            targetPoint = ray.GetPoint(75);
        }
        Debug.DrawRay(ray.origin, ray.direction);

        Vector3 heading = targetPoint - transform.position;
        float distance = heading.magnitude;
        Vector3 shootDirection = heading / distance;

        //Vector3 shootDirection = (targetPoint - transform.position).normalized;

            
        Transform currentAmmoTF = Instantiate(ammoTF, transform.position + ammoOffset, Quaternion.identity);
        Ammo currentAmmo = currentAmmoTF.GetComponent<Ammo>();
        Rigidbody currentAmmoRigidbody = currentAmmoTF.GetComponent<Rigidbody>();

        currentAmmoTF.forward = shootDirection;
        currentAmmo.ChangeMaterial(currentAmmoMaterial);
        currentAmmoRigidbody.AddForce(shootDirection * shootForce, ForceMode.Impulse);
    }

    public void SetAmmoMaterial(Material material)
    {
        currentAmmoMaterial = material;
    }
}
