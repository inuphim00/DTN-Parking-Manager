namespace DtnParkingSystem.Interface
{
	public interface IManageParkingSpaces
	{
		public Task<string> Occupy(string slotNumber, string occupant, string floor, string vehicleType);
		public Task<string> FreeSpace(string slotNumber, string occupant, string floor, string vehicleType);

	}
}
