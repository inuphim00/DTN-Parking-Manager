namespace DtnParkingSystem.Interface
{
    public interface IParkingSpaceDAO
    {
        Task AddOrUpdate<ParkingSpaces>(string floor, ParkingSpaces entity, string slot, CancellationToken ct);

        Task<IReadOnlyCollection<ParkingSpaces>> GetSubCollection<ParkingSpaces>(string id, CancellationToken ct);

        Task<IReadOnlyCollection<string>> GetDocuments<ParkingSpaces>(CancellationToken ct);
    }
}
