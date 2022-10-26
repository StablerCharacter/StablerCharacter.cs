using System.Linq;

namespace StablerCharacter
{
    public sealed class Branch : IEquatable<Branch>
    {
        public Dialog[] dialogs;
        int dialogIndex = 0;

        public Branch(Dialog[] dialogs)
        {
            this.dialogs = dialogs;
        }

        public bool Equals(Branch? other)
        {
            if (this == other) return true;
            if (other == null) return false;
            if (dialogIndex != other.dialogIndex) return false;
            if (dialogs.Length != other.dialogs.Length) return false;
            for (ushort i = 0; i < dialogs.Length; i++)
            {
                if (!dialogs[i].Equals(other.dialogs[i])) return false;
            }
            return true;
        }

        public Dialog GetCurrentDialog() => dialogs[dialogIndex];

        public Dialog GetNextDialog() => dialogs[++dialogIndex];

        public override string ToString()
        {
            List<string> list = new();
            foreach (Dialog dialog in dialogs)
                list.Add(dialog.ToString());
            return $"dialogIndex: {dialogIndex}\n{string.Join('\n', list)}";
        }
    }
}
