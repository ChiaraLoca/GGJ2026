using GGJ26.Input;
using UnityEngine;

namespace GGJ26.StateMachine
{
    public class MatchManager : MonoBehaviour
    {

        public static MatchManager Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }

        IPlayableCharacter player1;
        IPlayableCharacter player2;

        public bool IsOppeonentAttcking(IPlayableCharacter me)
        { 
            if(player1==null || player2==null)
                return false;

            if (me.Equals(player1))
                return player2.IsAttacking();
            else
                return player1.IsAttacking();
        }

        public bool IsFacingRight(IPlayableCharacter me)
        {
            // Se non esiste player2, per design ritorniamo true
            if (player2 == null)
                return true;

            // Se me è null, fallback sicuro
            if (me == null)
                return true;

            // Se manca player1, fallback
            if (player1 == null)
                return true;

            // Sicurezza extra sui Transform
            Transform t1 = player1.GetTransform();
            Transform t2 = player2.GetTransform();

            if (t1 == null || t2 == null)
                return true;

            if (me.Equals(player1))
                return t1.position.x < t2.position.x;
            else
                return t2.position.x < t1.position.x;
        }


        internal void RegisterPlayer(MockPlayableCharacter mockPlayableCharacter)
        {
            if(player1==null)
                player1 = mockPlayableCharacter;
            else if (player2 == null)
                player2 = mockPlayableCharacter;

        }
    }
}
