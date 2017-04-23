using Prism.Events;

namespace CC.Common.Infrastructure.Events
{
    public class FilePathChangedEvent : PubSubEvent<string>
    { }
}