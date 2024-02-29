using System.Text.Json;

namespace Notifyer.Events;

public class Event<T>
{
    public string Type { get; } = nameof(T);
    public DateTime DateTime { get; } = DateTime.UtcNow;
    public string Subject { get; }
    public T Data { get; }
    public Event(string Subject, T Data)
    {
        this.Subject = Subject;
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
