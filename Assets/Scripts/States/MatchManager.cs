using GGJ26.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputManagerEntry;
using static UnityEngine.LowLevelPhysics2D.PhysicsComposer;

namespace GGJ26.StateMachine
{
    public class MatchManager : MonoBehaviour
    {
        public static MatchManager Instance { get; private set; }



        private void Awake()
        {
            // Gestione singleton
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }

        private IPlayableCharacter player1;
        private IPlayableCharacter player2;

        public InputHandler playerInput1;
        public InputHandler playerInput2;

        public bool IsOppeonentAttcking(IPlayableCharacter me)
        {
            if (player1 == null || player2 == null || me == null)
                return false;

            if (me.Equals(player1))
                return player2.IsAttacking();
            else
                return player1.IsAttacking();
        }

        public bool IsFacingRight(IPlayableCharacter me)
        {
            // fallback sicuri
            if (player1 == null || player2 == null || me == null)
                return true;

            Transform t1 = player1.GetTransform();
            Transform t2 = player2.GetTransform();

            if (t1 == null || t2 == null)
                return true;

            if (me.Equals(player1))
                return t1.position.x < t2.position.x;
            else
                return t2.position.x < t1.position.x;
        }

        internal void RegisterPlayer(IPlayableCharacter character)
        {
            if (player1 == null)
            {


                player1 = character;

                Bind(playerInput1, player1);

                



            }
            else if (player2 == null)
            {
                player2 = character;
                Bind(playerInput2, player2);
            }



        }


        public static void Bind(InputHandler handler, IPlayableCharacter playableCharacter)
        {
            playableCharacter.SetInputHandler(handler);
        }


    }


}



    
