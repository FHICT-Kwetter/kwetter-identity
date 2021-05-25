using System.Linq;
using IdentityServer.Domain.OAuth;
using IdentityServer4.Models;
using NUnit.Framework;

namespace IdentityServer.Test.OAuth
{
    public class ValidateOAuthClientsTest
    {
        [Test]
        public void Correct_Amount_Of_Clients_Exists()
        {
            Assert.AreEqual(ClientConstants.GetAll().Count(), 1);
        }

        [Test]
        public void Web_App_Client_Is_Correct()
        {
            Assert.AreEqual(ClientConstants.KwetterWebApp.ClientId, "kwetter-web-app");
            Assert.AreEqual(ClientConstants.KwetterWebApp.ClientSecrets.First().Value, "kwetter-web-app-secret".Sha256());
            Assert.AreEqual(ClientConstants.KwetterWebApp.AllowedGrantTypes, GrantTypes.ResourceOwnerPassword);
            Assert.IsTrue(ClientConstants.KwetterWebApp.AllowedScopes.Contains(IdentityResourceConstants.OpenIdScope.Name));
            Assert.IsTrue(ClientConstants.KwetterWebApp.AllowedScopes.Contains(IdentityResourceConstants.ProfileScope.Name));
            Assert.IsTrue(ClientConstants.KwetterWebApp.AllowedScopes.Contains(IdentityResourceConstants.EmailScope.Name));
            Assert.IsTrue(ClientConstants.KwetterWebApp.AllowedScopes.Contains("kweet.list"));
            Assert.IsTrue(ClientConstants.KwetterWebApp.AllowOfflineAccess);
            Assert.AreEqual(ClientConstants.KwetterWebApp.RefreshTokenUsage, TokenUsage.OneTimeOnly);
            Assert.AreEqual(ClientConstants.KwetterWebApp.RefreshTokenExpiration, TokenExpiration.Absolute);
            Assert.IsTrue(ClientConstants.KwetterWebApp.UpdateAccessTokenClaimsOnRefresh);
        }
    }
}