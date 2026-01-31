using GGJ26.Input;
using GGJ26.StateMachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    public PlayerInputManager playerInputManager;
    public int PlayerCounter = 0;

    private void Awake()
    {
       
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        

        PlayerCounter++;
        

        if (PlayerCounter >= 2)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
       PlayerInput[] playerInputs = GameObject.FindObjectsOfType<PlayerInput>(true);

        foreach (PlayerInput playerInput in playerInputs)
        {
            playerInput.gameObject.transform.SetParent(playerInputManager.transform);
        }



        Debug.Log("Starting Game...");
        SceneManager.LoadScene("CharacterSelect");
    }



    public void QuitGame()
    {
        Application.Quit();
    }
}