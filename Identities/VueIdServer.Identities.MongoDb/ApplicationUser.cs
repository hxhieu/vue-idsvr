using System;
using AspNetCore.Identity.MongoDbCore.Models;

namespace VueIdServer.Identities.MongoDb
{
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
        public ApplicationRole() : base()
        {
        }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}