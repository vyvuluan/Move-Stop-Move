using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private FloatingJoystick joystick;

    [SerializeField] private Transform parentIndicator;
    [SerializeField] private Transform parentEnemy;
    [SerializeField] private Transform parentNameInfo;

    private Player player;
    private void Awake()
    {
        SpawnPlayer();
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * 20;
            randomPosition.y = 0f;
            SpawnEnemy(randomPosition);
        }
    }
    public void SpawnEnemy(Vector3 positionRandom)
    {
        GameObject enemyGO = SimplePool.Spawn(enemyPrefab, positionRandom, enemyPrefab.transform.rotation);
        Indicator indicator = SimplePool.Spawn(indicatorPrefab, indicatorPrefab.transform.position, indicatorPrefab.transform.rotation).GetComponent<Indicator>();
        enemyGO.GetComponent<IndicatorController>().OnInit(player.transform, indicator);
        enemyGO.GetComponent<Character>().SetParentNameInfo(parentNameInfo);
        //Set parent
        indicator.transform.SetParent(parentIndicator);
        enemyGO.transform.SetParent(parentEnemy);
    }
    public void SpawnPlayer()
    {
        GameObject playerGo = SimplePool.Spawn(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
        player = playerGo.GetComponentInChildren<Player>();
        player.SetParentNameInfo(parentNameInfo);
        player.SetJoystick(joystick);
        cameraFollow.SetPlayer(playerGo.transform);
    }
}
