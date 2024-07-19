using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShieldCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("카운터!");
            collision.transform.GetComponent<EnemyHeat>().enemyHP -= DataManager.Instance.Damage;
            collision.transform.GetComponent<EnemyHeat>().hpText.GetComponent<TextMeshProUGUI>().text = DataManager.Instance.Damage.ToString();
            collision.transform.GetComponent<EnemyHeat>().beDamaged = true;
            Vector3 revDir = collision.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
            collision.transform.GetComponent<Rigidbody2D>().AddForce(revDir.normalized * 1000f);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control_Sword>().shield.GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine("SlowMotionInCounter");
        }
        else if (collision.gameObject.CompareTag("BOSS"))
        {
            Debug.Log("카운터!");
            collision.transform.GetComponent<BossHeat>().bossHP -= DataManager.Instance.Damage;
            collision.transform.GetComponent<BossHeat>().hpText.GetComponent<TextMeshProUGUI>().text = DataManager.Instance.Damage.ToString();
            collision.transform.GetComponent<BossHeat>().beDamaged = true;
            Vector3 revDir = collision.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
            collision.transform.GetComponent<Rigidbody2D>().AddForce(revDir.normalized * 500f);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control_Sword>().shield.GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine("SlowMotionInCounter");
        }
        else if (collision.gameObject.CompareTag("EnemyWeapon"))
        {
            if(collision.transform.GetComponent<BossShootBullet>() != null)
                collision.transform.GetComponent<BossShootBullet>().bulletDirection *= -1;
            else
                collision.transform.GetComponent<EnemyBullet>().bulletDirection *= -1;
            collision.gameObject.tag = "Weapon";
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control_Sword>().shield.GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine("SlowMotionInCounter");
        }
    }

    IEnumerator SlowMotionInCounter()
    {
        Time.timeScale = 0.1f;
        float slowTime = 0f;
        while (true)
        {
            yield return null;

            slowTime += Time.unscaledDeltaTime;
            if (slowTime > 0.5f)
            {
                Time.timeScale = 1f;
                break;
            }
        }
    }
}
