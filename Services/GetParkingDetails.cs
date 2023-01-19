using DtnParkingSystem.Models;
using DtnParkingSystem.Interface;
using DtnParkingSystem.Services;


namespace DtnParkingSystem.Services
{
	public class GetParkingDetails : IGetParkingDetails
	{
		private readonly IParkingSpaceDAO _parkingSpaceDAO;
		private readonly IOccupantsDAO _occupantsDAO;


		public GetParkingDetails(IParkingSpaceDAO parkingSpaceDAO, IOccupantsDAO occupantsDAO)
		{
			_parkingSpaceDAO = parkingSpaceDAO;
			_occupantsDAO = occupantsDAO;

		}

		public async Task<IReadOnlyCollection<ParkingSpaces>> GetAllParkingSpaces()
		{
			var floorSpaces = await _parkingSpaceDAO.GetDocuments<ParkingSpaces>(default);
			var thisFloor = new List<ParkingSpaces>();
			foreach (var floors in floorSpaces)
			{
				var allfloors = await _parkingSpaceDAO.GetSubCollection<ParkingSpaces>(floors, default);
				thisFloor.AddRange(allfloors);
			}
			return thisFloor;
		}

		public async Task<IReadOnlyCollection<string>> GetAllFloorSpaces()
		{
			var floorSpaces = await _parkingSpaceDAO.GetDocuments<ParkingSpaces>(default);
			return floorSpaces;

		}

		public async Task<IReadOnlyCollection<Occupants>> GetOccupant()
		{
			var occupantDetails = await _occupantsDAO.GetAll<Occupants>(default);
			return occupantDetails;

		}

		public async Task<IReadOnlyCollection<string>> GetMotorOrBikeFilteredList()
		{
			var occupantsinAllFloors = await _parkingSpaceDAO.GetDocuments<ParkingSpaces>(default);
			var allOccupants = await _occupantsDAO.GetDocuments<Occupants>(default);
			var listOccupants = new List<string> { };
			foreach (var items in occupantsinAllFloors)
			{
				var floorOccupants = await _parkingSpaceDAO.GetSubCollection<ParkingSpaces>(items, default);
				foreach (var occupant in floorOccupants)
				{
					listOccupants.Add(occupant.Occupant);
				}

			}
			var occupantDetails = await _occupantsDAO.GetAll<Occupants>(default);
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
			var occupantsinAllFloors = await _parkingSpaceDAO.GetDocuments<ParkingSpaces>(default);
			var allOccupants = await _occupantsDAO.GetDocuments<Occupants>(default);
			var listOccupants = new List<string> { };
			foreach (var items in occupantsinAllFloors)
			{
				var floorOccupants = await _parkingSpaceDAO.GetSubCollection<ParkingSpaces>(items, default);
				foreach (var occupant in floorOccupants)
				{

					listOccupants.Add(occupant.Occupant);

				}

			}
			var occupantDetails = await _occupantsDAO.GetAll<Occupants>(default);
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

	}
}
