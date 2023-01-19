using DtnParkingSystem.Models;

namespace DtnParkingSystem.Interface
{
	public interface IGetParkingDetails
	{
		public Task<IReadOnlyCollection<ParkingSpaces>> GetAllParkingSpaces();
		public Task<IReadOnlyCollection<string>> GetAllFloorSpaces();
		public Task<IReadOnlyCollection<Occupants>> GetOccupant();
		public Task<IReadOnlyCollection<string>> GetCarFilteredList();
		public Task<IReadOnlyCollection<string>> GetMotorOrBikeFilteredList();

	}
}
