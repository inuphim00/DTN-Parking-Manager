namespace DtnParkingSystem.Interface
{
    public interface IManageOccupants
    {
        public void Register(string fullName, string contactNumber, string plateNumber, string vehicleType);
        public Task<string> EditUser(string fullName, string contactNumber, string plateNumber, string vehicleType, string originalName);
        public void Delete(string fullName);
    }
}
