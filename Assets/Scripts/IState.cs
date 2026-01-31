namespace GGJ26.StateMachine
{
    public interface IState
    {
        public void OnEnter();
        public void OnFrame();
        public void OnExit();
    }

}