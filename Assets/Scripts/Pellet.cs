using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			GameManager.score += 10;
			GameObject[] pellets = GameObject.FindGameObjectsWithTag("Pellet");
			Destroy(gameObject);

			if (pellets.Length == 1)
			{
				//GameObject.FindObjectOfType<GameGUINavigation>().LoadLevel();
			}
		}
	}
}
