using DtnParkingSystem.Interface;
using DtnParkingSystem.Models;
using DtnParkingSystem.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtnParkingSystem.UnitTests
{
    [TestFixture]
    public class ManageOccupantsTests
    {
        private ManageOccupants manageOccupants;
        private Mock<IOccupantsDAO> occupantsMock;
        [SetUp]
        public void Setup()
        { 
            occupantsMock = new Mock<IOccupantsDAO>();
            manageOccupants = new ManageOccupants(occupantsMock.Object);

        }
     
        [Test]
        public void Register_UserSuccessFullyRegistered_ReturnsSuccess()
        {
          
            var result = manageOccupants.Register("FullName", "09000000000", "AB1234", "Car").Result;
            Assert.That(result, Is.EqualTo("Success"));
          
        }
        [Test]
        public void Register_UserDidNotFillAllFields_ReturnsFail()
        {


            var result = manageOccupants.Register("FullName", "", "AB1234", "Car").Result;
            Assert.That(result, Is.EqualTo("Please fill all fields"));

        }

        [Test]
        public void Edit_UserEditedSuccessfully_ReturnsSuccess()
        {

       
            var result = manageOccupants.EditUser("FullName", "09000000000", "AB1234", "Car", "OriginalName").Result;
            Assert.That(result, Is.EqualTo("Success"));

        }

        [Test]
        public void Edit_UserDidNotFillAllFields_ReturnsFail()
        {

            var result = manageOccupants.EditUser("FullName", "", "AB1234", "Car", "OriginalName").Result;
            Assert.That(result, Is.EqualTo("Please fill all fields"));

        }

        [Test]
        public void Delete_UserDeletedSuccessfully_ReturnsSuccess()
        {

            var result = manageOccupants.Delete("FullName");
            Assert.That(result, Is.EqualTo("Success"));

        }



    }
}
