namespace GUS.Core.Tutorial
{
    public interface ITutorialStep
    {
        void ShowText(string text);
        void Activate(TutorialSystem tutorial);
        void Deactivate();
        void Next();
    }
}