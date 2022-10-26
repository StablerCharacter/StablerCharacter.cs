namespace StablerCharacter
{
    public sealed class Dialog : IEquatable<Dialog>
    {
        public string? personSpeaking = string.Empty;
        public string message = string.Empty;
        public event EventHandler? events;

        public bool Equals(Dialog? other)
        {
            if (other == null) return false;
            if (this == other) return true;
            if (message != other.message) return false;
            if (events == other.events) return true;
            return false;
        }

        public override string ToString()
        {
            return $"{(string.IsNullOrEmpty(personSpeaking) ? string.Empty : personSpeaking + ": ")}{message}\nEvents: {events?.ToString()}";
        }
    }
}
