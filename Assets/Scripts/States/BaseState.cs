using GGJ26.Input;
using UnityEngine;


namespace GGJ26.StateMachine
{
    public abstract class BaseState : IState
    {
        protected StateMachineBehaviour StateMachineBehaviour;
        protected IPlayableCharacter PlayableCharacter;
        protected int Frame;
        
        public virtual void OnEnter()
        {
            Debug.Log($"{GetType().Name} Enter");
        }

        public virtual void OnFrame()
        {
            Debug.Log($"{GetType().Name} Frame {Frame}");
            Frame++;
        }

        public virtual void OnExit()
        {
            Debug.Log($"{GetType().Name} Exit");
        }
    }

}