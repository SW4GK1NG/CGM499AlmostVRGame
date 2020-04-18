using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public float spawnInterval;
    public float startDelay;
    public float enemySpeed;
    public Transform[] spawnPoints;
    public GameObject[] Enemy;
    public Transform goal;
    public Text UIText;
    public Text KillUI;
    public Text HealthUI;
    
    GameObject[] enemylist;
    string[] tagToDisable = {
        "ELeaf", "EWater", "EFire"
    };
    bool Killed10;
    bool Killed20;
    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

        KillUI.text = "Kills: " + MasterControl.Instance.Kills + "/30";
        HealthUI.text = "Healths: " + MasterControl.Instance.Health;

        if (MasterControl.Instance.Health == 0) {
            CancelInvoke("Spawn");
            foreach (string tag in tagToDisable) {
                enemylist = GameObject.FindGameObjectsWithTag(tag);
                for(var i = 0; i < enemylist.Length; i++) {
                    Destroy(enemylist[i]);
                }
            }
            UIText.text = "YOU LOSE LUL";
        }

        if (MasterControl.Instance.Kills == 10 && !Killed10) {
            Killed10 = true;
            CancelInvoke("Spawn");
            enemySpeed = enemySpeed * 1.5f;
            InvokeRepeating("Spawn", 0, spawnInterval - 2);
        }

        if (MasterControl.Instance.Kills == 20 && !Killed20) {
            Killed20 = true;
            CancelInvoke("Spawn");
            enemySpeed = enemySpeed * 1.3f;
            InvokeRepeating("Spawn", 0, spawnInterval - 3.5f);
        }

        if (MasterControl.Instance.Kills >= 30) {
            CancelInvoke("Spawn");
            foreach (string tag in tagToDisable) {
                enemylist = GameObject.FindGameObjectsWithTag(tag);
                for(var i = 0; i < enemylist.Length; i++) {
                    Destroy(enemylist[i]);
                }
            }
            UIText.text = "YOU WON POGU";
        }
    }

    void Spawn() {
        int spawnNum = Random.Range(0, spawnPoints.Length);
        int enemyNum = Random.Range(0, Enemy.Length);
        GameObject enemy = Instantiate(Enemy[enemyNum]);
        EnemyPatrol enemyScript = enemy.GetComponent<EnemyPatrol>();
        enemy.transform.position = spawnPoints[spawnNum].position;
        enemyScript.speed = enemySpeed;
        enemyScript.moveSpots = goal;
    }
}
