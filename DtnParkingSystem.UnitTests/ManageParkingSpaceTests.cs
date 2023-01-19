using DtnParkingSystem.Interface;
using DtnParkingSystem.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtnParkingSystem.UnitTests
{
    public class ManageParkingSpaceTests
    {
        private ManageParkingSpace manageParkingSpace;
        private Mock<IParkingSpaceDAO> parkingSpaceMock;
        private Mock<IOccupantsDAO> occupantsMock;
        [SetUp]
        public void Setup()
        {
            parkingSpaceMock = new Mock<IParkingSpaceDAO>();
            occupantsMock= new Mock<IOccupantsDAO>();
            manageParkingSpace = new ManageParkingSpace(parkingSpaceMock.Object, occupantsMock.Object);

        }
        [Test]
        public void Occupy_UserHasOccupied_ReturnsSuccess()
        {
            var result = manageParkingSpace.Occupy("616", "FullName", "6th Floor", "Car").Result;
            Assert.That(result, Is.EqualTo("Success"));
        }
        [Test]
        public void FreeUp_UserFreeUpParkingSpace_ReturnsSuccess()
        {
            var result = manageParkingSpace.FreeSpace("616", "FullName", "6th Floor", "Car").Result;
            Assert.That(result, Is.EqualTo("Success"));
        }
    }
}
