using Google.Cloud.Firestore;
using TestParkingSystem.Interface;
using TestParkingSystem.Models;

namespace TestParkingSystem.Services
{
	public class ManageParkingSpace : IManageParkingSpaces
	{
		private readonly ParkingSpaceDAO _parkingSpaceDAO;
		private readonly OccupantsDAO _occupantsDAO;

		public ManageParkingSpace(ParkingSpaceDAO parkingSpaceDAO, ParkingDetails parkingDetails, OccupantsDAO occupantsDAO)
		{
			_parkingSpaceDAO = parkingSpaceDAO;
			_occupantsDAO = occupantsDAO;
		}

		public async Task<string> Validate(string slotNumber, string occupant, string floor, string vehicleType)
		{
			string message = "";
			var occupants = await _occupantsDAO.GetAll<Occupants>(default(CancellationToken));
			foreach (var occupantDetails in occupants)
			{
				if (occupant == occupantDetails.FullName)
				{
					if (occupantDetails.Vehicle == "Motorcyle" && vehicleType == "Bike/Motorcyle" || occupantDetails.Vehicle == "Bike" && vehicleType == "Bike/Motorcyle")
					{
						Occupy(slotNumber, occupant, floor, vehicleType);
						message = "Success";
					}
					else if (vehicleType == occupantDetails.Vehicle)
					{
						Occupy(slotNumber, occupant, floor, vehicleType);
						message = "Success";
					}
					else
					{
						message = occupantDetails.Vehicle.ToString() + " type vehicle cannot park on this parking space.";
					}

				}


			}
			return (message);


		}
		public async void Occupy(string slotNumber, string occupant, string floor, string vehicleType)
		{
			var slotnumber = slotNumber;
			var thisDateTime = DateTime.UtcNow;
			var specifiedDateTime = DateTime.SpecifyKind(thisDateTime, DateTimeKind.Utc);
			var converterDateTime = Timestamp.FromDateTime(specifiedDateTime);

			var selOcupant = "";
			if (occupant == "Available")
			{
				selOcupant = "";
			}
			else
			{
				selOcupant = occupant;
			}

			var user = new ParkingSpaces
			{
				Occupant = selOcupant,
				SlotNumber = slotnumber,
				DateTime = converterDateTime,
				Floor = floor,
				SlotType = vehicleType
			};

			await _parkingSpaceDAO.AddOrUpdate(floor, user, slotnumber, default(CancellationToken));



		}
		public async void FreeSpace(string slotNumber, string occupant, string floor, string vehicleType)
		{
			var thisDateTime = DateTime.Now;
			var specifiedDateTime = DateTime.SpecifyKind(thisDateTime, DateTimeKind.Utc);
			var converterDateTime = Timestamp.FromDateTime(specifiedDateTime);
			var slotnumber = slotNumber;

			var selOcupant = occupant;
			if (selOcupant == "Available")
			{
				selOcupant = "";
			}

			var user = new ParkingSpaces
			{
				Occupant = selOcupant,
				SlotNumber = slotnumber,
				DateTime = converterDateTime,
				Floor = floor,
				SlotType = vehicleType
			};

			await _parkingSpaceDAO.AddOrUpdate(floor, user, slotnumber, default(CancellationToken));



		}
	}
}
