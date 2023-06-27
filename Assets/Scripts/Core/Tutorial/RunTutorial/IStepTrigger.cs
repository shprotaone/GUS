using GUS.Core.Locator;
using UnityEngine;

namespace GUS.Core.Tutorial
{
    public interface IStepTrigger
    {
        void Init(IServiceLocator serviceLocator);
        void OnTriggerEnter(Collider other);
    }
}
