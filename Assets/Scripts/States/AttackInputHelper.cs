using GGJ26.Input;
using JetBrains.Annotations;
using System;
using UnityEngine;


namespace GGJ26.StateMachine
{

    public class AttackInputHelper
    {
        private IPlayableCharacter character;
        private InputCollector inputCollector;

        public AttackInputHelper(IPlayableCharacter character)
        {
            this.character = character;
            this.inputCollector = character.GetInputCollector();
        }

        // Controlla se dobbiamo passare a uno stato d'attacco
        public IState CheckAttackInput(StateMachineBehaviour sm)
        {
            InputData input = inputCollector.GetLastInputInBuffer();
            
            if (input == null) {
                return null;
            }

            if (MatchManager.Instance.IsOppeonentAttcking(character))
            {

                if (!(sm.current is LowBlock) &&
                        ((input.Movement == NumpadDirection.DownLeft && MatchManager.Instance.IsFacingRight(character)) ||
                        (input.Movement == NumpadDirection.DownRight && !MatchManager.Instance.IsFacingRight(character))))
                {
                    return new LowBlock(character, sm);
                }

                if (!(sm.current is Block) &&
                    ((input.Movement == NumpadDirection.Left && MatchManager.Instance.IsFacingRight(character)) ||
                    (input.Movement == NumpadDirection.Right && !MatchManager.Instance.IsFacingRight(character))))
                {
                    return new Block(character, sm);
                }

            }
            else
            {
                if (inputCollector.AttackPressedThisFrame() && !character.IsAttacking() && !(sm.current is Attack))
                {
                    Motion motion = inputCollector.GetMotion(MatchManager.Instance.IsFacingRight(character));
                    if (motion != null)
                        return new Attack(motion, character, sm);
                }

                

                if (!(sm.current is Crouch) &&
                    (input.Movement == NumpadDirection.Down ||
                     input.Movement == NumpadDirection.DownLeft ||
                     input.Movement == NumpadDirection.DownRight))
                {
                    return new Crouch(character, sm);
                }

                if (!(sm.current is Move) &&
                    (input.Movement == NumpadDirection.Left ||
                     input.Movement == NumpadDirection.Right))
                {
                    return new Move(character, sm);
                }

                if (!(sm.current is JumpStartup) &&
                    (input.Movement == NumpadDirection.Up ||
                     input.Movement == NumpadDirection.UpLeft ||
                     input.Movement == NumpadDirection.UpRight))
                {
                    return new JumpStartup(character, sm);
                }

                if (!(sm.current is Neutral) &&
                    input.Movement == NumpadDirection.Neutral)
                {
                    return new Neutral(character, sm);
                }

                
            }

            return null;
        }

        // Puoi aggiungere altri metodi per input generici in futuro
    }
}
