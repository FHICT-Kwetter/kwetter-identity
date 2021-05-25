using System.Linq;
using IdentityServer.Domain.OAuth;
using IdentityServer4.Models;
using NUnit.Framework;

namespace IdentityServer.Test.OAuth
{
    public class ValidateOAuthApiScopesTest
    {
        [Test]
        public void Correct_Amount_Of_Resources_Exists()
        {
            Assert.AreEqual(ApiScopeConstants.GetAll().Count(), 4);
        }
        
        [Test]
        public void KwetterApiResource_Is_Correct()
        {
            Assert.AreEqual(ApiScopeConstants.KweetList.Name, new ApiScope("kweet.list").Name);
            Assert.AreEqual(ApiScopeConstants.KweetRead.Name, new ApiScope("kweet.read").Name);
            Assert.AreEqual(ApiScopeConstants.KweetAdd.Name, new ApiScope("kweet.add").Name);
            Assert.AreEqual(ApiScopeConstants.KweetDelete.Name, new ApiScope("kweet.delete").Name);
        }
    }
}   