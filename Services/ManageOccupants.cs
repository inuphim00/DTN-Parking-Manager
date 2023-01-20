using DtnParkingSystem.Interface;
using DtnParkingSystem.Models;

namespace DtnParkingSystem.Services
{
    public class ManageOccupants : IManageOccupants
    {
        private readonly IOccupantsDAO _occupantsDAO;
        private readonly IParkingSpaceDAO _parkingSpaceDAO;
        public ManageOccupants(IOccupantsDAO occupantsDAO, IParkingSpaceDAO parkingSpaceDAO)
        {
            _occupantsDAO = occupantsDAO;
            _parkingSpaceDAO = parkingSpaceDAO;
        }

        public async Task<string> Register(string fullName, string contactNumber, string plateNumber, string vehicleType)
        {
            try
            {
                var validate = Validate(fullName, contactNumber, plateNumber, vehicleType).Result;
                if (validate == "Success")
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
                    return (validate);
                }
            }
            catch (Exception ex)
            {
                return ("Error:" + ex);
            }

        }


        public async Task<string> EditUser(string fullName, string contactNumber, string plateNumber, string vehicleType, string originalName)
        {
            try
            {
                var validate = Validate(fullName, contactNumber, plateNumber, vehicleType).Result;
                if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrEmpty(contactNumber) && !string.IsNullOrEmpty(vehicleType))
                {
                    if (validate == "Success")
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
                        return (validate);
                    }
                }
                else
                {
                    return ("Please fill all fields");
                }
            }
            catch (Exception ex)
            {
                return ("Error: " + ex);
            }

        }
        public string Delete(string fullName)
        {
            try
            {
                var validateResult = ValidateDelete(fullName).Result;
                if (validateResult == "Success")
                {
                    _occupantsDAO.DeleteUser(fullName);
                    return ("Success");
                }
                else
                {
                    return (validateResult);
                }
            }
            catch (Exception ex)
            {
                return ("Error: " + ex);
            }
        }

        public async Task<string> ValidateDelete(string fullName)
        {
            var occupantsinAllFloors = await _parkingSpaceDAO.GetDocuments<ParkingSpaces>(default);
            var listOccupants = new List<string> { };
            foreach (var items in occupantsinAllFloors)
            {
                var floorOccupants = await _parkingSpaceDAO.GetSubCollection<ParkingSpaces>(items, default);

                foreach (var occupants in floorOccupants)
                {
                    listOccupants.Add(occupants.Occupant);
                }
            }

            foreach (var items in listOccupants)
            {
                if (fullName == items)
                {
                    return ("Please free up the slot this user has occupied first");
                }
            }
            return ("Success");
        }
        public async Task<string> Validate(string fullName, string contactNumber, string plateNumber, string vehicleType)
        {
            if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrEmpty(contactNumber) && !string.IsNullOrEmpty(vehicleType))
            {

                var allOccupants = await _occupantsDAO.GetAll<Occupants>(default);
                foreach (var docs in allOccupants)
                {
                    if (docs.FullName == fullName)
                    {
                        return ("User already exist");
                    }
                    else if (docs.PlateNumber == plateNumber)
                    {
                        return ("Plate number already exist");
                    }

                }

                return ("Success");
            }
            else
            {
                return ("Please fill all fields");
            }

        }
    }
}
