using TestParkingSystem.Interface;
using TestParkingSystem.Models;

namespace TestParkingSystem.Services
{
    public class GetParkingDetails : IGetParkingDetails
    {
        private readonly ParkingSpaceDAO _parkingSpaceDAO;
        private readonly OccupantsDAO _occupantsDAO;


        public GetParkingDetails(ParkingSpaceDAO parkingSpaceDAO, OccupantsDAO occupantsDAO)
        {
            _parkingSpaceDAO = parkingSpaceDAO;
            _occupantsDAO = occupantsDAO;

        }

        public async Task<IReadOnlyCollection<ParkingSpaces>> GetFloorSpaces(string floor)
        {
            var floorSpaces = await _parkingSpaceDAO.GetSubCollection<ParkingSpaces>(floor, default(CancellationToken));

            return floorSpaces;


        }
        public async Task<IReadOnlyCollection<ParkingSpaces>> GetAllParkingSpaces()
        {
            var floorSpaces = await _parkingSpaceDAO.GetDocuments<ParkingSpaces>(default(CancellationToken)); //6th floor basement 7th floor

            var thisFloor = new List<ParkingSpaces>(); // Created new list

            foreach (var floors in floorSpaces)
            {

                var allfloors = await _parkingSpaceDAO.GetSubCollection<ParkingSpaces>(floors, default(CancellationToken));
                thisFloor.AddRange(allfloors);
            }

            return thisFloor;
        }

        public async Task<IReadOnlyCollection<string>> GetAllFloorSpaces()
        {
            var floorSpaces = await _parkingSpaceDAO.GetDocuments<ParkingSpaces>(default(CancellationToken));

            return floorSpaces;


        }

        public async Task<IReadOnlyCollection<Occupants>> GetOccupant()
        {
            var occupantDetails = await _occupantsDAO.GetAll<Occupants>(default(CancellationToken));
            return occupantDetails;

        }

        public async Task<IReadOnlyCollection<string>> GetMotorOrBikeFilteredList()
        {
            var occupantsinAllFloors = await _parkingSpaceDAO.GetDocuments<ParkingSpaces>(default(CancellationToken));
            var allOccupants = await _occupantsDAO.GetDocuments<Occupants>(default(CancellationToken));

            var listOccupants = new List<string> { };


            foreach (var items in occupantsinAllFloors)
            {
                var floorOccupants = await _parkingSpaceDAO.GetSubCollection<ParkingSpaces>(items, default(CancellationToken));
                foreach (var occupant in floorOccupants)
                {

                    listOccupants.Add(occupant.Occupant);

                }

            }
            var occupantDetails = await _occupantsDAO.GetAll<Occupants>(default(CancellationToken));



            var selectList = new List<string> { };


            foreach (var docs in allOccupants)
            {

                selectList.Add(docs);

                foreach (var item in listOccupants)
                {

                    if (item == docs)
                    {
                        selectList.Remove(docs);
                    }
                    foreach (var carOccupants in occupantDetails)
                    {
                        if (carOccupants.Vehicle != "Motorcycle" && carOccupants.Vehicle != "Bicycle")
                        {

                            selectList.Remove(carOccupants.FullName);
                        }
                    }
                }
            }



            return selectList;
        }

        public async Task<IReadOnlyCollection<string>> GetCarFilteredList()
        {
            var occupantsinAllFloors = await _parkingSpaceDAO.GetDocuments<ParkingSpaces>(default(CancellationToken));
            var allOccupants = await _occupantsDAO.GetDocuments<Occupants>(default(CancellationToken));

            var listOccupants = new List<string> { };
            foreach (var items in occupantsinAllFloors)
            {
                var floorOccupants = await _parkingSpaceDAO.GetSubCollection<ParkingSpaces>(items, default(CancellationToken));
                foreach (var occupant in floorOccupants)
                {

                    listOccupants.Add(occupant.Occupant);

                }

            }
            var occupantDetails = await _occupantsDAO.GetAll<Occupants>(default(CancellationToken));

            var selectList = new List<string> { };

            foreach (var docs in allOccupants)
            {

                selectList.Add(docs);

                foreach (var item in listOccupants)
                {

                    if (item == docs)
                    {
                        selectList.Remove(docs);
                    }
                    foreach (var carOccupants in occupantDetails)
                    {
                        if (carOccupants.Vehicle != "Car")
                        {
                            selectList.Remove(carOccupants.FullName);
                        }
                    }
                }
            }



            return selectList;
        }


        public async Task<IReadOnlyCollection<string>> GetFilteredList()
        {
            var occupantsinAllFloors = await _parkingSpaceDAO.GetDocuments<ParkingSpaces>(default(CancellationToken));
            var allOccupants = await _occupantsDAO.GetDocuments<Occupants>(default(CancellationToken));

            var listOccupants = new List<string> { };
            foreach (var items in occupantsinAllFloors)
            {
                var floorOccupants = await _parkingSpaceDAO.GetSubCollection<ParkingSpaces>(items, default(CancellationToken));
                foreach (var occupant in floorOccupants)
                {
                    listOccupants.Add(occupant.Occupant);
                }

            }

            var selectList = new List<string> { };

            foreach (var docs in allOccupants)
            {

                selectList.Add(docs);

                foreach (var item in listOccupants)
                {
                    if (item == docs)
                    {
                        selectList.Remove(docs);
                    }
                }
            }



            return selectList;
        }

        public async Task<IReadOnlyCollection<string>> GetListAvailable()
        {
            var occupantsinAllFloors = await _parkingSpaceDAO.GetDocuments<ParkingSpaces>(default(CancellationToken));
            var allOccupants = await _occupantsDAO.GetDocuments<Occupants>(default(CancellationToken));

            var listOccupants = new List<string> { };
            foreach (var items in occupantsinAllFloors)
            {
                var floorOccupants = await _parkingSpaceDAO.GetSubCollection<ParkingSpaces>(items, default(CancellationToken));
                foreach (var occupant in floorOccupants)
                {
                    listOccupants.Add(occupant.Occupant);
                }

            }

            var selectList = new List<string> { };

            foreach (var docs in allOccupants)
            {

                selectList.Add(docs);

                foreach (var item in listOccupants)
                {
                    if (item == docs)
                    {
                        selectList.Remove(docs);
                    }
                }
            }

            selectList.Add("Available");

            return selectList;
        }

    }
}
