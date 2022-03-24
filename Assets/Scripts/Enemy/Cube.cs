using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cube : MonoBehaviour, IPickupable
{
    public List<Material> materials;
    private Material currentMaterial;
    private bool isChangingColor;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickUpClip;

    private void Start()
    {
        audioSource.clip = pickUpClip;
        currentMaterial = materials[Random.Range(0, materials.Count)];
        gameObject.GetComponent<Renderer>().material = currentMaterial;
    }

    public void PickUp(GameObject captor)
    {
        if (!isChangingColor)
        {
            audioSource.Play();
            isChangingColor = true;
            Player player = captor.GetComponent<Player>();
            player.weaponShooter.SetAmmoMaterial(currentMaterial);
            player.colorBlocksPanel.colorBlocks.Where(x => x.color == currentMaterial.color).ToList()[0].changeCount();
            Destroy(gameObject);
        }   
    }
}
