using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]                             
    public class Wave
    {
        public EnemyAI[] enemies;                
        public float timeBetweenSpawns = 3f;      
        public float timeBetweenWaves = 10f;     
        public int enemiesCount;                   
    }

    [Header("Wave Settings")]
    [SerializeField] private float countDown;         
    public Wave[] waves;                                 
    public Transform[] spawnPoint;                      
    public int currentWave = 0;                        
    private bool countDownBegin;                     
    [SerializeField] private TextMeshProUGUI waveCountDownText;

    private void Start()
    {
        countDownBegin = true;                        
        for (int i = 0; i < waves.Length; i++)             
        {
            waves[i].enemiesCount = waves[i].enemies.Length;     
        }
       // waveCountDownText.text = "wave: " + currentWave.ToString();       
    }

    private void Update()
    {
        //waveCountDownText.text = "wave: " + currentWave.ToString();       
        if (currentWave >= waves.Length)                                  
        {
            Debug.Log("All Waves Completed!");          
            return;
        }
        if (countDownBegin == true)                      
        {
            countDown -= Time.deltaTime;                  
        }

        if (countDown <= 0f)                            
        {
            countDownBegin = false;                      
            countDown = waves[currentWave].timeBetweenWaves;   
            StartCoroutine(SpawnWave());
        }

        if (waves[currentWave].enemiesCount == 0)                
        {
            countDownBegin = true;                      
            currentWave++;                            
        }
    }

    IEnumerator SpawnWave()
    {
        if (currentWave < waves.Length)      
        {
            for (int i = 0; i < waves[currentWave].enemies.Length; i++)    
            {
                Transform spawnPoints = spawnPoint[Random.Range(0, spawnPoint.Length)]; 
                EnemyAI enemy = Instantiate(waves[currentWave].enemies[i], spawnPoints.position, Quaternion.identity); 
                enemy.transform.SetParent(spawnPoints);                                                         
                yield return new WaitForSeconds(waves[currentWave].timeBetweenSpawns);                 
            }
            Debug.Log("Wave Spawned");                   
        }
    }
}
