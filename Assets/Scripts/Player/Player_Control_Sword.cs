using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Control_Sword : MonoBehaviour
{

    public float horizontalInput;
    public float verticalInput;
    bool up = false;
    bool down = false;
    bool right = false;
    bool left = false;
    bool upright = false;
    bool upleft = false;
    bool downright = false;
    bool downleft = false;
    Vector2 prevPosition = new Vector2(951201, 980423);

    public float speed;
    int dashCount = 2;
    float dashCoolTime = 0f;

    // UI
    public GameObject map;
    public GameObject keyGuide;
    public HealthUIManager healthUIManager;
    GameObject DashManager;
    //

    // Start is called before the first frame update
    void Start()
    {
        speed = DataManager.Instance.Speed;
        StartCoroutine("Flip");
        StartCoroutine("ChargeDash");
        StartCoroutine("HitDelay");
        dashCount = DataManager.Instance.DashCount;
        healthUIManager.SethealthCount(DataManager.Instance.Health);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = ManagingInput.GetAxis("Horizontal");
        verticalInput = ManagingInput.GetAxis("Vertical");

        if (SceneManager.GetActiveScene().name == "BossScene")
        {
            if (GameObject.Find("Camera").transform.Find("BossCam").gameObject.activeSelf)
            {
                ManagingInput.blockInputs = true;
            }
            else
            {
                ManagingInput.blockInputs = false;
                UpdateData();
            }
        }
        else
        {
            ManagingInput.blockInputs = false;
            UpdateData();
        }

        if(prevPosition.x == 951201 && prevPosition.y == 980423) // init value, first move
        {
            prevPosition.y = transform.position.y;
            prevPosition.x = transform.position.x;
        }
        else
        {
            if(up && right && left && down)
            {
                Vector2 dir = prevPosition - new Vector2(transform.position.x, transform.position.y);
                transform.position += new Vector3(dir.normalized.x, dir.normalized.y, 0);
                up = right = left = down = false;
            }
            prevPosition.y = transform.position.y;
            prevPosition.x = transform.position.x;
        }

        //Block();
        //toggleMap();
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    baseSkill();
        //}
        //else if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    speed = DataManager.Instance.Speed;
        //}
        //
        //transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);
        //transform.Translate(Vector2.up * verticalInput * Time.deltaTime * speed);
        //
        //healthUIManager.SethealthCount(DataManager.Instance.Health);
    }

    private void UpdateData()
    {
        Block();
        toggleMap();
        if (ManagingInput.GetKeyDown(KeyCode.Space))
        {
            baseSkill();
        }
        else if (ManagingInput.GetKeyUp(KeyCode.Space))
        {
            speed = DataManager.Instance.Speed;
        }

        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector2.up * verticalInput * Time.deltaTime * speed);

        healthUIManager.SethealthCount(DataManager.Instance.Health);
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void toggleMap()
    {
        if (ManagingInput.GetKeyDown(KeyCode.Tab))
        {
            map.SetActive(!map.activeSelf);
            keyGuide.SetActive(!keyGuide.activeSelf);
            if (map.activeSelf)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
        }
    }

    public void baseSkill()
    {
        if (dashCount > 0)
        {
            GameObject.Find("Canvas_Dash").transform.GetChild(dashCount).gameObject.SetActive(false);
            dashCount--;
            speed *= 3;
            StartCoroutine("DashCutter");
        }
    }

    IEnumerator DashCutter()
    {
        //dashState = true;
        DataManager.Instance.DashState = true;
        this.gameObject.layer = 10;
        yield return new WaitForSeconds(0.25f);
        //dashState = false;
        DataManager.Instance.DashState = false;
        this.gameObject.layer = 0;
    }

    IEnumerator ChargeDash()
    {
        while (true)
        {
            yield return null;

            if (dashCount < DataManager.Instance.DashCount)
            {
                dashCoolTime += Time.deltaTime;
                if (dashCoolTime >= 3)
                {
                    dashCount++;
                    GameObject.Find("Canvas_Dash").transform.GetChild(dashCount).gameObject.SetActive(true);
                    dashCoolTime = 0;
                }
            }
            else
                continue;
        }

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy")) //|| collision.gameObject.CompareTag("Boss"))
    //    {
    //        if (dashState)
    //        {
    //            //Destroy(collision.gameObject);
    //        }
    //        else
    //        {
    //            Debug.Log("�ƾ�!");
    //        }
    //    }
    //}


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BOSS") || collision.gameObject.CompareTag("EnemyWeapon"))
        {
            if (DataManager.Instance.DashState)
            {
                // attack damage to enemy based on character damage
                // this will be managed by enemymanager
            }
            else
            {
                if (!DataManager.Instance.beHit)
                {
                    DataManager.Instance.beHit = true;
                    Debug.Log("피해입음!");
                    this.gameObject.GetComponent<HitEffect>().TriggerHitEffect();
                    DataManager.Instance.Health = DataManager.Instance.Health - 0.5f;
                    healthUIManager.SethealthCount(DataManager.Instance.Health);
                    if (DataManager.Instance.Health <= 0)
                    {
                        DataManager.Instance.beHit = false;
                        DataManager.Instance.isDead = true;
                    }
                }
            }
        }
    }

    IEnumerator Flip()
    {
        while (true)
        {
            horizontalInput = ManagingInput.GetAxis("Horizontal");

            yield return null;
            if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

        }
    }

    IEnumerator HitDelay()
    {
        float hitDelay = 0f;

        while (true)
        {
            yield return null;

            if (DataManager.Instance.beHit)
            {
                hitDelay += Time.deltaTime;
                if (hitDelay >= 0.5f)
                {
                    hitDelay = 0f;
                    DataManager.Instance.beHit = false;
                }
            }
        }
    }

    public void Block()
    {
        RaycastHit2D[] hitdown = Physics2D.RaycastAll(transform.position, Vector2.down);

        for (int i = 0; i < hitdown.Length; i++)
        {
            if (hitdown[i].transform != null)
            {
                if (hitdown[i].distance < 0.5 && hitdown[i].collider.CompareTag("Wall"))
                {
                    if (verticalInput < 0)
                    {
                        verticalInput = 0;
                        down = true;
                    }
                }

            }
        }

        RaycastHit2D[] hitup = Physics2D.RaycastAll(transform.position, Vector2.up);
        for (int i = 0; i < hitup.Length; i++)
        {
            if (hitup[i].transform != null)
            {
                if (hitup[i].distance < 0.5 && hitup[i].collider.CompareTag("Wall"))
                {
                    if (verticalInput > 0)
                    {
                        verticalInput = 0;
                        up = true;
                    }
                }
            }
        }

        RaycastHit2D[] hitleft = Physics2D.RaycastAll(transform.position, Vector2.left);
        for (int i = 0; i < hitleft.Length; i++)
        {
            if (hitleft[i].transform != null)
            {
                if (hitleft[i].distance < 0.5 && hitleft[i].collider.CompareTag("Wall"))
                {
                    if (horizontalInput < 0)
                    {
                        horizontalInput = 0;
                        left = true;
                    }
                }

            }
        }

        RaycastHit2D[] hitright = Physics2D.RaycastAll(transform.position, Vector2.right);
        for (int i = 0; i < hitright.Length; i++)
        {
            if (hitright[i].transform != null)
            {
                if (hitright[i].distance < 0.5 && hitright[i].collider.CompareTag("Wall"))
                {
                    if (horizontalInput > 0)
                    {
                        horizontalInput = 0;
                        right = true;
                    }
                }
            }
        }

        RaycastHit2D[] hitDownRight = Physics2D.RaycastAll(transform.position, (Vector2.down + Vector2.right).normalized);
        for (int i = 0; i < hitDownRight.Length; i++)
        {
            if (hitDownRight[i].transform != null)
            {
                if (hitDownRight[i].distance < 0.5 && hitDownRight[i].collider.CompareTag("Wall"))
                {
                    if (horizontalInput > 0)
                    {
                        horizontalInput = 0;
                    }
                    if (verticalInput < 0)
                    {
                        verticalInput = 0;
                    }
                }
            }
        }

        RaycastHit2D[] hitDownLeft = Physics2D.RaycastAll(transform.position, (Vector2.down + Vector2.right).normalized);
        for (int i = 0; i < hitDownLeft.Length; i++)
        {
            if (hitDownLeft[i].transform != null)
            {
                if (hitDownLeft[i].distance < 0.5 && hitDownLeft[i].collider.CompareTag("Wall"))
                {
                    if (horizontalInput < 0)
                    {
                        horizontalInput = 0;
                    }
                    if (verticalInput < 0)
                    {
                        verticalInput = 0;
                    }
                }
            }
        }

        RaycastHit2D[] hitUpRight = Physics2D.RaycastAll(transform.position, (Vector2.down + Vector2.right).normalized);
        for (int i = 0; i < hitUpRight.Length; i++)
        {
            if (hitUpRight[i].transform != null)
            {
                if (hitUpRight[i].distance < 0.5 && hitUpRight[i].collider.CompareTag("Wall"))
                {
                    if (horizontalInput > 0)
                    {
                        horizontalInput = 0;
                    }
                    if (verticalInput > 0)
                    {
                        verticalInput = 0;
                    }
                }
            }
        }

        RaycastHit2D[] hitUpLeft = Physics2D.RaycastAll(transform.position, (Vector2.down + Vector2.right).normalized);
        for (int i = 0; i < hitUpLeft.Length; i++)
        {
            if (hitUpLeft[i].transform != null)
            {
                if (hitUpLeft[i].distance < 0.5 && hitUpLeft[i].collider.CompareTag("Wall"))
                {
                    if (horizontalInput < 0)
                    {
                        horizontalInput = 0;
                    }
                    if (verticalInput > 0)
                    {
                        verticalInput = 0;
                    }
                }
            }
        }

        if(!up || !down || !left || !right)
            up = down = left = right = false;
    }
}
