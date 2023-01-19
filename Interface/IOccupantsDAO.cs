using Google.Cloud.Firestore;

namespace DtnParkingSystem.Interface
{
	public interface IOccupantsDAO
	{
		void DeleteUser(string fullName);

		Task AddOrUpdate<Occupants>(Occupants entity, string fullName, CancellationToken ct);

		Task<IReadOnlyCollection<string>> GetDocuments<Occupants>(CancellationToken ct);

		Task<IReadOnlyCollection<Occupants>> GetAll<Occupants>(CancellationToken ct);

		Task<IReadOnlyCollection<Occupants>> GetSubCollections<Occupants>(CancellationToken ct);
	}
}
