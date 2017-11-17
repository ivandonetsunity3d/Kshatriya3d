using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour {

    public Transform LeftMostPoint;
    public Transform RightMostPoint;
    private GameObject _Player;


    public GameObject TrollPrefab;
    public GameObject BossPrefab;
    public GameObject SkeletonKnightPrefab;
    public GameObject MummyPrefab;
    public GameObject GhoulPrefab;
    public GameObject UndeadPrefab;


    public int TotalTrolls;
    public int TotalBosses;
    public int TotalSkeletonKnights;
    public int TotalMummies;
    public int TotalGhouls;
    public int TotalUndeads;



    void GenerateEnemies(int TotalEnemies, GameObject EnemyPrefab) 
    {
        for (int i = 0; i < TotalEnemies; i++)
        {

            Vector3 EnemyLocation = LeftMostPoint.position;

            //EnemyLocation.x = 16f;
            //EnemyLocation.y = 0f;
            //EnemyLocation.z = -55f;

            Quaternion Rotatio = Quaternion.identity;




            int RndNumber = Random.Range(Mathf.RoundToInt(RightMostPoint.position.z), Mathf.RoundToInt(LeftMostPoint.position.z));

            // Debug.Log(RndNumber);


            EnemyLocation.z = RndNumber;// EnemyLocation.z - RndNumber;



            GameObject NewEnemy = Instantiate(EnemyPrefab, EnemyLocation, Rotatio) as GameObject;

            NewEnemy.transform.name = "Enemy";

            Enemy EnemyScript = NewEnemy.GetComponent<Enemy>();

            EnemyScript.LeftMost = LeftMostPoint;
            EnemyScript.RightMost = RightMostPoint;

            EnemyScript._Player = _Player;

        }
    }

    // Use this for initialization
    void Start () {
        _Player = GameObject.Find("Player");

        GenerateEnemies(TotalTrolls, TrollPrefab);
        GenerateEnemies(TotalBosses, BossPrefab);

        GenerateEnemies(TotalSkeletonKnights, SkeletonKnightPrefab);
        GenerateEnemies(TotalMummies, MummyPrefab);
        GenerateEnemies(TotalGhouls, GhoulPrefab);
        GenerateEnemies(TotalUndeads, UndeadPrefab);



    }

    // Update is called once per frame
    void Update () {
		
	}



}
