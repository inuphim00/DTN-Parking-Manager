using Google.Cloud.Firestore;
using TestParkingSystem.Models;

namespace TestParkingSystem.Services
{
    public class OccupantsDAO
    {
        private readonly FirestoreDb _fireStoreDb = null!;

        public OccupantsDAO(FirestoreDb fireStoreDb)
        {
            _fireStoreDb = fireStoreDb;
        }

        public async void DeleteUser(string fullName)
        {
            var document = _fireStoreDb.Collection("Occupants").Document(fullName);
            await document.DeleteAsync();
        }
        public async Task AddOrUpdate<T>(T entity, string fullName, CancellationToken ct) where T : Occupants
        {
            var document = _fireStoreDb.Collection(typeof(T).Name).Document(fullName);
            await document.SetAsync(entity, cancellationToken: ct);

        }


        public async Task<IReadOnlyCollection<string>> GetDocuments<T>(CancellationToken ct) where T : Occupants
        {

            var collection = _fireStoreDb.Collection(typeof(T).Name);
            var snapshot = await collection.GetSnapshotAsync();
            var AllOccupants = new List<string> { };
            foreach (DocumentSnapshot docSnap in snapshot.Documents)
            {
                AllOccupants.Add(docSnap.Id);
            }
            return AllOccupants;
        }

        public async Task<IReadOnlyCollection<T>> GetAll<T>(CancellationToken ct) where T : Occupants
        {
            var collection = _fireStoreDb.Collection(typeof(T).Name);
            var snapshot = await collection.GetSnapshotAsync(ct);
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }

        public async Task<IReadOnlyCollection<T>> GetSubCollections<T>(CancellationToken ct) where T : Occupants
        {
            var collection = _fireStoreDb.Collection(typeof(T).Name);
            var snapshot = await collection.GetSnapshotAsync(ct);
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }
    }
}
