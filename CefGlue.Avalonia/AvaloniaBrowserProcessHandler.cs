using System;
using System.Reactive.Linq;
using Avalonia.Threading;

namespace Xilium.CefGlue.Avalonia
{
    internal class AvaloniaBrowserProcessHandler : CefBrowserProcessHandler
    {
        private IDisposable _current;
        private object _schedule = new object();

        protected override void OnScheduleMessagePumpWork(long delayMs)
        {
            lock (_schedule)
            {
                if (_current != null)
                {
                    _current.Dispose();
                }

                if (delayMs <= 0)
                {
                    delayMs = 1;
                }

                _current = Observable.Interval(TimeSpan.FromMilliseconds(delayMs)).ObserveOn(AvaloniaScheduler.Instance).Subscribe((i) =>
                {
                    CefRuntime.DoMessageLoopWork();
                });
            }
        }
    }
}