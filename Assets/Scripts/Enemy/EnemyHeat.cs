using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHeat : MonoBehaviour
{
    private float enemyHP;

    public ParticleSystem deathEffectPrefab;


    // Start is called before the first frame update
    void Start()
    {
        enemyHP = 5.0f * DataManager.Instance.StageLevel;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 충돌 시 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        /* for special weapon
                switch (DataManager.Instance.Weapon)
        {
            case nameof(WeaponType.Sword):
                Destroy(collision.gameObject);
                enemyHP -= 1;
                break;

            case nameof(WeaponType.Axe):
                Destroy(collision.gameObject);
                enemyHP -= 2;
                break;

            case nameof(WeaponType.Bow):
                Destroy(collision.gameObject);
                enemyHP -= 1;
                break;

            // 다른 무기 유형들...

            default:
                // for no weapon
                Debug.LogWarning("알 수 없는 무기 유형입니다.");
                break;
        }
          */



        // different damage according to weapon type ShortSword, LongSword, Axe, ShotGun, Rifle, Sniper
        if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Skill"))
        {
            if (collision.gameObject.CompareTag("Weapon"))
            {
                if (DataManager.Instance.Weapon == WeaponType.Gun.ToString())
                {
                    Debug.Log("총 맞음!");
                    enemyHP -= DataManager.Instance.Damage;
                }
                else if (DataManager.Instance.Weapon == WeaponType.Sword.ToString())
                {
                    Debug.Log("칼 맞음!");
                    enemyHP -= DataManager.Instance.Damage;
                }
                else
                {
                    Debug.Log("총기 타입 없음!");
                    enemyHP -= DataManager.Instance.Damage;
                }
            }
            else if (collision.gameObject.CompareTag("Skill"))
            {
                Debug.Log("스킬 맞음!");
                if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Axe.ToString())
                {
                    Debug.Log("도끼 스킬 맞음!");
                    enemyHP -= DataManager.Instance.AxeDamage;
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.LongSword.ToString())
                {
                    Debug.Log("대검 스킬 맞음!");
                    enemyHP -= (DataManager.Instance.Damage * 5f);
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.ShortSword.ToString())
                {
                    Debug.Log("단검 스킬 맞음!");
                    enemyHP -= DataManager.Instance.ShurikenDamage;
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Sniper.ToString())
                {
                    Debug.Log("저격 스킬 맞음!");
                    enemyHP -= DataManager.Instance.SkillDamage;
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.ShotGun.ToString())
                {
                    Debug.Log("샷건 스킬 맞음!");
                    enemyHP -= DataManager.Instance.SkillDamage;
                }
                else if (DataManager.Instance.SpecialWeapon == SpecialWeaponType.Rifle.ToString())
                {
                    Debug.Log("라이플 스킬 맞음!");
                    enemyHP -= DataManager.Instance.SkillDamage;
                }
                else
                {
                    Debug.Log("칼 기본 스킬 맞음!");
                    enemyHP -= (DataManager.Instance.Damage * 2.5f);
                }
            }
            else
            {
                Debug.Log("넌 왜 닳는거..?");
                enemyHP = DataManager.Instance.Damage;

            }
        }
        // when enemy heated, compare enemyHP
        //GameObject hp1 = transform.Find("HP_1").gameObject;



        Transform enemyHPTransform = gameObject.transform.Find("EnemyHP");
        Transform hp2Transform = enemyHPTransform.Find("HP_2");
        Transform hp3Transform = enemyHPTransform.Find("HP_3");
        Transform hp4Transform = enemyHPTransform.Find("HP_4");
        Transform hp5Transform = enemyHPTransform.Find("HP_5");
        switch (enemyHP)
        {
            case <= 0:
                Debug.Log("Die!");
                Destroy(gameObject);
                //Instantiate(deathEffectPrefab, transform.position, quaternion.identity); 사망 파티클

                break;

            case <= 1:
                Debug.Log("적피 1");
                if (hp2Transform != null)
                {
                    Destroy(hp2Transform.gameObject);
                }
                if (hp3Transform != null)
                {
                    Destroy(hp3Transform.gameObject);
                }
                if (hp4Transform != null)
                {
                    Destroy(hp4Transform.gameObject);
                }
                if (hp5Transform != null)
                {
                    Destroy(hp5Transform.gameObject);
                }
                //Destroy(enemyHPTransform.gameObject.transform.Find("HP_2"));
                break;

            case <= 2:
                Debug.Log("적피 2");
                if (hp3Transform != null)
                {
                    Destroy(hp3Transform.gameObject);
                }
                if (hp4Transform != null)
                {
                    Destroy(hp4Transform.gameObject);
                }
                if (hp5Transform != null)
                {
                    Destroy(hp5Transform.gameObject);
                }
                break;

            case <= 3:
                Debug.Log("적피 3");
                if (hp4Transform != null)
                {
                    Destroy(hp4Transform.gameObject);
                }
                if (hp5Transform != null)
                {
                    Destroy(hp5Transform.gameObject);
                }
                break;

            case <= 4:
                Debug.Log("적피 4");
                if (hp5Transform != null)
                {
                    Destroy(hp5Transform.gameObject);
                }
                break;

            default:
                break;
        }
    }
}

