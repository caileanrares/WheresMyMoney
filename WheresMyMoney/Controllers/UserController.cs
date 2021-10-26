using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheresMyMoney.Repository;

namespace WheresMyMoney.Controllers
{
    public class UserController : Controller
    {
        Repository.UserRepository userRepository = new Repository.UserRepository();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(Guid id)
        {
            var users = userRepository.GetUserById(id);
            return View("UserDetails",users);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View("CreateUser");
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var userModel = new Models.UserModel();

                UpdateModel(userModel);
                userModel.Email = User.Identity.Name;
                userRepository.CreateUser(userModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateUser");
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(Guid id)
        {
            var userModel = userRepository.GetUserById(id);
            return View("DeleteUser", userModel);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                userRepository.DeleteUser(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteUser");
            }
        }
    }
}
