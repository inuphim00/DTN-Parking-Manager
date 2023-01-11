using Google.Cloud.Firestore;
using TestParkingSystem.Models;

namespace TestParkingSystem.Services
{
    public class ParkingSpaceDAO
    {

        private readonly FirestoreDb _fireStoreDb = null!;

        public ParkingSpaceDAO(FirestoreDb fireStoreDb)
        {
            _fireStoreDb = fireStoreDb;
        }

        public async Task AddOrUpdate<T>(string floor, T entity, string slot, CancellationToken ct) where T : ParkingSpaces
        {
            var document = _fireStoreDb.Collection(typeof(T).Name).Document(floor).Collection("slots").Document(slot);
            await document.SetAsync(entity, cancellationToken: ct);

        }

        public async Task<IReadOnlyCollection<T>> GetSubCollection<T>(string id, CancellationToken ct) where T : ParkingSpaces
        {
            var collection = _fireStoreDb.Collection(typeof(T).Name).Document(id).Collection("slots");
            var snapshot = await collection.GetSnapshotAsync(ct);
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }

        public async Task<IReadOnlyCollection<string>> GetDocuments<T>(CancellationToken ct) where T : ParkingSpaces
        {
            var collection = _fireStoreDb.Collection(typeof(T).Name);

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
