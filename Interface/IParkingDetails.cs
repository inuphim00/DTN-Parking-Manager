using TestParkingSystem.Models;

namespace DtnParkingSystem.Interface
{
    public interface IParkingDetails
    {
        public IEnumerable<ParkingSpaces> ParkingSpaces { get; set; }
        public IEnumerable<Occupants> Occupants { get; set; }
        public string slotnumber { get; set; }
    }
}
