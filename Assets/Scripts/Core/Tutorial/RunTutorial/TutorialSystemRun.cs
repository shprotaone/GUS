using Cysharp.Threading.Tasks;
using GUS.Core.Locator;
using GUS.Core.SaveSystem;
using GUS.LevelBuild;
using GUS.Player;
using UnityEngine;

namespace GUS.Core.Tutorial
{
    public class TutorialSystemRun
    {
        private SpecialPlatformBuilder _specialPlatformBuilder;
        private TutorialPlatform _tutorialPlatform;
        private StorageService _storageService;
        private PlayerActor _player;

        public int TutorialSteps { get; private set; }
        public async void Init(IServiceLocator serviceLocator, Transform container)
        {
            _specialPlatformBuilder = serviceLocator.Get<SpecialPlatformBuilder>();
            _player = serviceLocator.Get<PlayerActor>();
            _storageService = serviceLocator.Get<StorageService>();

            _player.InputType.Blocker(true);
            _specialPlatformBuilder.SetTutorial();

            await UniTask.Delay(1000);

           _tutorialPlatform = container.GetComponentInChildren<TutorialPlatform>();
            
            _tutorialPlatform.Init(serviceLocator);
            TutorialSteps = _tutorialPlatform.TriggerCount;

            await UniTask.Delay(1000);
                        
        }

        public void EndTutorial()
        {
            _player.InputType.Blocker(false);
            _player.MovementType.CanMove(true);
            _storageService.Data._tutorialSteps[1] = true;
            _storageService.Save();
        }
    }
}

