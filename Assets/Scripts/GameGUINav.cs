using System;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUINav : MonoBehaviour
{


	private bool _paused;
	private bool quit;
	private string _errorMsg;

	public float initialDelay;

	public Canvas PauseCanvas;
	public Canvas GameOverCanvas;

	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// if scores are show, go back to main menu
			if (GameManager.gameState == GameManager.GameState.Paused)
				Menu();

			else
			{
				if (quit == true)
					ToggleQuit();
				else
					TogglePause();
			}
		}
	}

	// public handle to show ready screen coroutine call
	public void H_ShowReadyScreen()
	{
		StartCoroutine("ShowReadyScreen", initialDelay);
	}

	public void H_ShowGameOverScreen()
	{
		StartCoroutine("ShowGameOverScreen");
	}


	IEnumerator ShowGameOverScreen()
	{
		GameOverCanvas.enabled = true;
		yield return new WaitForSeconds(2);
		Menu();
	}


	//------------------------------------------------------------------
	// Button functions

	public void TogglePause()
	{
		if (_paused)
		{
			Time.timeScale = 1;
			PauseCanvas.enabled = false;
			_paused = false;
			//MenuButton.enabled = true;
		}

		else
		{
			PauseCanvas.enabled = true;
			Time.timeScale = 0;
			_paused = true;
			//MenuButton.enabled = false;
		}
	}

	public void ToggleQuit()
	{
		if (quit)
		{
			PauseCanvas.enabled = true;
			quit = false;
		}

		else
		{
			PauseCanvas.enabled = false;
			quit = true;
		}
	}

	public void Menu()
	{
		SceneManager.LoadScene("Menu");
		Time.timeScale = 1.0f;

		//GameManager.DestroySelf();
	}


	public void LoadLevel()
	{
		GameManager.Level++;
		SceneManager.LoadScene("Scene");
	}

}
