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
        private int lastSpriteIndex = -1; // Per evitare di chiamare ChangeSprite se non cambia

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

            // Calcola l'indice dello sprite in base alla fase e al frame corrente
            int spriteIndex = CalculateSpriteIndex(newPhase);

            // Log fase solo quando cambia
            if (newPhase != phase)
            {
                switch (newPhase)
                {
                    case 0: 
                        Debug.Log($"Startup {motionName}"); 
                        break;
                    case 1: 
                        Debug.Log($"Active {motionName}");
                        break;
                    case 2: 
                        Debug.Log($"Recovery {motionName}");
                        break;
                }
                phase = newPhase;
            }

            // Cambia sprite solo se l'indice è cambiato
            if (spriteIndex != lastSpriteIndex && newPhase < 3)
            {
                // Durante recovery, changeOnlyHitbox solo se motion ha 1 solo sprite per fase (mosse normali)
                bool isMultiFrameMotion = motion.startupSpriteCount > 1 || motion.activeSpriteCount > 1 || motion.recoverySpriteCount > 1;
                bool changeOnlyHitbox = (newPhase == 2) && !isMultiFrameMotion;
                character.GetPlayerSpriteUpdater().ChangeSprite(motion.name, spriteIndex, changeOnlyHitbox);
                lastSpriteIndex = spriteIndex;
            }

            // Fine attacco → ritorno a Move
            if (newPhase == 3)
            {
                sm.ChangeState(new Move(character, sm));
            }
        }

        /// <summary>
        /// Calcola l'indice dello sprite in base alla fase corrente e al frame.
        /// Distribuisce i frame della fase equamente tra gli sprite disponibili.
        /// </summary>
        private int CalculateSpriteIndex(int currentPhase)
        {
            switch (currentPhase)
            {
                case 0: // Startup
                    {
                        int startupFrames = motion.startupEnd;
                        int framesPerSprite = Mathf.Max(1, startupFrames / motion.startupSpriteCount);
                        int index = Mathf.Min(frame / framesPerSprite, motion.startupSpriteCount - 1);
                        return index;
                    }
                case 1: // Active
                    {
                        int activeStart = motion.startupEnd;
                        int activeFrames = motion.activeEnd - activeStart;
                        int frameInPhase = frame - activeStart;
                        int framesPerSprite = Mathf.Max(1, activeFrames / motion.activeSpriteCount);
                        int index = Mathf.Min(frameInPhase / framesPerSprite, motion.activeSpriteCount - 1);
                        return motion.startupSpriteCount + index;
                    }
                case 2: // Recovery
                    {
                        int recoveryStart = motion.activeEnd;
                        int recoveryFrames = motion.totalFrames - recoveryStart;
                        int frameInPhase = frame - recoveryStart;
                        int framesPerSprite = Mathf.Max(1, recoveryFrames / motion.recoverySpriteCount);
                        int index = Mathf.Min(frameInPhase / framesPerSprite, motion.recoverySpriteCount - 1);
                        return motion.startupSpriteCount + motion.activeSpriteCount + index;
                    }
                default:
                    return 0;
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