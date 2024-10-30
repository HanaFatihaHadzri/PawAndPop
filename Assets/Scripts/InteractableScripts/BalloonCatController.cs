using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonCatController : MonoBehaviour
{
   public GameObject pickupCat;

   public GameObject sadEmoticon;
   public GameObject happyEmoticon;

    public void SpawnCatToPickup()
   {
       Instantiate(pickupCat, transform.position, Quaternion.identity);
   }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("outBound"))
        {
            //GameManager.Instance.popup.AddToQueue("Cat Rescued !");
            GameManager.Instance._targetObjectSpawner.targetObjectRemove(gameObject);
            GameManager._instance.AddSavedCats(2); //assume 1 cat = 2 XP
            Destroy(gameObject);            
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.Play("CatDistress");
            gameObject.GetComponent<BalloonCatMovement>().moveSpeed = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Enable the BalloonCatMovement component
            BalloonCatMovement balloonMovement = gameObject.GetComponent<BalloonCatMovement>();
            if ( balloonMovement != null )
            {
                AudioManager.instance.Play("CatRelief");
                balloonMovement.enabled = true; // To activate the component
            }
        }
    }
}
