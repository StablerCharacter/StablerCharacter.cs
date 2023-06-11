using System.Linq;

namespace StablerCharacter
{
    public sealed class Branch : IEquatable<Branch>
    {
        public Dialog[] Dialogs;
        int dialogIndex = 0;

        public Branch(Dialog[] dialogs)
        {
            this.Dialogs = dialogs;
        }

        public Dialog GetCurrentDialog() => Dialogs[dialogIndex];

        public Dialog GetNextDialog() => Dialogs[++dialogIndex];
        public bool TryGetNextDialog(out Dialog dialog)
        {
            if (++dialogIndex >= Dialogs.Length)
            {
                dialog = new();
                return false;
            }
            dialog = Dialogs[dialogIndex];
            return true;
        }

        public override string ToString()
        {
            List<string> list = new();
            foreach (Dialog dialog in Dialogs)
                list.Add(dialog.ToString());
            return $"dialogIndex: {dialogIndex}\n{string.Join('\n', list)}";
        }

        public bool Equals(Branch? other)
        {
            if (this == other) return true;
            if (other == null) return false;
            if (dialogIndex != other.dialogIndex) return false;
            if (Dialogs.Length != other.Dialogs.Length) return false;
            for (ushort i = 0; i < Dialogs.Length; i++)
            {
                if (!Dialogs[i].Equals(other.Dialogs[i])) return false;
            }
            return true;
        }

        public override bool Equals(object? obj) => Equals(obj as Branch);

        public override int GetHashCode() => Dialogs.Length.GetHashCode() + dialogIndex.GetHashCode();
    }
}
