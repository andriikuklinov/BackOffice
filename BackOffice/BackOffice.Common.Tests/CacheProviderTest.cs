using BackOffice.Common.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BackOffice.Common.Tests
{
    [TestClass]
    public class CacheProviderTest
    {
        private CacheProvider cacheProvider;

        public CacheProviderTest()
        {
            cacheProvider = new CacheProvider("mypath");
        }

        [TestMethod]
        public void CreateKey_ReturnsNotNull()
        {
            //Arrange

            //Act
            string result = cacheProvider.CreateKey("Clients");
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Clear_ReturnsTrue()
        {
            //Arrange
            bool expectedResult = true;
            //Act
            cacheProvider.CreateKey("Clients");
            bool result = cacheProvider.Clear();
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Clear_ReturnsFalse()
        {
            //Arrange
            bool expectedResult = false;
            //Act
            cacheProvider.Clear();
            bool result = cacheProvider.Clear();
            //Assert
            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void Clear_IsClearedDirectoryExists_ReturnsFalse()
        {
            //Arrange
            bool expectedResult = false;
            //Act
            cacheProvider.Clear();
            //Assert
            Assert.AreEqual(Directory.Exists(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"BackOffice")
            ), expectedResult);
        }

        [TestMethod]
        public void CreateKey_IsDirectoryExists_ReturnsTrue()
        {
            //Arrange
            bool expectedResult = true;
            //Act
            cacheProvider.CreateKey("Clients");
            //Assert
            Assert.AreEqual(Directory.Exists(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BackOffice")
            ), expectedResult);
        }

        [TestMethod]
        public void CreateKey_IsReturnsRightPath_ReturnsTrue()
        {
            //Arrange
            string path = Path.Combine(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BackOffice")
            , "Clients.cache");
            //Act
            string result = cacheProvider.CreateKey("Clients");
            //Assert
            Assert.AreEqual(path, result);
        }

        [TestMethod]
        public void Set_IsFileExists_IsTrue()
        {
            //Arrange
            string filePath = Path.Combine(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BackOffice")
            , "Clients.cache");
            //Act
            cacheProvider.Set<string>(filePath, "Test string");
            //Assert
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        public void Set_IsDataEquals_ReturnsTrue()
        {
            //Arrange
            string filePath = Path.Combine(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BackOffice")
            , "Clients.cache");
            string data = "Test string";
            //Act
            cacheProvider.Set<string>(filePath, data);
            string result = cacheProvider.Get<string>(filePath);
            //Assert
            Assert.AreEqual(data, result);
        }
    }
}
