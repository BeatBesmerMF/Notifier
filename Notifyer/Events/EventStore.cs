using System.Text.Json;

namespace Notifyer.Events;
public class EventStore (){
    List<BaseEvent> Events = [];

    public void StoreEvents(List<BaseEvent> events)
    {
        Events.AddRange(events);
        Console.WriteLine(JsonSerializer.Serialize(Events));
    }
}