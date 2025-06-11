using System;
using System.Collections.Generic;

public class CreateStateFactory : Singleton<CreateStateFactory>
{
    public Dictionary<Type, IState> GenerateState(IStateFactory stateFactory)
    {
        return stateFactory.CreateState();
    }
}
public interface IStateFactoryProvider
{
    IStateFactory GetFactory(string type, object controller, IStateMachine machine);
}
