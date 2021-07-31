using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float bulletFloatOffset;

    private PlayerController playerController;
    private MapManager mapManager;
    AudioSource shoot;

    [SerializeField]
    private GameObject playerSprite;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        mapManager = GameObject.FindWithTag("MapManager").GetComponent<MapManager>();
        playerAnimator = playerSprite.GetComponent<Animator>();
        shoot = GetComponent<AudioSource>();
    }
     
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && LevelManager.Instance.canPlayerMove)
        {
            playerAnimator.SetTrigger("Shoot");
            ShootBullet();
        }
    }

    public void ShootBullet()
    {
        Transform planeLocation = this.gameObject.transform;
        mapManager.GetPlaneTransform(playerController.objectLocation).ApplyTo(planeLocation);
        EffectsManager.instance.playShoot();
        Instantiate(bullet, planeLocation.position + bulletFloatOffset * transform.up, planeLocation.rotation);
    }
}
