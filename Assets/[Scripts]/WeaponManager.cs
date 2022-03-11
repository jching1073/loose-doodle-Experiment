// WeaponManager.cs
// Owned by Garabatos Inc.
// Created by: Jerome Ching (300817930)

using UnityEngine;
using System.Collections;
using System.IO;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class WeaponManager : MonoBehaviour, ISaveLoad
{
    public GameObject playerCam;
    public GameObject shootingRayCast;
    public float weaponRange = 100;
    public float damageEnemy = 1;
    public Animator playerAnimator;
    public bool isFlashLight = false;
    public bool isItOn = false;
    public LayerMask flashLightOff;
    public LayerMask flashLightOn;
    public GameObject lightSource;

    public ParticleSystem waterSplash;

    public GameObject hitParticle;

    [Header("Sound Effects")]
    public SoundFile shootSound;
    public SoundFile flashLightSFX;
    public SoundFile hitSFX;

    private AudioSource soundPlayer;

    void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;

        //turn off shooting animation
        if (playerAnimator.GetBool("IsShooting"))
        {
            playerAnimator.SetBool("IsShooting", false);
        }

        if (Input.GetKeyDown(InputManager.Instance.shoot))
        {
            Debug.Log("Shooooot");
            Shoot();
        }

        if (Input.GetKeyDown(InputManager.Instance.power2))
        {
            //FlashLightToggle();
            if (isFlashLight == true)
            {
                FlashLightToggle();
            }
        }
    }

    public void PickUpFlashLight()
    {
        isFlashLight = true;
        UIManager.Instance.SetFlashlight(true);
    }

    void Shoot()
    {
        waterSplash.Play();
        shootSound.PlayWithSource(soundPlayer);
        //trigger animation
        playerAnimator.SetBool("IsShooting", true);
        RaycastHit hit;
        if (Physics.Raycast(shootingRayCast.transform.position, -transform.forward, out hit, weaponRange))
        {
            Debug.Log("Outside");
            EnemyManagerSimple enemyManager = hit.transform.GetComponent<EnemyManagerSimple>();
            EnemyAI enemyAI = hit.transform.GetComponent<EnemyAI>();
            //enemyManager.HitEnemy(damageEnemy);
            if (enemyManager != null)
            {
                Debug.Log("Hitting Enemy");
                enemyManager.HitEnemy(damageEnemy);
                GameObject intantiatParticle = Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
                intantiatParticle.transform.parent = hit.transform;

                Destroy(intantiatParticle, 2f);
                hitSFX.PlayWithSource(soundPlayer);

            }

            if (enemyAI != null)
            {
                Debug.Log("Hitting Enemy");
                enemyAI.TakeDamage(damageEnemy);
                GameObject intantiatParticle = Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
                intantiatParticle.transform.parent = hit.transform;

                Destroy(intantiatParticle, 2f);
                hitSFX.PlayWithSource(soundPlayer);
            }
        }
    }

    //Toggle flashlight on and off
    public void FlashLightToggle()
    {
        //Turns flash light On
        if (isFlashLight)
        {
            if (isItOn == false)
            {
                TurnOnFlashLight();
            }
            //Turns flash light off
            else if (isItOn == true)
            {
                TurnOffFlashLight();
            }
        }
    }

    private IEnumerator FlashLightTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(10.0f);
        yield return wait;
        if(isItOn == true)
        {
            TurnOffFlashLight();
        }

    }
    public void TurnOnFlashLight()
    {
        playerCam.GetComponent<Camera>().cullingMask = flashLightOn;
        isItOn = true;
        UIManager.Instance.flashlightActive(true);
        lightSource.SetActive(true);
        flashLightSFX.PlayWithSource(soundPlayer);
        Debug.Log("Flash Light is On");
        StartCoroutine(FlashLightTimer());
    }
    public void TurnOffFlashLight()
    {
        playerCam.GetComponent<Camera>().cullingMask = flashLightOff;
        //playerCam.GetComponent<Camera>().cullingMask = 0 << 50;
        isItOn = false;
        UIManager.Instance.flashlightActive(false);
        lightSource.SetActive(false);
        flashLightSFX.PlayWithSource(soundPlayer);
        Debug.Log("Flash Light is Off");
    }

    public void Save(TextWriter writer)
    {
        writer.WriteLine(isFlashLight ? "Weapon flashlight" : "Weapon not flashlight");
    }

    public void Load(TextReader reader)
    {
        string line = reader.ReadLine();
        if (line == "Weapon flashlight")
        {
            PickUpFlashLight();
        }
    }
}
