using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheresMyMoney.ViewModels;

namespace WheresMyMoney.Controllers
{
    public class MovementController : Controller
    {
        private Repository.MovementsRepository movementRepository = new Repository.MovementsRepository();
        private Repository.UserRepository userRepository = new Repository.UserRepository();
        // GET: Movement
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<UserMovementsNamesViewModel> movements = movementRepository.GetAllMovementsCustomViewByUser(User.Identity.Name);
                return View("Index", movements);
            }
            List<UserMovementsNamesViewModel> noMovements = new List<UserMovementsNamesViewModel>();
            return View("Index", noMovements);

            //List<UserMovementsNamesViewModel> movements = movementRepository.GetAllMovementsCustomView();
            //List<Models.MovementModel> movements = movementRepository.GetAllMovements();
            //return View("Index",movements);
        }

        // GET: Movement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movement/Create
        public ActionResult Create()
        {
            
           Models.MovementModel movementModel = new Models.MovementModel();
           Models.UserModel user = userRepository.GetUserByEmail(User.Identity.Name);
           movementModel.UserId = user.UserId;

            return View("CreateMovement",movementModel);

           
            
        }

        // POST: Movement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.MovementModel movementModel = new Models.MovementModel();
                UpdateModel(movementModel);
                
                movementRepository.CreateMovement(movementModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateMovement");
            }
        }

        // GET: Movement/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.MovementModel movementModel = movementRepository.GetMovementById(id);
            return View("EditMovement",movementModel);
        }

        // POST: Movement/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                var movementModel = new Models.MovementModel();
                UpdateModel(movementModel);
                movementRepository.UpdateMovement(movementModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditMovement");
            }
        }

        // GET: Movement/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.MovementModel movementModel = movementRepository.GetMovementById(id);
            return View("DeleteMovement", movementModel);
            
        }

        // POST: Movement/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                movementRepository.DeleteMovement(id);


                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteMovement");
            }
        }
    }
}
