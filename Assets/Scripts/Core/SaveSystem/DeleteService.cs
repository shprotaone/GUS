using GUS.Core.Locator;

namespace GUS.Core.SaveSystem
{
    public class DeleteService
    {
        private JsonToFirebase _jsonToFireBase;
        private StorageService _storageService;

        public void Init(IServiceLocator serviceLocator)
        {
            _jsonToFireBase = serviceLocator.Get<JsonToFirebase>();
            _storageService = serviceLocator.Get<StorageService>();
        }

        public void Execute()
        {
            //_jsonToFireBase.Delete(_storageService.Data);
            _storageService.Delete();
        }
    }
}

