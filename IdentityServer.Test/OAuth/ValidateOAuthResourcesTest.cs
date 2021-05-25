using System.Linq;
using IdentityServer.Domain.OAuth;
using NUnit.Framework;

namespace IdentityServer.Test.OAuth
{
    public class ValidateOAuthResourcesTest
    {
        [Test]
        public void Correct_Amount_Of_Resources_Exists()
        {
            Assert.AreEqual(ApiResourceConstants.GetAll().Count(), 1);
        }
        
        [Test]
        public void KwetterApiResource_Is_Correct()
        {
            Assert.IsTrue(ApiResourceConstants.KwetterApiResource.Scopes.Contains("kweet.list"));
            Assert.IsTrue(ApiResourceConstants.KwetterApiResource.Scopes.Contains("kweet.read"));
            Assert.IsTrue(ApiResourceConstants.KwetterApiResource.Scopes.Contains("kweet.add"));
            Assert.IsTrue(ApiResourceConstants.KwetterApiResource.Scopes.Contains("kweet.delete"));
        }
    }
}