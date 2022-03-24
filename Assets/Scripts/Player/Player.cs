using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private TextMeshProUGUI healthText;
    public ColorBlocksPanel colorBlocksPanel;
    public WeaponShooter weaponShooter;

    public void Damage(int damage)
    {
        health -= damage;
        healthText.text = health.ToString();
        if (health == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        SceneManager.LoadScene("DemoScene");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IPickupable pickupable = hit.gameObject.GetComponent<IPickupable>();
        if (pickupable != null)
        {
            pickupable.PickUp(gameObject);
        }
    }
}
