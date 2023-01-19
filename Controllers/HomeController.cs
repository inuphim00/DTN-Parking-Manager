using DtnParkingSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DtnParkingSystem.Models;


namespace DtnParkingSystem.Controllers
{
	public class HomeController : Controller
	{
		private IParkingDetails _parkingDetails;
		private readonly IGetParkingDetails _getParkingDetails;
		private readonly IManageParkingSpaces _manageParkingSpace;

		public HomeController(IGetParkingDetails getParkingDetails, IManageParkingSpaces manageParkingSpace, IParkingDetails parkingDetails)
		{
			_getParkingDetails = getParkingDetails;
			_manageParkingSpace = manageParkingSpace;
			_parkingDetails = parkingDetails;
		}

		public async Task<IActionResult> Index()
		{
			var floorSpaces = _getParkingDetails.GetAllParkingSpaces();
			var occupantDetails = await _getParkingDetails.GetOccupant();
			_parkingDetails.ParkingSpaces = floorSpaces.Result;
			_parkingDetails.Occupants = occupantDetails;
			ViewBag.CarList = _getParkingDetails.GetCarFilteredList().Result;
			ViewBag.MotorBikeList = _getParkingDetails.GetMotorOrBikeFilteredList().Result;
			ViewBag.AllFloors = _getParkingDetails.GetAllFloorSpaces().Result;
			return View(_parkingDetails);
		}

		[ActionName("FreeUp")]
		[Route("FreeUp")]
		public IActionResult FreeUp([FromForm] IFormCollection formCollection)
		{
			
			TempData["Message"] = _manageParkingSpace.FreeSpace(formCollection["slotnumber"], formCollection["Occupant"], formCollection["floor"], formCollection["vehicleType"]).Result;
            return RedirectToAction("Index");
		}

		[ActionName("Submit")]
		[Route("Submit")]
		public IActionResult Submit([FromForm] IFormCollection formCollection)
		{
			
			TempData["Message"] = _manageParkingSpace.Occupy(formCollection["slotnumber"], formCollection["Occupant"], formCollection["floor"], formCollection["vehicleType"]).Result;
            return RedirectToAction("Index");

		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}