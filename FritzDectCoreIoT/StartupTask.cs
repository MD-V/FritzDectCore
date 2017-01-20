using Windows.ApplicationModel.Background;
using Restup.Webserver.Rest;
using Restup.Webserver.Http;
using System;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace FritzDectCoreIoT
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {

            deferral = taskInstance.GetDeferral();

            // 
            // TODO: Insert code to perform background work
            //
            // If you start any asynchronous methods here, prevent the task
            // from closing prematurely by using BackgroundTaskDeferral as
            // described in http://aka.ms/backgroundtaskdeferral
            //

            try
            {
                var restRouteHandler = new RestRouteHandler();
                restRouteHandler.RegisterController<FritzDectController>();

                var configuration = new HttpServerConfiguration()
                  .ListenOnPort(8800)
                  .RegisterRoute("FritzDectCore", restRouteHandler)
                  .EnableCors();

                var httpServer = new HttpServer(configuration);
                await httpServer.StartServerAsync();

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

           

        }
    }
}
