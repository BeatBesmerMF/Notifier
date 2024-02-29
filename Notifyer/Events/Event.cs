using System.Text.Json;

namespace Notifyer.Events;

public class BaseEvent
{
    public string Type { get; }

    public DateTime DateTime { get; } = DateTime.UtcNow;
    public string Subject { get; }

    public BaseEvent(string Type, string Subject)
    {
        this.Type = Type;
        this.Subject = Subject;
    }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class Event<T> : BaseEvent where T : class
{
    public T Data { get; }
    public Event(string Subject, T Data) :
    base(Data.GetType().Name, Subject)
    {
        this.Data = Data;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public record UserSubscribedToMachine(
    string UserId,
    string MachineId
);
