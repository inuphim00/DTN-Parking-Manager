using DtnParkingSystem.Interface;
using TestParkingSystem.Models;
using TestParkingSystem.Services;

namespace DtnParkingSystem.Services
{
    public class ManageOccupants : IManageOccupants
    {
        private readonly OccupantsDAO _occupantsDAO;

        public ManageOccupants(OccupantsDAO occupantsDAO)
        {
            _occupantsDAO = occupantsDAO;
        }

        public async void Register(string fullName, string contactNumber, string plateNumber, string vehicleType)
        {


            var user = new Occupants
            {
                FullName = fullName,
                ContactNumber = contactNumber,
                PlateNumber = plateNumber,
                Vehicle = vehicleType
            };

            await _occupantsDAO.AddOrUpdate(user, fullName, default(CancellationToken));



        }

        public async Task<string> EditUser(string fullName, string contactNumber, string plateNumber, string vehicleType, string originalName)
        {
           
            var user = new Occupants
            {
                FullName = fullName,
                ContactNumber = contactNumber,
                PlateNumber = plateNumber,
                Vehicle = vehicleType
            };

            if (fullName != originalName)
            {
                _occupantsDAO.DeleteUser(originalName);
            }

            await _occupantsDAO.AddOrUpdate(user, fullName, default(CancellationToken));
            return ("Success");



        }

        public void Delete(string fullName)
        {

            _occupantsDAO.DeleteUser(fullName);
        }
    }
}
