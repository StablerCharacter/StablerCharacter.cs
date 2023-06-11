using Raylib_cs;
using System.Numerics;

namespace StablerCharacter.RenderObjects
{
    public sealed class StoryDialog : RenderObject
    {
        public DialogStyle DialogStyle;
        readonly StoryManager storyManager;
        Vector2 rectanglePosition;
        Vector2 personSpeakingTextPosition;
        Vector2 messageTextPosition;
        Dialog currentDialog = new();

        public StoryDialog(DialogStyle dialogStyle, StoryManager storyManager)
        {
            this.DialogStyle = dialogStyle;
            this.storyManager = storyManager;
        }

        public override void OnStart()
        {
            Vector2 windowSize = new(GameManager.GameConfig.WindowWidth, GameManager.GameConfig.WindowHeight);
            rectanglePosition = DialogStyle.dialogPosition.GetPosition(windowSize);
            rectanglePosition.X -= DialogStyle.dialogBoxSize.X / 2;
            rectanglePosition.Y -= DialogStyle.dialogBoxSize.Y;
            personSpeakingTextPosition = DialogStyle.personSpeakingTextPosition.GetPosition(windowSize, true, DialogStyle.dialogBoxSize, rectanglePosition);
            messageTextPosition = DialogStyle.messagePosition.GetPosition(windowSize, true, DialogStyle.dialogBoxSize, rectanglePosition);
            messageTextPosition.Y -= DialogStyle.messageTextInfo.FontSize / 2;

            ClickableArea = new Rectangle(rectanglePosition.X, rectanglePosition.Y, DialogStyle.dialogBoxSize.X, DialogStyle.dialogBoxSize.Y);

            currentDialog = storyManager.GetCurrentDialog();
        }

        public override void Render()
        {
            PollEvents();

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                if (storyManager.TryGetNextDialog(out Dialog dialog))
                    currentDialog = dialog;
            }

            Raylib.DrawFPS(20, 20);
            Raylib.DrawRectangle(
                (int)rectanglePosition.X,
                (int)rectanglePosition.Y,
                (int)DialogStyle.dialogBoxSize.X,
                (int)DialogStyle.dialogBoxSize.Y,
                DialogStyle.dialogBoxBackgroundColor
            );
            if(string.IsNullOrWhiteSpace(currentDialog.PersonSpeaking))
            {
                Raylib.DrawTextEx(
                    DialogStyle.personSpeakingTextInfo.Font,
                    currentDialog.PersonSpeaking,
                    personSpeakingTextPosition,
                    DialogStyle.personSpeakingTextInfo.FontSize,
                    DialogStyle.personSpeakingTextInfo.TextSpacing,
                    DialogStyle.personSpeakingTextInfo.TextColor
                );
            }
            Raylib.DrawTextPro(
                DialogStyle.messageTextInfo.Font,
                currentDialog.Message,
                messageTextPosition,
                Vector2.Zero,
                0f,
                DialogStyle.messageTextInfo.FontSize,
                DialogStyle.messageTextInfo.TextSpacing,
                DialogStyle.messageTextInfo.TextColor
            );
        }

        public override void OnRemoved()
        {
        }
    }
}
