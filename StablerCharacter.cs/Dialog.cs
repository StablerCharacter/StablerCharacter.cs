namespace StablerCharacter
{
    public sealed class Dialog : IEquatable<Dialog>
    {
        public string? PersonSpeaking = string.Empty;
        public string Message = string.Empty;
        public event Action? Events;

        public bool Equals(Dialog? other)
        {
            if (other == null) return false;
            if (this == other) return true;
            if (Message != other.Message) return false;
            if (Events == other.Events) return true;
            return false;
        }

        public override string ToString()
        {
            return $"{(string.IsNullOrEmpty(PersonSpeaking) ? string.Empty : PersonSpeaking + ": ")}{Message}\nEvents: {Events?.ToString()}";
        }
    }
}
