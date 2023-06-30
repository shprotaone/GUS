using GUS.Core.Locator;
using GUS.Objects;
using UnityEngine;

namespace GUS.Core.Tutorial
{
    public class TutorialPlatform : Platform
    {
        private IStepTrigger[] _triggers;
        public int TriggerCount { get;private set; }
        public void Init(IServiceLocator serviceLocator)
        {
            _triggers = GetComponentsInChildren<IStepTrigger>();
            TriggerCount= _triggers.Length;

            foreach(var trigger in _triggers)
            {
                trigger.Init(serviceLocator);
            }
        }
    }
}