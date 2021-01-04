using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private enum BossStage
    {
        Idle,
        Stage_1,
        Stage_2,
        Stage_3,
        Stage_4
    }

    private BossStage bossStage = BossStage.Idle;
    private BossStage oldBossStage = BossStage.Idle;

    private bool isBattleStarted = false;
    private bool isFirstTimeInState = false;

    [SerializeField] private EnemiesSpawner enemiesSpawner = null;

    [Space]
    [SerializeField] private List<Transform> runToPointList = new List<Transform>();
    [SerializeField] private float startBattleRaduis = 50f;
    [SerializeField] private Color startBattleColor = Color.red;

    [Space]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject healthBarDisplay;
    private HealthSystem healthSystem = new HealthSystem(400);

    private void Start ()
    {
        healthBar.Setup(healthSystem);
    }

    private void OnDrawGizmos ()
    {
        Gizmos.color = startBattleColor;
        Gizmos.DrawWireSphere(this.transform.position, startBattleRaduis);
    }

    private void Update ()
    {
        if (isBattleStarted == false)
        {
            if (OverLap(startBattleRaduis))
            {
                isBattleStarted = true;
                healthBarDisplay.SetActive(true);
                StartBattle();
                CheckState();
            }
        }
    }

    private bool OverLap (float radius, string tagtoCheck = "Player")
    {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, radius);

        if (hits != null)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].gameObject.tag == tagtoCheck)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            healthSystem.Damage(other.GetComponent<Bullet>().bulletDamage, 2);

            ChangeState();
            CheckState();

            if (healthSystem.Health <= 0)
            {
                gameObject.SetActive(false);
                healthBarDisplay.SetActive(false);
            }
        }
    }

    private void ChangeState ()
    {
        var currentHealth = healthSystem.Health;

        if (currentHealth < 70)
        {
            bossStage = BossStage.Stage_4;
        } else if (currentHealth < 130)
        {
            bossStage = BossStage.Stage_3;
        } else if (currentHealth < 200)
        {
            bossStage = BossStage.Stage_2;
        }
    }

    private void CheckState ()
    {
        if (bossStage != oldBossStage)
        {
            oldBossStage = bossStage;
        } else
        {
            return;
        }

        var enemiesTransforms = enemiesSpawner.SpawnEnemy(3);

        List<Enemy> enemies = new List<Enemy>();
        foreach (Transform enemyTransform in enemiesTransforms)
        {
            enemies.Add(enemyTransform.GetComponent<Enemy>());
        }

        foreach (Enemy enemy in enemies)
        {
            var goToPoint = runToPointList[UnityEngine.Random.Range(0, runToPointList.Count)].position;
            enemy.Goto(goToPoint);
        }
    }

    private void StartBattle ()
    {
        bossStage = BossStage.Stage_1;
    }
}
