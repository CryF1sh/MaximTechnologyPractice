using Microsoft.Extensions.Options;
using Moq;
using WebAPIMaximPractice;
using WebAPIMaximPractice.Controllers;
namespace TestWebAPIMaximPractice
{
    public class StringProcessingTests
    {
        [TestCase("hello", "ollehhello")]
        [TestCase("test", "etts")]
        public void TestStringProcessing(string inputStr, string expectedResult)
        {
            var result = StringProcessingController.StringProcessing(inputStr);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("hello", true)]
        [TestCase("test123", false)]
        [TestCase("TEST", false)]
        [TestCase("тест", false)]
        public void TestStringCheck(string inputStr, bool expectedResult)
        {
            var result = StringProcessingController.StringCheck(inputStr);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestGetCharCount_ValidInput_ReturnsExpectedResult()
        {
            string inputStr = "ollehhello";

            var result = StringProcessingController.GetCharCount(inputStr);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Dictionary<char, int>>(result);
            Assert.AreEqual(4, result['l']);
            Assert.AreEqual(2, result['h']);
            Assert.AreEqual(2, result['e']);
            Assert.AreEqual(2, result['o']);
        }
        [TestCase("helloolleh", "elloolle")]
        [TestCase("test", "")]
        public void TestFindLongestSubstring(string inputStr, string expectedResult)
        {
            var result = StringProcessingController.FindLongestSubstring(inputStr);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestQuickSort()
        {
            string inputStr = "ollehhello";

            var result = StringProcessingController.QuickSort(inputStr);

            Assert.AreEqual("eehhlllloo", result);
        }

        [Test]
        public void TestTreeSort()
        {
            string inputStr = "ollehhello";

            var result = StringProcessingController.TreeSort(inputStr);

            Assert.AreEqual("eehhlllloo", result);
        }
    }

}