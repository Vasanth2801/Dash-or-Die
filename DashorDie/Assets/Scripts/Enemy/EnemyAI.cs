using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] Transform target;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private HealthBar health;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spawner = FindObjectOfType<EnemySpawner>();
        health = FindObjectOfType<HealthBar>();
    }

    void Update()
    {
        if(target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime,Space.World);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            health.TakeDamage(20);
            gameObject.SetActive(false);
           spawner.waves[spawner.currentWave].enemiesCount--;
        }
    }
}