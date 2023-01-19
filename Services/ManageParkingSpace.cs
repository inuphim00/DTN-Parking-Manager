using DtnParkingSystem.Interface;
using DtnParkingSystem.Models;
using Google.Cloud.Firestore;


namespace DtnParkingSystem.Services
{
	public class ManageParkingSpace : IManageParkingSpaces
	{
		private readonly IParkingSpaceDAO _parkingSpaceDAO;
		private readonly IOccupantsDAO _occupantsDAO;

		public ManageParkingSpace(IParkingSpaceDAO parkingSpaceDAO, IOccupantsDAO occupantsDAO)
		{
			_parkingSpaceDAO = parkingSpaceDAO;
			_occupantsDAO = occupantsDAO;
		}

		public async Task<string>Occupy(string slotNumber, string occupant, string floor, string vehicleType)
		{
			try
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

					await _parkingSpaceDAO.AddOrUpdate(floor, user, slotnumber, default);
					return ("Success");
				
				
			}
			catch(Exception ex)
			{
				return ("Error:" + ex);
			}


		}
		public async Task<string>FreeSpace(string slotNumber, string occupant, string floor, string vehicleType)
		{
			try
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

				await _parkingSpaceDAO.AddOrUpdate(floor, user, slotnumber, default);
				return ("Success");
			}catch(Exception ex)
			{
				return ("Error:" +ex);
			}



		}
	}
}
