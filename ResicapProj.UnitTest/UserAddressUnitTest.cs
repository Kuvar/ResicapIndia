using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResicapIndia.Models;
using ResicapIndia.Services;
using ResicapIndia.ViewModels;

namespace ResicapProj.UnitTest
{
    [TestClass]
    public class UserAddressUnitTest
    {
        [TestMethod]
        public void AddAddressUnitTestAsync()
        {
            bool result = false;

            //Arrange
            var mockData= new MockDataStore();
            var address = new Item{ Id = Guid.NewGuid().ToString(), Address = "#123, ABC Floor, Alexa Line, GA)" };


            //Act
            Task.Run(async () =>
                {
                   result = await mockData.AddItemAsync(address);
                });


            //Assert
            Assert.IsTrue(result, "Address added successfully");

        }
    }
}
