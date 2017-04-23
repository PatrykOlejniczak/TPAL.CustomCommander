using System.Collections.Generic;
using CC.Common.Infrastructure.Models;
using Prism.Events;

namespace CC.Common.Infrastructure.Events
{
    public class SelectFileChangedEvent : PubSubEvent<List<FileModel>>
    { }
}