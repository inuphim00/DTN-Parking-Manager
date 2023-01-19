using DtnParkingSystem.Controllers;
using DtnParkingSystem.Interface;
using Google.Cloud.Firestore;
using DtnParkingSystem.Models;
using Microsoft.CodeAnalysis;


namespace DtnParkingSystem.Services
{
	public class OccupantsDAO : IOccupantsDAO
	{
		private FirestoreDb _fireStoreDb = FirestoreDb.Create("dtn-parking-manager");

        public async void DeleteUser(string fullName)
		{
			var document = _fireStoreDb.Collection("Occupants").Document(fullName);
			await document.DeleteAsync();
		}
		public async Task AddOrUpdate<Occupants>(Occupants entity, string fullName, CancellationToken ct)
		{
			var document = _fireStoreDb.Collection(typeof(Occupants).Name).Document(fullName);
			await document.SetAsync(entity, cancellationToken: ct);

		}

		public async Task<IReadOnlyCollection<string>> GetDocuments<Occupants>(CancellationToken ct)
		{

			var collection = _fireStoreDb.Collection(typeof(Occupants).Name);
			var snapshot = await collection.GetSnapshotAsync();
			var AllOccupants = new List<string> { };
			foreach (DocumentSnapshot docSnap in snapshot.Documents)
			{
				AllOccupants.Add(docSnap.Id);
			}
			return AllOccupants;
		}

		public async Task<IReadOnlyCollection<Occupants>> GetAll<Occupants>(CancellationToken ct)
		{
			var collection = _fireStoreDb.Collection(typeof(Occupants).Name);
			var snapshot = await collection.GetSnapshotAsync(ct);
			return snapshot.Documents.Select(x => x.ConvertTo<Occupants>()).ToList();
		}

		public async Task<IReadOnlyCollection<Occupants>> GetSubCollections<Occupants>(CancellationToken ct)
		{
			var collection = _fireStoreDb.Collection(typeof(Occupants).Name);
			var snapshot = await collection.GetSnapshotAsync(ct);
			return snapshot.Documents.Select(x => x.ConvertTo<Occupants>()).ToList();
		}
	}
}
