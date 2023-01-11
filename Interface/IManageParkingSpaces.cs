namespace TestParkingSystem.Interface
{
    public interface IManageParkingSpaces
    {
        public Task<string> Validate(string slotNumber, string occupant, string floor, string vehicleType);
        public void Occupy(string slotNumber, string occupant, string floor, string vehicleType);
        public void FreeSpace(string slotNumber, string occupant, string floor, string vehicleType);

    }
}
