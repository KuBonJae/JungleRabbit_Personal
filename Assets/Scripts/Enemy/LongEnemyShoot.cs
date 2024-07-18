using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongEnemyShoot : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    Transform enemyBulletSpawnPointTransform;
    private float fireRate = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EnemyShoot", 2.0f, fireRate); // 2초 후 부터 fireRate 간격으로 탄막 생성
    }

    // 임의 EnemyShoot 클래스 생성
    void EnemyShoot()
    {
        enemyBulletSpawnPointTransform = gameObject.transform;
        // i used this code but it failed. maybe transform issue? still i dont't know. instead gameobject transform.
        //enemyBulletSpawnPointTransform = gameObject.transform.Find("SpawnEnemyBullet") ;
        Instantiate(enemyBulletPrefab, enemyBulletSpawnPointTransform.position, enemyBulletSpawnPointTransform.rotation); // 적 탄막 스폰 위치에서 적 탄막 생성
    }
}
