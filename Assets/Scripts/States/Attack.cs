using GGJ26.Input;
using System.Linq;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public class Attack : IState
    {
        private IPlayableCharacter character;
        private StateMachineBehaviour sm;
        public Motion motion;
        private int frame;
        private int phase; // 0=startup, 1=active, 2=recovery
        private string motionName;

        public Attack(Motion motion, IPlayableCharacter character, StateMachineBehaviour sm)
        {
            this.motion = motion;
            this.character = character;
            this.sm = sm;
            frame = 0;
            phase = -1;
            motionName = string.Join(",", motion.Inputs.Select(x => x.ToString()));
        }

        public void OnEnter()
        {
            
            //Debug.Log($"Attack Enter: {motionName}");
            Debug.Log($"ATTACK {character.GetInputCollector().Print()}");
            character.SetAttacking(true);

            character.GetPlayerSpriteUpdater().ChangeSprite(motion.name, 0);
        }

        public void OnFrame()
        {
            frame++;

            int newPhase = frame < motion.startupEnd ? 0 :
                           frame < motion.activeEnd ? 1 :
                           frame < motion.totalFrames ? 2 : 3;

            // Log fase solo quando cambia
            if (newPhase != phase)
            {
                switch (newPhase)
                {
                    case 0: Debug.Log($"Startup {motionName}"); break;
                    case 1: Debug.Log($"Active {motionName}"); break;
                    case 2: Debug.Log($"Recovery {motionName}"); break;
                }
                phase = newPhase;
            }

            // Durante active puoi gestire hitbox qui
            if (phase == 1)
            {
                character.GetPlayerSpriteUpdater().ChangeSprite(motion.name, 1);
                // character.SpawnHitbox(motion.hitboxData);
            }
            if (phase == 2)
            {
                character.GetPlayerSpriteUpdater().ChangeSprite(motion.name, 2);
                // character.SpawnHitbox(motion.hitboxData);
            }

            // Fine attacco → ritorno a Move
            if (newPhase == 3)
            {
                sm.ChangeState(new Move(character, sm));
            }
        }

        public void OnExit()
        {
            Debug.Log($"Attack Exit: {motionName}");
            character.SetAttacking(false);
            character.GetPlayerSpriteUpdater().ChangeSprite("idle",0);
        }
    }
}