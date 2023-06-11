using lizzie;
using Serilog;
using Serilog.Events;

namespace StablerCharacter
{
    namespace Events
    {
        public class LogEvent : IDialogEvent
        {
            public LogEventLevel LogLevel = LogEventLevel.Debug;
            public string Content = string.Empty;

            public LogEvent(LogEventLevel logLevel, string content)
            {
                LogLevel = logLevel;
                Content = content;
            }

            public void Triggered()
            {
                switch (LogLevel)
                {
                    case LogEventLevel.Verbose:
                        Log.Verbose(Content);
                        break;
                    case LogEventLevel.Debug:
                        Log.Debug(Content);
                        break;
                    case LogEventLevel.Information:
                        Log.Information(Content);
                        break;
                    case LogEventLevel.Warning:
                        Log.Warning(Content);
                        break;
                    case LogEventLevel.Error:
                        Log.Error(Content);
                        break;
                    case LogEventLevel.Fatal:
                        Log.Fatal(Content);
                        break;
                }
            }
        }
    }
    
    public sealed partial class StoryConstructor
    {
        [Bind(Name = "verbose")]
        object? Verbose(Binder<StoryConstructor> _, Arguments arguments)
        {
            var e = new Events.LogEvent(LogEventLevel.Verbose, arguments.Get<string>(0));

            if (arguments.Get(1) != null)
            {
                e.Triggered();
                return null;
            }
            
            return e;
        }

        [Bind(Name = "debug")]
        object? Debug(Binder<StoryConstructor> _, Arguments arguments)
        {
            var e = new Events.LogEvent(LogEventLevel.Debug, arguments.Get<string>(0));

            if (arguments.Get(1) != null)
            {
                e.Triggered();
                return null;
            }

            return e;
        }

        [Bind(Name = "information")]
        object? Information(Binder<StoryConstructor> _, Arguments arguments)
        {
            var e = new Events.LogEvent(LogEventLevel.Information, arguments.Get<string>(0));

            if (arguments.Get(1) != null)
            {
                e.Triggered();
                return null;
            }

            return e;
        }

        [Bind(Name = "warning")]
        object? Warning(Binder<StoryConstructor> _, Arguments arguments)
        {
            var e = new Events.LogEvent(LogEventLevel.Warning, arguments.Get<string>(0));

            if (arguments.Get(1) != null)
            {
                e.Triggered();
                return null;
            }

            return e;
        }

        [Bind(Name = "error")]
        object? Error(Binder<StoryConstructor> _, Arguments arguments)
        {
            var e = new Events.LogEvent(LogEventLevel.Error, arguments.Get<string>(0));

            if (arguments.Get(1) != null)
            {
                e.Triggered();
                return null;
            }

            return e;
        }

        [Bind(Name = "fatal")]
        object? Fatal(Binder<StoryConstructor> _, Arguments arguments)
        {
            var e = new Events.LogEvent(LogEventLevel.Fatal, arguments.Get<string>(0));

            if (arguments.Get(1) != null)
            {
                e.Triggered();
                return null;
            }

            return e;
        }
    }
}
