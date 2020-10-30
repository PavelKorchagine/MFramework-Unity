using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// EventTriggerExtension
    /// </summary>
    public static class EventTriggerExtension
    {
        /// <summary>
        /// RegisterEventTrigger
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="eventTriggerType"></param>
        /// <param name="callback"></param>
        public static void RegisterEventTrigger(this EventTrigger trigger, EventTriggerType eventTriggerType
            , UnityAction<BaseEventData> callback)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventTriggerType;
            entry.callback.AddListener(callback);
            trigger.triggers.Add(entry);

        }

        /// <summary>
        /// RegisterEventTrigger
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="eventTriggerType"></param>
        public static void RemoveEventTrigger(this EventTrigger trigger, EventTriggerType eventTriggerType)
        {
            EventTrigger.Entry entry = null;

            for (int i = 0; i < trigger.triggers.Count; i++)
            {
                if (trigger.triggers[i].eventID == eventTriggerType)
                {
                    entry = trigger.triggers[i];

                    if (trigger.triggers.Contains(entry))
                        trigger.triggers.Remove(entry);
                }
            }
        }
    }
}
