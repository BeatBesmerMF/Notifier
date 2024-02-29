using Notifyer.Events;

namespace Notifyer.State;

public class StatisticsState
{
    public Dictionary<string, int> Statistics { get; } = new();

    public void Project(BaseEvent Event)
    {
        switch (Event.Type)
        {
            case nameof(UserSubscribedToMachine):
                if (!Statistics.ContainsKey(nameof(UserSubscribedToMachine)))
                {
                    Statistics[nameof(UserSubscribedToMachine)] = 0;
                }
                Statistics[nameof(UserSubscribedToMachine)]++;
                break;
        }
        
    }
}
