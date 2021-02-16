using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicClipOne;
   
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			GameManager.score += 10;
			GameObject[] pellets = GameObject.FindGameObjectsWithTag("Pellet");
            musicSource.clip = musicClipOne;
            musicSource.Play();
			PlayerController.pelletCount += 1;
            Destroy(gameObject);

			if (pellets.Length == 1)
			{
				//GameObject.FindObjectOfType<GameGUINavigation>().LoadLevel();
			}
		}
	}
}
