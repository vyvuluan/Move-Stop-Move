using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform parentIndicator;
    [SerializeField] private Transform parentEnemy;

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * 20;
            randomPosition.y = 0;
            SpawnEnemy(randomPosition);
        }
    }
    public void SpawnEnemy(Vector3 positionRandom)
    {
        GameObject enemyGO = SimplePool.Spawn(enemyPrefab, positionRandom, enemyPrefab.transform.rotation);
        Indicator indicator = SimplePool.Spawn(indicatorPrefab, indicatorPrefab.transform.position, indicatorPrefab.transform.rotation).GetComponent<Indicator>();
        indicator.transform.SetParent(parentIndicator);
        enemyGO.GetComponent<IndicatorController>().OnInit(player, indicator);
        enemyGO.transform.SetParent(parentEnemy);
    }
}
