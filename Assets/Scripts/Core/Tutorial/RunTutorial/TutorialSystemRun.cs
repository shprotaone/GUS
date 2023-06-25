using Cysharp.Threading.Tasks;
using GUS.Core.Locator;
using GUS.Core.Pool;
using GUS.LevelBuild;
using GUS.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.Core.Tutorial
{
    public class TutorialSystemRun
    {
        private SpecialPlatformBuilder _specialPlatformBuilder;
        private TutorialPlatform _tutorialPlatform;
        private IServiceLocator _serviceLocator;

        public async void Init(IServiceLocator serviceLocator, Transform container)
        {
            _specialPlatformBuilder = serviceLocator.Get<SpecialPlatformBuilder>();
            _serviceLocator = serviceLocator;
            _specialPlatformBuilder.SetTutorial();
           
            await UniTask.Delay(1000);

           _tutorialPlatform = container.GetComponentInChildren<TutorialPlatform>();

            _tutorialPlatform.Init(_serviceLocator);
        }
    }
}

