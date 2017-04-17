using Prism.Events;

namespace CC.Common.Infrastructure.Events
{
    public class DirectoryChangedEvent : PubSubEvent<string>
    { }
}
