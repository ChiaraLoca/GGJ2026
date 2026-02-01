using GGJ26.Input;
using GGJ26.StateMachine;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MainMenuController : MonoBehaviour
{
    public PlayerInputManager playerInputManager;
    public int PlayerCounter = 0;
    [SerializeField] public GameObject canvaObj;


    private void Awake()
    {
       
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        

        PlayerCounter++;
        if(canvaObj != null)
        {
            if(PlayerCounter == 1)
            {
                //cerco il componente di tipo GameObject tra i figli di canvaObj con nome "Player1Active" e lo attivo
                canvaObj.GetComponentsInChildren<Transform>(true).FirstOrDefault(obj => obj.name == "Player1Active")?.gameObject.SetActive(true);
            }

            if (PlayerCounter == 2)
            {
                canvaObj.GetComponentsInChildren<Transform>(true).FirstOrDefault(obj => obj.name == "Player2Active")?.gameObject.SetActive(true);
                canvaObj.GetComponentsInChildren<Transform>(true).FirstOrDefault(obj => obj.name == "StartButton")?.gameObject.SetActive(true);

            }

        }

        //if (PlayerCounter >= 2)
        //{
        //    StartGame();
        //}
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