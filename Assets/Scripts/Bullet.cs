using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 70f;
    [SerializeField] private int damage = 50;
    [SerializeField] private float explosionRadius = 0f;
    [SerializeField] private GameObject impactEffect;

    private Transform target;
    
    public void Seek(Transform _target) => target = _target;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }
    
    private void HitTarget()
    {
        Debug.Log("Hit");
        GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);

        if (explosionRadius > 0f)
            Explode();
        else
            Damage(target);
    }
    
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
                Damage(collider.transform);
        }
    }
    
    private void Damage(Transform enemy)
    {
        Destroy(gameObject);
        
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null) 
            e.TakeDamage(damage);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}