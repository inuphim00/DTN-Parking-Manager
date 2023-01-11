using TestParkingSystem.Models;

namespace TestParkingSystem.Interface
{
	public interface IGetParkingDetails
	{
		public Task<IReadOnlyCollection<ParkingSpaces>> GetFloorSpaces(string floor);
		public Task<IReadOnlyCollection<ParkingSpaces>> GetAllParkingSpaces();
		public Task<IReadOnlyCollection<string>> GetAllFloorSpaces();
		public Task<IReadOnlyCollection<Occupants>> GetOccupant();
		public Task<IReadOnlyCollection<string>> GetFilteredList();
		public Task<IReadOnlyCollection<string>> GetListAvailable();
		public Task<IReadOnlyCollection<string>> GetCarFilteredList();

		public Task<IReadOnlyCollection<string>> GetMotorOrBikeFilteredList();

	}
}
