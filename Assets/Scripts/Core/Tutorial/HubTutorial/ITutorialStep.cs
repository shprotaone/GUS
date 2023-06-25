namespace GUS.Core.Tutorial
{
    public interface ITutorialStep
    {
        void ShowText(string text);
        void Activate(TutorialSystemHUB tutorial);
        void Deactivate();
        void Next();
    }
}