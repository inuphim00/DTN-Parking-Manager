using Google.Cloud.Firestore;

namespace TestParkingSystem.Models
{
	[FirestoreData]
	public class ParkingSpaces
	{



		[FirestoreProperty]
		public string Occupant { get; set; } = string.Empty;

		[FirestoreProperty]
		public string SlotNumber { get; set; } = string.Empty;

		[FirestoreProperty]
		public Timestamp DateTime { get; set; }

		[FirestoreProperty]
		public string SlotType { get; set; } = string.Empty;


		[FirestoreProperty]
		public string Floor { get; set; } = string.Empty;
		public ParkingSpaces()
		{

		}
		public ParkingSpaces(string occupant)
		{

			Occupant = occupant;

		}
	}

}
