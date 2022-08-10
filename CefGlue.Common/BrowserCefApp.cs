using System.Collections.Generic;
using Xilium.CefGlue.Common.Handlers;
using Xilium.CefGlue.Common.InternalHandlers;
using Xilium.CefGlue.Common.Shared;

namespace Xilium.CefGlue.Common
{
    internal class BrowserCefApp : CommonCefApp
    {
        private readonly CefBrowserProcessHandler _browserProcessHandler;
        private readonly KeyValuePair<string, string>[] _flags;
        private readonly List<string> _args;

        internal BrowserCefApp(CustomScheme[] customSchemes = null, List<string> args = null, KeyValuePair<string, string>[] flags = null, BrowserProcessHandler browserProcessHandler = null) :
            base(customSchemes)
        {
            _browserProcessHandler = new CommonBrowserProcessHandler(browserProcessHandler, customSchemes);
            _flags = flags;
            _args = args;
        }

        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {
            if (string.IsNullOrEmpty(processType))
            {
                if (CefRuntimeLoader.IsOSREnabled)
                {
                    commandLine.AppendSwitch("disable-gpu", "1");
                    commandLine.AppendSwitch("disable-gpu-compositing", "1");
                    commandLine.AppendSwitch("enable-begin-frame-scheduling", "1");
                    commandLine.AppendSwitch("disable-smooth-scrolling", "1");
                }

                if (_flags != null)
                {
                    foreach (var flag in _flags)
                    {
                        commandLine.AppendSwitch(flag.Key, flag.Value);
                    }
                }

                if (_args != null)
                {
                    foreach (var arg in _args)
                    {
                        commandLine.AppendArgument(arg);
                    }
                }
            }
        }

        protected override CefBrowserProcessHandler GetBrowserProcessHandler()
        {
            return _browserProcessHandler;
        }
    }
}
