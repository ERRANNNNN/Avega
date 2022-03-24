using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform PlayerTF;
    [SerializeField] private Player player;
    [SerializeField] private Transform EnemyTF;
    private List<Enemy> enemies = new List<Enemy>();
    private NavMeshTriangulation triangulation;
    // Start is called before the first frame update
    void Start()
    {
        triangulation = NavMesh.CalculateTriangulation();
        StartCoroutine(SpawnEnemies());
        Enemy.onEnemyDeath += DeleteEnemy;
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (enemies.Count != 10)
            {
                bool isCorrectPoint = false;
                while (!isCorrectPoint)
                {
                    int vertexIndex = Random.Range(0, triangulation.vertices.Length);
                    NavMeshHit navMeshHit;
                    if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out navMeshHit, 1f, -1))
                    {
                        
                        Vector3 position = navMeshHit.position;
                        position += new Vector3(0, 0.7f, 0);
                        Vector2 viewPortPosition = Camera.main.WorldToViewportPoint(position);
                        float distanceToPlayer = Vector3.Distance(player.transform.position, position);
                        if ((viewPortPosition.x > 0 && viewPortPosition.x < 1) && (viewPortPosition.y > 0 && viewPortPosition.x < 1))
                        {
                            continue;
                        } else if (distanceToPlayer < 10)
                        {
                            continue;
                        }
                        Enemy enemy = Instantiate(EnemyTF, position, Quaternion.identity).GetComponent<Enemy>();
                        enemies.Add(enemy);
                        enemy.Player = player;
                        enemy.playerTF = PlayerTF;
                    }
                    isCorrectPoint = true;
                }
            }
            yield return new WaitForSeconds(3);
        }
    }

    private void DeleteEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    private void OnDestroy()
    {
        Enemy.onEnemyDeath -= DeleteEnemy;
    }
}
