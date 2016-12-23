using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Owin;
using SignalR.Models;

namespace SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }

    }

    public class InformationRequestHub : Hub
    {
        [HubMethodName("NotifyInformationRequestToClient")]

        public static void NotifyInformationRequestToClient()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<InformationRequestHub>();
            context.Clients.All.updatedClients();
        }

        public static void NotifyUpdatedInformationRequestToClient(InformationRequest request)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<InformationRequestHub>();
            context.Clients.All.updatedModifyInformationRequestClients(request);
        }
    }
}