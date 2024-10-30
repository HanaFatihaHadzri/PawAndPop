using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrongBalloonDogController : EnemyBaseController
{
    public EnemyHp health;
    public Image hpBar;
    public IEnumerator running;
    private Coroutine hpUpdateCoroutine;
    public float invincibilityDuration = 1f;
    public bool isInvincible = false;

    private void Start()
    {
        health = new EnemyHp(100, 100);
        UpdateHPtoMax();
    }

    
    /*-----------ENEMY COLLIDER SCRIPTINGS START--------------*/
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWhip")) //player's bullet & flying weapon
        {
            Destroy(collision.gameObject); //destroy player's whip after hit the enemy

            DeductHp();

            EnemyGetHit();

            Debug.Log("Enemy get hit and hp deduct!");
        }
        
        if (collision.gameObject.CompareTag("PlayerPopper")) //player's bullet & flying weapon
        {
            DeductHp();

            EnemyGetHit();

            Debug.Log("Enemy Defeated !");
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //when balloon dog STAY collide with cat
    {
        if (collision.gameObject.CompareTag("CatPickup"))
        {
            AudioManager.instance.Play("CatDistress");
            collision.gameObject.GetComponent<BalloonCatController>().sadEmoticon.SetActive(true);
            collision.gameObject.GetComponent<BalloonCatController>().happyEmoticon.SetActive(false);
            collision.gameObject.GetComponent<BalloonCatMovement>().moveSpeed = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //when balloon dog EXIT collide with cat
    {
        if (collision.gameObject.CompareTag("CatPickup"))
        {
            AudioManager.instance.Stop("CatDistress");
            collision.gameObject.GetComponent<BalloonCatController>().sadEmoticon.SetActive(false);
            collision.gameObject.GetComponent<BalloonCatController>().happyEmoticon.SetActive(true);
            collision.gameObject.GetComponent<BalloonCatMovement>().moveSpeed = 3f;
        }
    }

    public void EnemyGetHit()
    {
        //defeated sfx
        AudioManager.instance.Play("EnemyDefeat2");

        //vibrate
        //TriggerVibrationPattern();

        //instatiate hit prefab
        GameObject hitInstance = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        Destroy(hitInstance, .5f); //finish animation then destroy after 0.5 sec
    }

    /*-----------ENEMY COLLIDER SCRIPTINGS END--------------*/

    /*-----------ENEMY HP SCRIPTINGS START--------------*/
    void UpdateHpBar(float goalHp)
    {
        if (hpUpdateCoroutine != null)
        {
            StopCoroutine(hpUpdateCoroutine);
        }
        hpUpdateCoroutine = StartCoroutine(LerpHp(goalHp));
    }

    public void UpdateHPtoMax()
    {
        hpBar.fillAmount = 1;

    }

    IEnumerator LerpHp(float goalHp, float duration = 0.2f)
    {
        float startHp = hpBar.fillAmount * health._maxHp;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newHp = Mathf.Lerp(startHp, goalHp, elapsedTime / duration);
            hpBar.fillAmount = newHp / health._maxHp;
            yield return null;
        }

        hpBar.fillAmount = goalHp / health._maxHp;
        hpUpdateCoroutine = null;
    }

    public void DeductHp()
    {
        //health._hp = Mathf.Max(0, health._hp - 10);
        health._hp = health._hp - 10;

        UpdateHpBar(health._hp);

        if (health._hp <= 0)
        {
            EnemyGetHit();

            GameManager._instance.AddDefeatEnemy(); //for UI usage 

            StartCoroutine(PlayEnemyDefeatSfx(.5f));
        }
    }

    IEnumerator PlayEnemyDefeatSfx(float clipLength)
    {
        AudioManager.instance.Play("EnemyDefeat1");
        yield return new WaitForSeconds(clipLength);
        DestroyEnemy();
        Debug.Log("Enemy Defeated !");
    }


    /*-----------ENEMY HP SCRIPTINGS END--------------*/
}
