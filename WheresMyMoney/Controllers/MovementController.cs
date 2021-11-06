using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheresMyMoney.Models;
using WheresMyMoney.ViewModels;

namespace WheresMyMoney.Controllers
{
    [Authorize(Roles = "User")]
    public class MovementController : Controller
    {
        private Repository.MovementsRepository movementRepository = new Repository.MovementsRepository();
        private Repository.UserRepository userRepository = new Repository.UserRepository();
        private Repository.CategoryRepository categoryRepository = new Repository.CategoryRepository();
        private Repository.MovementTypeRepository movementTypeRepository = new Repository.MovementTypeRepository();
        // GET: Movement
        public ActionResult Index(string sortOrder, string searchString, string startDate,  string endDate, string movementType)
        {
            if (User.Identity.IsAuthenticated)
            { 

            var movements = movementRepository.GetAllMovementsByUser(User.Identity.Name);
            var categories = categoryRepository.getAllCategories();
            var movementTypes = movementTypeRepository.GetAllMovementsTypes();

            var viewMovements = new List<ViewModels.UserMovementsNamesViewModel>();

            for (var i= 0;i < movements.Count;i++)
            {
                var viewMovement = new ViewModels.UserMovementsNamesViewModel(); 
                viewMovement.MovementId = movements[i].MovementId;
                viewMovement.Date = movements[i].Date;
                viewMovement.MovementType = movementTypes.FirstOrDefault(x => x.MovementTypeId == movements[i].MovementTypeId).Name;
                viewMovement.Category = categories.FirstOrDefault(x => x.CategoryId == movements[i].CategoryId).Name;
                viewMovement.Notes = movements[i].Notes;
                viewMovement.VALUE = movements[i].VALUE;

                viewMovements.Add(viewMovement);
            }

                            

                ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
                ViewBag.ValuesSortParam = sortOrder == "value_ascending" ? "value_desc" : "value_ascending";
                ViewBag.Expense = "Expense";
                ViewBag.Income = "Income";

                switch (movementType)
                {
                    case "Income":
                        var tempViewMovementsIncome = from s in viewMovements select s;
                        tempViewMovementsIncome = viewMovements.Where(x => x.MovementType == "Income");
                        viewMovements = tempViewMovementsIncome.ToList();
                        break;
                    case "Expense":
                        var tempViewMovementsExpense = from s in viewMovements select s;
                        tempViewMovementsExpense = viewMovements.Where(x => x.MovementType == "Expense");
                        viewMovements = tempViewMovementsExpense.ToList();
                        break;
                }
                
                

                if (!string.IsNullOrEmpty(searchString))
                {
                    var viewMovementsNotes = from s in viewMovements select s;
                    viewMovementsNotes = viewMovements.Where(z => z.Notes != null);
                    viewMovementsNotes = viewMovementsNotes.Where(x => x.Notes.ToLower().Contains(searchString.ToLower()));


                    var viewMovementsCategory = from c in viewMovements select c;
                    viewMovementsCategory = viewMovements.Where(x => x.Category.ToLower().Contains(searchString.ToLower()));
                   

                    viewMovements = viewMovementsNotes.ToList();
                    foreach(var mov in viewMovementsCategory)
                    { viewMovements.Add(mov); };    
                                    

                }

               if (!string.IsNullOrEmpty(endDate)&& !string.IsNullOrEmpty(startDate))
                {
                    var tempViewMovements = from s in viewMovements select s;
                    tempViewMovements = viewMovements.Where(x => x.Date >= DateTime.Parse(startDate)&& x.Date<=DateTime.Parse(endDate));
                    viewMovements = tempViewMovements.ToList();
                }



                switch (sortOrder)
                {
                    case "date_desc":
                        viewMovements = viewMovements.OrderByDescending(x => x.Date).ToList();
                        break;
                    case "value_ascending":
                        viewMovements = viewMovements.OrderBy(x => x.VALUE).ToList();
                        break;
                    case "value_desc":
                        viewMovements = viewMovements.OrderByDescending(x => x.VALUE).ToList();
                        break;
                    default:
                        viewMovements = viewMovements.OrderBy(x => x.Date).ToList();
                        break;
                }

                return View("Index", viewMovements);
            }
            List<UserMovementsNamesViewModel> noMovements = new List<UserMovementsNamesViewModel>();
            return View("Index", noMovements);

           
        }

        // GET: Movement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movement/Create

        public ActionResult Create()
        {
            ViewModels.CreateMovementViewModel movementModel = new ViewModels.CreateMovementViewModel()
            {
                Movement = new Models.MovementModel(),
                Category = categoryRepository.getAllCategories(),
                MovementType = movementTypeRepository.GetAllMovementsTypes()
            };
            
            
            Models.UserModel user = userRepository.GetUserByEmail(User.Identity.Name);
            movementModel.Movement.UserId = user.UserId;

            return View("CreateMovement", movementModel);
        }
        
        //public ActionResult Create()
        //{
                        
        //   Models.MovementModel movementModel = new Models.MovementModel();
        //   Models.UserModel user = userRepository.GetUserByEmail(User.Identity.Name);
        //   movementModel.UserId = user.UserId;

        //    return View("CreateMovement",movementModel);           
            
        //}

        // POST: Movement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //Models.MovementModel movementModel = new Models.MovementModel();

                ViewModels.CreateMovementViewModel movementModel = new ViewModels.CreateMovementViewModel()
                {
                    Movement = new Models.MovementModel(),
                    Category = categoryRepository.getAllCategories(),
                    MovementType = movementTypeRepository.GetAllMovementsTypes()
                };

                UpdateModel(movementModel);
                
                movementRepository.CreateMovement(movementModel.Movement);

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

            var allCategories = categoryRepository.getAllCategories();

            ViewModels.CreateMovementViewModel movementModel = new ViewModels.CreateMovementViewModel()
            {
                Movement = movementRepository.GetMovementById(id),
                Category = new List<CategoryModel>(),
                MovementType = movementTypeRepository.GetAllMovementsTypes()
            };

            movementModel.Category = allCategories.Where(x => x.MovementTypeId == movementModel.Movement.MovementTypeId);        

            return View("EditMovement", movementModel);
        }




        //Models.MovementModel movementModel = movementRepository.GetMovementById(id);
        //return View("EditMovement",movementModel); 
    

        // POST: Movement/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                ViewModels.CreateMovementViewModel movementModel = new ViewModels.CreateMovementViewModel()
                {
                    Movement = new Models.MovementModel(),
                    Category = categoryRepository.getAllCategories(),
                    MovementType = movementTypeRepository.GetAllMovementsTypes()
                };

                UpdateModel(movementModel);
                
               
                movementRepository.UpdateMovement(movementModel.Movement);


                //var movementModel = new Models.MovementModel();
                //UpdateModel(movementModel);
                //movementRepository.UpdateMovement(movementModel);

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
