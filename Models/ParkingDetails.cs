namespace TestParkingSystem.Models
{
    public class ParkingDetails
    {
        public IEnumerable<ParkingSpaces> ParkingSpaces { get; set; } = Enumerable.Empty<ParkingSpaces>();
        public IEnumerable<Occupants> Occupants { get; set; } = Enumerable.Empty<Occupants>();

        public string slotnumber { get; set; } = string.Empty;
    }
}
