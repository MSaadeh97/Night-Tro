using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "knightro")
		{
			GameManager.score += 10;
			GameObject[] pellets = GameObject.FindGameObjectsWithTag("pellets");
			Destroy(gameObject);

			if (pellets.Length == 1)
			{
				//GameObject.FindObjectOfType<GameGUINavigation>().LoadLevel();
			}
		}
	}
}
