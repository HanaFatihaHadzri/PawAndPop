using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushController : MonoBehaviour
{
    //public AudioSource popSfx;
    public GameObject hitEffectPrefab;
    public GameObject afterHitEffectPrefab; //leaves falls effect?

    private int getHitCount;

    public AudioSource getHitSfx;

    private void Start()
    {
        getHitCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWhip")) //player's bullet
        {
            Destroy(collision.gameObject); //destroy player's whip after hit

            //DeductHp();

            GetHit();

            Debug.Log("Enemy get hit and hp deduct!");
        }
    }
    public void GetHit()
    {
        //PlayHitSfx();
        getHitSfx.Play();

        //instatiate hit prefab
        GameObject hitInstance = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        Destroy(hitInstance, .5f); //finish animation then destroy after 0.5 sec
    }

    public void DeductHp()
    {
        //health._hp = Mathf.Max(0, health._hp - 10);
        getHitCount--;

        if (getHitCount <= 0)
        {
            GetHit();

            //other sfx play here

            Destroy(gameObject);

            Debug.Log("Bush collider collapsed !");
        }
    }
}
