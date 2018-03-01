/* All states should be using this as a base so that it can work with the StateMachine */

public interface StateBase
{
    void EnterState();
    void UpdateState();
    void ExitState();

    string StateID
    { get; }
}