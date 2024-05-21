using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    #region Public Variables
    public enum WeaponType { 
        fist,
        gun,
        machinegun
    }
    public WeaponType activeWeapon = WeaponType.fist;
    public Transform attackPoint;
    public GameObject gunProjectile;
    public GameObject machineGunProjectile;
    public GameObject Arm;
    public GameObject gunVFX;
    public GameObject machineGunVFX;
    public LayerMask enemyLayers;
    public Animator animator;
    public Image fistImg;
    public Image gunImg;
    public Image machineGunImg;
    public Gun gun;
    public float attackRange = 0.2f;
    public float startTimeBtwShots;
    public float attackCooldown;
    public float hasGun = 0.0f;
    public float hasMachineGun = 0.0f;
    public int regenerationHealth = 1;
    public int maxHealth = 500;
    public int attackDamage = 40;
    #endregion

    #region Private Variables
    private CharacterController characterController;
    private AudioController audioController;
    private float lastAttack = 0;
    private float fistCooldown = 0.3f;
    private float gunCooldown = 0.3f;
    private float machineGunCooldown = 0.1f;
    private float timeBtwShots;
    private int currentHealth;
    private bool attackMode;
    #endregion

    public HealthBar healthBar;
    private void Start()
    {
        setMaxHealth();
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        audioController = GetComponent<AudioController>();
    }

    public void setMaxHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void SetWeaponType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeWeapon = WeaponType.fist;
            attackCooldown = fistCooldown;
            if (hasGun > 0.0f) gunImg.color = Color.grey;
            fistImg.color = Color.white;
            if(hasMachineGun > 0.0f) machineGunImg.color = Color.gray;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && hasGun > 0.0f)
        {
            activeWeapon = WeaponType.gun;
            attackCooldown = gunCooldown;
            gunVFX.SetActive(true);
            machineGunVFX.SetActive(false);
            if (hasGun > 0.0f)  gunImg.color = Color.white;
            fistImg.color = Color.grey;
            if (hasMachineGun > 0.0f)  machineGunImg.color = Color.gray;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && hasMachineGun > 0.0f)
        {
            activeWeapon = WeaponType.machinegun;
            attackCooldown = machineGunCooldown;
            gunVFX.SetActive(false);
            machineGunVFX.SetActive(true);
            if (hasGun > 0.0f)  gunImg.color = Color.grey;
            fistImg.color = Color.grey;
            if (hasMachineGun > 0.0f)  machineGunImg.color = Color.white;
        }
    }



    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentHealth);
        SetWeaponType();
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time - lastAttack > attackCooldown && characterController.canAttack())
        {
            Attack();
        }
        if (activeWeapon == WeaponType.gun || activeWeapon == WeaponType.machinegun)
        {
            animator.SetFloat("HasGun", 1.0f);
        }
        else
        {
            animator.SetFloat("HasGun", 0.0f);
        }
        if (activeWeapon == WeaponType.gun || activeWeapon == WeaponType.machinegun)
        {
            Arm.SetActive(true);
        }
        if (activeWeapon == WeaponType.fist || !characterController.canAttack())
        {
            Arm.SetActive(false);
        }
    }

    void Attack() 
    {
        attackMode = true;
        lastAttack = Time.time;
        if (activeWeapon == WeaponType.gun || activeWeapon == WeaponType.machinegun)
        {
            Vector3 projectilePosition = new Vector3(gun.gameObject.transform.position.x, gun.gameObject.transform.position.y, gun.gameObject.transform.position.z);
            if (activeWeapon == WeaponType.gun)
            {
                Instantiate(gunProjectile, projectilePosition, Quaternion.identity);
                audioController.playGunSound();
            }
            else if (activeWeapon == WeaponType.machinegun) 
            {
                Instantiate(machineGunProjectile, projectilePosition, Quaternion.identity);
                audioController.playMachineGunSound();
            }
            
        }
        else if(activeWeapon == WeaponType.fist && animator.GetFloat("Speed") < 0.01f)
        {
            animator.SetTrigger("Attack");
            audioController.playPunchSound();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Enemies"));

            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                ShootingEnemy shootingEnemy = enemy.GetComponent<ShootingEnemy>();
                if (enemyController != null)
                {
                    enemyController.TakeDamage(attackDamage);
                }
                if (shootingEnemy != null)
                {
                    shootingEnemy.TakeDamage(attackDamage);
                }
            }
        }
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        audioController.playHurtSound();
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            characterController.Death();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
