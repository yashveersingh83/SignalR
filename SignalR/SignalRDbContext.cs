using System.Data.Entity;
using SignalR.Models;

namespace SignalrDemo
{
    public class SignalRDbContext:DbContext
    {
        public SignalRDbContext()
            : base("name=SignalRConnection")
        {
            
        }
        public DbSet<InformationRequest> InformationRequests { get; set; }
    }
}