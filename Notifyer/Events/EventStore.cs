using System.Text.Json;

namespace Notifyer.Events;
public class EventStore() {
    List<BaseEvent> Events = [];

    public class CustomEventArgs : EventArgs {
        public BaseEvent Event { get; set; }
    }

    public event EventHandler<CustomEventArgs> eventHandler;
    
    public void StoreEvents(List<BaseEvent> events)
    {
        Events.AddRange(events);
        foreach (var e in events)
        {
            eventHandler(this, new CustomEventArgs { Event = e });
        }
        Console.WriteLine(JsonSerializer.Serialize(Events));
    }
}