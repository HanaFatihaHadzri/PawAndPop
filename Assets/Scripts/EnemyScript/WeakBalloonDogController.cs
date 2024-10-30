using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakBalloonDogController : EnemyBaseController
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerWhip")) //player's bullet & flying weapon
        {
            Destroy(collision.gameObject); //destroy player's whip after hit the enemy

            //defeated sfx
            AudioManager.instance.Play("EnemyDefeat1");
            //vibrate
            //TriggerVibrationPattern();

            //instatiate hit prefab
            GameObject hitInstance = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(hitInstance, .5f); //finish animation then destroy after 0.5 sec

            GameManager._instance.AddDefeatEnemy(); //for UI usage 

            DestroyEnemy();

            Debug.Log("Enemy Defeated !");
        }

        if (collision.gameObject.CompareTag("PlayerPopper")) //player's bullet & flying weapon
        {
            //defeated sfx
            AudioManager.instance.Play("EnemyDefeat1");

            //instatiate hit prefab
            GameObject hitInstance = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(hitInstance, .5f); //finish animation then destroy after 0.5 sec

            GameManager._instance.AddDefeatEnemy(); //for UI usage 

            DestroyEnemy();

            Debug.Log("Enemy Defeated !");
        }

    }

    private void OnTriggerStay2D(Collider2D collision) //when balloon dog STAY collide with cat
    {
        if(collision.gameObject.CompareTag("CatPickup"))
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
}
