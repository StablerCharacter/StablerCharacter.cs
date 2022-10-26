using Raylib_cs;
using System.Numerics;

namespace StablerCharacter.RenderObjects
{
    public sealed class StoryDialog : RenderObject
    {
        public DialogStyle dialogStyle;
        Vector2 rectanglePosition;
        Vector2 personSpeakingTextPosition;
        Vector2 messageTextPosition;
        readonly StoryManager storyManager;

        public StoryDialog(DialogStyle dialogStyle, StoryManager storyManager)
        {
            this.dialogStyle = dialogStyle;
            this.storyManager = storyManager;
        }

        public override void OnStart()
        {
            Vector2 windowSize = new(GameManager.gameConfig.windowWidth, GameManager.gameConfig.windowHeight);
            rectanglePosition = dialogStyle.dialogPosition.GetPosition(windowSize);
            rectanglePosition.X -= dialogStyle.dialogBoxSize.X / 2;
            rectanglePosition.Y -= dialogStyle.dialogBoxSize.Y;
            personSpeakingTextPosition = dialogStyle.personSpeakingTextPosition.GetPosition(windowSize, true, dialogStyle.dialogBoxSize, rectanglePosition);
            messageTextPosition = dialogStyle.messagePosition.GetPosition(windowSize, true, dialogStyle.dialogBoxSize, rectanglePosition);
            messageTextPosition.Y -= dialogStyle.messageTextInfo.fontSize / 2;

            clickableArea = new Rectangle(rectanglePosition.X, rectanglePosition.Y, dialogStyle.dialogBoxSize.X, dialogStyle.dialogBoxSize.Y);
        }

        public override void Render()
        {
            PollEvents();

            Raylib.DrawFPS(20, 20);
            Raylib.DrawRectangle(
                (int)rectanglePosition.X,
                (int)rectanglePosition.Y,
                (int)dialogStyle.dialogBoxSize.X,
                (int)dialogStyle.dialogBoxSize.Y,
                dialogStyle.dialogBoxBackgroundColor
            );
            Dialog dialog = storyManager.GetCurrentDialog();
            if(string.IsNullOrWhiteSpace(dialog.personSpeaking))
            {
                Raylib.DrawTextEx(
                    dialogStyle.personSpeakingTextInfo.font,
                    dialog.personSpeaking,
                    personSpeakingTextPosition,
                    dialogStyle.personSpeakingTextInfo.fontSize,
                    dialogStyle.personSpeakingTextInfo.textSpacing,
                    dialogStyle.personSpeakingTextInfo.textColor
                );
            }
            Raylib.DrawTextPro(
                dialogStyle.messageTextInfo.font,
                dialog.message,
                messageTextPosition,
                new(),
                0f,
                dialogStyle.messageTextInfo.fontSize,
                dialogStyle.messageTextInfo.textSpacing,
                dialogStyle.messageTextInfo.textColor
            );
        }

        public override void OnRemoved()
        {
        }
    }
}
