﻿using Prism.Events;

namespace CC.Common.Infrastructure.Events
{
    public class ActualFilePathChangedEvent : PubSubEvent<string>
    { }
}