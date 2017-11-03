using System.Threading.Tasks;
using Selfnet.Web.Controllers;
using Shouldly;
using Xunit;

namespace Selfnet.Web.Tests.Controllers
{
    public class HomeController_Tests: SelfnetWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
