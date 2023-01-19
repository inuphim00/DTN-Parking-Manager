namespace DtnParkingSystem.Interface
{
    public interface IManageOccupants
    {
        public Task<string> Validate(string fullName, string contactNumber, string plateNumber, string vehicleType);
        public Task<string>Register(string fullName, string contactNumber, string plateNumber, string vehicleType);
        public Task<string> EditUser(string fullName, string contactNumber, string plateNumber, string vehicleType, string originalName);
        public string Delete(string fullName);
    }
}
