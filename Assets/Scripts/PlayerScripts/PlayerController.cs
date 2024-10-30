using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerHp health;
    public Image hpBar;
    IEnumerator running;
    private Coroutine hpUpdateCoroutine;
    private bool isInvincible = false;

    public PlayerMovement playerMove;

    private SpriteRenderer rend;

    void Start()
    {
        //playerMove = GetComponent<PlayerMovement>();
        health = new PlayerHp(100,100);
        UpdateHPtoMax();

        rend = GetComponent<SpriteRenderer>();
    }
    
    /*-----------PLAYER COLLIDER SCRIPTINGS START--------------*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && !isInvincible) //balloon animals
        {
            DeductHp();
            StartCoroutine(InvincibilityCoroutine());
            //Debug.Log("Player collide enemy and hp deduct!");
        }
        if(collision.gameObject.CompareTag("HpFountain"))
        {
            AddHp();
            //Debug.Log("Player refill Hp!");
        }
    }
    /*-----------PLAYER COLLIDER SCRIPTINGS END--------------*/

    /*-----------PLAYER HP SCRIPTINGS START--------------*/
    void UpdateHpBar(float goalHp)
    {
        if (hpUpdateCoroutine != null)
        {
            StopCoroutine(hpUpdateCoroutine);
        }
        hpUpdateCoroutine = StartCoroutine(LerpHp(goalHp));
    }

    void StopLerpHP()
    {
        if (running != null)
        {

            StopCoroutine(running);
            running = null;
        }
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
        health._hp = Mathf.Max(0, health._hp - 5);
        UpdateHpBar(health._hp);
        
        if (health._hp <= 0)
        {
            UiManager._instance.GameOver();
        }
    }

    public void AddHp()
    {
        health._hp = Mathf.Min(health._hp + 10, health._maxHp);
        UpdateHpBar(health._hp);
    }

    public void ResetHealth()
    {
        if (hpUpdateCoroutine != null)
        {
            StopCoroutine(hpUpdateCoroutine);
        }
        health._hp = health._maxHp;
        UpdateHPtoMax();
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        //add visual feedback here - red n semi transparent
        rend.color = new Color(1f, 0f, 0f, .5f);

        //play sfx
        AudioManager.instance.Play("PlayerHurt");

        yield return new WaitForSeconds(1f);
        rend.color = new Color(1f, 1f, 1f, 1f);
        isInvincible = false;
    }
    /*-----------PLAYER HP SCRIPTINGS END--------------*/


}
