using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float changeDistance;
    private Transform _target;
    private int _wavepointIndex;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        _target = Waypoints.waypoints[0];
    }

    private void Update()
    {
        Vector3 dir = _target.position - transform.position;
        
        transform.Translate(dir.normalized * enemy.Speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= changeDistance)
            GetNextWaypoint();

        enemy.Speed = enemy.startSpeed;
    }
    
    
    private void GetNextWaypoint()
    {
        if (_wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            PathEnded();
            return;
        }
        
        _wavepointIndex++;
        _target = Waypoints.waypoints[_wavepointIndex];
    }
    
    void PathEnded()
    {
        PlayerStats.Lives--;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}