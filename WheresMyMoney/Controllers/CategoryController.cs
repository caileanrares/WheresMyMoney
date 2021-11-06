using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WheresMyMoney.Controllers
    
{
    [Authorize(Roles = "User")]
    public class CategoryController : Controller
    {
        private Repository.CategoryRepository categoryRepository = new Repository.CategoryRepository();
        private Repository.MovementTypeRepository movementTypeRepository = new Repository.MovementTypeRepository();

        // GET: Category
        public ActionResult Index()
        {
            List<Models.CategoryModel> categories = categoryRepository.getAllCategories();
            List<Models.MovementTypeModel> Types = movementTypeRepository.GetAllMovementsTypes();
            List<ViewModels.CategoryViewModel> catergoriesViewModel = new List<ViewModels.CategoryViewModel>();


            foreach (var category in categories)
            {
                var categoryViewModel = new ViewModels.CategoryViewModel();
                categoryViewModel.CategoryId = category.CategoryId;
                categoryViewModel.Name = category.Name;
                categoryViewModel.MovementTypeName = Types.FirstOrDefault(x => x.MovementTypeId == category.MovementTypeId).Name;

                catergoriesViewModel.Add(categoryViewModel);
            }

            return View("Index", catergoriesViewModel);
        }


        public ActionResult Create()
        {
            ViewModels.CreateCategoryViewModel categoryViewModel = new ViewModels.CreateCategoryViewModel()
            {
                category = new Models.CategoryModel(),
                type = movementTypeRepository.GetAllMovementsTypes()
            };

            categoryViewModel.category.CategoryId = Guid.NewGuid();

           
            return View("CreateCategory", categoryViewModel);
        }

        [HttpPost]
        public ActionResult Create (FormCollection collection)
        {
            try
            {
                ViewModels.CreateCategoryViewModel categoryViewModel = new ViewModels.CreateCategoryViewModel()
                {
                    category = new Models.CategoryModel(),
                    type = movementTypeRepository.GetAllMovementsTypes()
                };

                UpdateModel(categoryViewModel);
                categoryRepository.CreateCategory(categoryViewModel.category);

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                return View("CreateCategory");            }
        }


        public ActionResult Edit(Guid id)
        {
            ViewModels.CreateCategoryViewModel categoryViewModel = new ViewModels.CreateCategoryViewModel()
            {
                category = categoryRepository.getCategorybyId(id),
                type = movementTypeRepository.GetAllMovementsTypes()
            };

            return View("EditCategory", categoryViewModel);

        }

        [HttpPost]
        public ActionResult Edit (Guid id,FormCollection collection)
        {
            try
            {
                ViewModels.CreateCategoryViewModel categoryViewModel = new ViewModels.CreateCategoryViewModel()
                {
                    category = new Models.CategoryModel(),
                    type = movementTypeRepository.GetAllMovementsTypes()
                };

                UpdateModel(categoryViewModel);

                categoryRepository.UpdateCategory(categoryViewModel.category);

                return RedirectToAction("Index");

            }
            catch 
            {

                return View("EditCategory");
            }
        }

        public ActionResult Delete (Guid id)
        {
            Models.CategoryModel categoryToDetelete = categoryRepository.getCategorybyId(id);
            return View("DeleteCategory", categoryToDetelete);
        }

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {

                categoryRepository.DeleteCategory(id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View("DeleteCategory");
            }
        }
    }
}