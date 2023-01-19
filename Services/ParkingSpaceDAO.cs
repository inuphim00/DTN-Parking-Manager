using DtnParkingSystem.Interface;
using DtnParkingSystem.Models;
using Google.Cloud.Firestore;

namespace DtnParkingSystem.Services
{
	public class ParkingSpaceDAO : IParkingSpaceDAO
	{

        private FirestoreDb _fireStoreDb = FirestoreDb.Create("dtn-parking-manager");

        public async Task AddOrUpdate<ParkingSpaces>(string floor, ParkingSpaces entity, string slot, CancellationToken ct)
		{
			var document = _fireStoreDb.Collection(typeof(ParkingSpaces).Name).Document(floor).Collection("slots").Document(slot);
			await document.SetAsync(entity, cancellationToken: ct);

		}

		public async Task<IReadOnlyCollection<ParkingSpaces>> GetSubCollection<ParkingSpaces>(string id, CancellationToken ct)
		{
			var collection = _fireStoreDb.Collection(typeof(ParkingSpaces).Name).Document(id).Collection("slots");
			var snapshot = await collection.GetSnapshotAsync(ct);
			return snapshot.Documents.Select(x => x.ConvertTo<ParkingSpaces>()).ToList();
		}

		public async Task<IReadOnlyCollection<string>> GetDocuments<ParkingSpaces>(CancellationToken ct) { 
			var collection = _fireStoreDb.Collection(typeof(ParkingSpaces).Name);

			var snapshot = await collection.GetSnapshotAsync();
			var AllFloors = new List<string> { };
			foreach (DocumentSnapshot docSnap in snapshot.Documents)
			{
				AllFloors.Add(docSnap.Id);
			}
			return AllFloors;
		}
	}
}
