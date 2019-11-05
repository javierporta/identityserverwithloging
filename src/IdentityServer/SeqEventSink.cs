using IdentityServer4.Events;
using IdentityServer4.Services;
using Serilog;
using Serilog.Core;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class SeqEventSink : IEventSink
    {
        private readonly Logger _log;
        private readonly Logger _textLog;

        public SeqEventSink()
        {
            _log = new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            _textLog = new LoggerConfiguration()
                .WriteTo.File(@"identityserver4_events.txt")
                .CreateLogger();
        }

        public Task PersistAsync(Event evt)
        {
            if (evt.EventType == EventTypes.Success ||
                evt.EventType == EventTypes.Information)
            {
                _log.Information("{Name} ({Id}), Details: {@details}",
                    evt.Name,
                    evt.Id,
                    evt);

                _textLog.Information("{Name} ({Id}), Details: {@details}",
                    evt.Name,
                    evt.Id,
                    evt);
            }
            else
            {
                _log.Error("{Name} ({Id}), Details: {@details}",
                    evt.Name,
                    evt.Id,
                    evt);

                _textLog.Error("{Name} ({Id}), Details: {@details}",
                                    evt.Name,
                                    evt.Id,
                                    evt);
            }

            return Task.CompletedTask;
        }
    }
}
