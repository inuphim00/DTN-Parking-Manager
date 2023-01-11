using Google.Cloud.Firestore;

namespace TestParkingSystem.Models
{
	[FirestoreData]
	public class Occupants
	{

		[FirestoreProperty]
		public string ContactNumber { get; set; } = string.Empty;
		[FirestoreProperty]
		public string PlateNumber { get; set; } = string.Empty;
		[FirestoreProperty]

		public string FullName { get; set; } = string.Empty;

		[FirestoreProperty]
		public string Vehicle { get; set; } = string.Empty;


	}
}
