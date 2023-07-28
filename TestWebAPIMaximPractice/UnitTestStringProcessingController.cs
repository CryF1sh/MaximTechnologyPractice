using Moq;
using WebAPIMaximPractice.Controllers;
namespace TestWebAPIMaximPractice
{
    public class StringProcessingControllerTests
    {
        private StringProcessingController _controller;
        [SetUp]
        public void Setup()
        {
            var appSettingsMock = new Mock<WebAPIMaximPractice.AppSettings>();
            appSettingsMock.Setup(a => a.BlackList).Returns(new string[] { "abc", "test" }); 

            _controller = new StringProcessingController(appSettingsMock);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}