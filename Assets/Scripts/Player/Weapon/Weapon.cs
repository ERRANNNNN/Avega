using UnityEngine;
using System.Collections;
using TMPro;

public class Weapon : MonoBehaviour 
{
    [SerializeField] private FP_Input playerInput;
    [SerializeField] private WeaponShooter weaponShooter;
    
    [SerializeField] private float shootRate = 0.15F;
    [SerializeField] private float reloadTime = 1.0F;
    [SerializeField] private int ammoCount = 15;

    [SerializeField] private AudioClip reloadAudioClip;
    [SerializeField] private AudioClip shootAudioClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI ammoCountText;

    private int ammo;
    private float delay;
    private bool reloading;

	void Start () 
    {
        ammo = ammoCount;
        ammoCountText.text = ammo.ToString();
	}
	
	void Update () 
    {
        if(playerInput.Shoot())
            if(Time.time > delay)
                Shoot();

        if (playerInput.Reload())
            if (!reloading && ammoCount < ammo)
                StartCoroutine("Reload");
	}

    void Shoot()
    {
        if (ammoCount > 0)
        {
            audioSource.clip = shootAudioClip;
            audioSource.Play();
            weaponShooter.ShootAmmo();
            ammoCount--;
            ammoCountText.text = ammoCount.ToString();
        }

        delay = Time.time + shootRate;
    }

    IEnumerator Reload()
    {
        audioSource.clip = reloadAudioClip;
        audioSource.Play();
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammoCount = ammo;
        ammoCountText.text = ammoCount.ToString();
        reloading = false;
    }
}
