using DtnParkingSystem.Interface;
using DtnParkingSystem.Models;

namespace DtnParkingSystem.Services
{
	public class ManageOccupants : IManageOccupants
    {
        private readonly IOccupantsDAO _occupantsDAO;

        public ManageOccupants(IOccupantsDAO occupantsDAO)
        {
            _occupantsDAO = occupantsDAO;
        }

        public async Task<string>Register(string fullName, string contactNumber, string plateNumber, string vehicleType)
        {
            try
            {
                if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrEmpty(contactNumber) && !string.IsNullOrEmpty(vehicleType))
                {
                    var user = new Occupants
                    {
                        FullName = fullName,
                        ContactNumber = contactNumber,
                        PlateNumber = plateNumber,
                        Vehicle = vehicleType
                    };

                    await _occupantsDAO.AddOrUpdate(user, fullName, default(CancellationToken));
                    return ("Success");
                }
                else
                {
                    return ("Please fill all fields");
                }
            }
            catch(Exception ex)
            {
                return ("Error:" + ex);
            }
  
        }

        public async Task<string> EditUser(string fullName, string contactNumber, string plateNumber, string vehicleType, string originalName)
        {
            try
            {
                if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrEmpty(contactNumber) && !string.IsNullOrEmpty(vehicleType))
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
                else
                {
                    return ("Please fill all fields");
                }
            }catch(Exception ex)
            {
                return ("Error: " + ex);
            }
        }

        public string Delete(string fullName)
        {
            try
            {
                _occupantsDAO.DeleteUser(fullName);
                return ("Success");
            }
            catch(Exception ex)
            {
                return ("Error: " + ex);
            }
        }
    }
}
