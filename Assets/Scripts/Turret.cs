using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] private Transform PartToRotate;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float Range;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    
    [Header("Laser Atributes / Opcional")]
    [SerializeField] private bool useLaser = false;
    [SerializeField] private int damageOverTime = 30;
    [Range(0,1)]public float slowAmount = 0.5f;
    
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem laserEffect;
    [SerializeField] private Light laserLight;

    private float fireCountdown = 0f;
    private Transform _target;
    private string _enemyTag = "Enemy";
    private Enemy targetEnemy;

    private void Start() => InvokeRepeating("UpdateTarget", 0f, 0.5f);
    
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(_enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= Range)
        {
            _target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            _target = null;
        }
    }

    private void Update()
    {
        if (_target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserEffect.Stop();
                    laserLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
            return;
        }
        
        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    
    private void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserEffect.Play();
            laserLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, _target.position);
        Vector3 dir = firePoint.position - _target.position;

        laserEffect.transform.position = _target.position + dir.normalized * _target.transform.localScale.x / 2;
        laserEffect.transform.rotation = Quaternion.LookRotation(dir);
    }
    
    private void LockOnTarget()
    {
        Vector3 dir = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(90f, rotation.y, 0f);
    }
    
    private void Shoot()
    {
        Debug.Log("Shooting - " + name);
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(_target);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0.4f, 1, 1);
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}