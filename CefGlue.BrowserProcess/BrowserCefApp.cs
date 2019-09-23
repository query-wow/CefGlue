using Xilium.CefGlue.BrowserProcess.Handlers;
using Xilium.CefGlue.Common;

namespace Xilium.CefGlue.BrowserProcess
{
    internal class BrowserCefApp : CommonCefApp
    {
        private readonly CefRenderProcessHandler _renderProcessHandler = new CommonCefRenderProcessHandler();

        internal BrowserCefApp(CustomScheme[] customSchemes) : base(customSchemes)
        {
        }

        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            return _renderProcessHandler;
        }
    }
}