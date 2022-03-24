using System.Collections;
using UnityEngine;
public class Ammo : MonoBehaviour
{
    [SerializeField] private Renderer ammoRenderer;

    void Start()
    {
        StartCoroutine(DelayedDeath());
    }

    IEnumerator DelayedDeath()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    public void ChangeMaterial(Material material)
    {
        ammoRenderer.material = material;
    }

    public void Death()
    {
        StopCoroutine(DelayedDeath());
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Damage();
            Death();
        }
    }
}
