using System.Linq;
using IdentityServer.Domain.OAuth;
using IdentityServer4.Models;
using NUnit.Framework;

namespace IdentityServer.Test.OAuth
{
    public class ValidateOAuthIdentityResourcesTest
    {
        [Test]
        public void Correct_Amount_Of_Identity_Resources_Exists()
        {
            Assert.AreEqual(IdentityResourceConstants.GetAll().Count(), 3);
        }
        
        [Test]
        public void Open_Id_Scope_Is_Correct()
        {
            Assert.AreEqual(IdentityResourceConstants.OpenIdScope.Name, new IdentityResources.OpenId().Name);
        }
        
        [Test]
        public void Profile_Scope_Is_Correct()
        {
            Assert.AreEqual(IdentityResourceConstants.ProfileScope.Name, new IdentityResources.Profile().Name);
        }
        
        [Test]
        public void Email_Scope_Is_Correct()
        {
            Assert.AreEqual(IdentityResourceConstants.EmailScope.Name, new IdentityResources.Email().Name);
        }
    }
}