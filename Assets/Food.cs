using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			if (CompareTag("Sushi"))
			{
				GameManager.score += 400;
			}
			if (CompareTag("Dango"))
			{
				GameManager.score += 600;
			}
			if (CompareTag("MilkTea"))
			{
				GameManager.score += 1200;
			}
			if (CompareTag("Ramen"))
			{
				GameManager.score += 2000;
			}
			Destroy(gameObject);

		}
	}
}
