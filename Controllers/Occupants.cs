using DtnParkingSystem.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DtnParkingSystem.Controllers
{
	public class Occupants : Controller
    {
        private readonly IManageOccupants _manageOccupants;
        private readonly IGetParkingDetails _getParkingDetails;

        public Occupants(IManageOccupants manageOccupants, IGetParkingDetails getParkingDetails)
        {
            _manageOccupants = manageOccupants;
            _getParkingDetails = getParkingDetails;
        }
        public async Task<IActionResult> Index()
        {
            var occupants = await _getParkingDetails.GetOccupant();
            return View(occupants);
        }

        [ActionName("Register")]
        [Route("Register")]
        public IActionResult Register([FromForm] IFormCollection formCollection)
        {
            
            TempData["Message"] = _manageOccupants.Register(formCollection["fullname"], formCollection["contactnumber"], formCollection["platenumber"], formCollection["vehicleType"]).Result;
            return RedirectToAction("Index");
        }
        [ActionName("EditUser")]
        [Route("EditUser")]
        public IActionResult EditUser([FromForm] IFormCollection formCollection)
        {
            TempData["Message"] = _manageOccupants.EditUser(formCollection["fullnameEdit"], formCollection["contactnumberEdit"], formCollection["platenumberEdit"], formCollection["vehicleTypeEdit"], formCollection["fullNameOriginal"]).Result;
            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        [Route("Delete")]
        public IActionResult Delete([FromForm] IFormCollection formCollection)
        {
            _manageOccupants.Delete(formCollection["userToDelete"]);
            return RedirectToAction("Index");
        }
    }
}
