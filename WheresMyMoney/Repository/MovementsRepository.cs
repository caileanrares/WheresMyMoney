using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WheresMyMoney.Models;
using WheresMyMoney.Models.DBObjects;
using WheresMyMoney.ViewModels;

namespace WheresMyMoney.Repository
{
    public class MovementsRepository
    {
        private Models.DBObjects.WheresMyMoneyModelsDataContext dbContext;

        public MovementsRepository()
        {
            dbContext = new Models.DBObjects.WheresMyMoneyModelsDataContext();
        }

        public MovementsRepository(Models.DBObjects.WheresMyMoneyModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public List<MovementModel> GetAllMovements()
        {
            List<MovementModel> movementsList = new List<MovementModel>();

            foreach (var dbMovement in dbContext.Movements)
            {
                movementsList.Add(MapDbObjectToModel(dbMovement));

                
            }

            return movementsList;

        }


        public List<MovementModel> GetMovementsByUser(Guid id)
        {
            List<MovementModel> Movements = new List<MovementModel>();

            foreach (var movement in dbContext.Movements.Where(x => x.UserId == id))
            {
                Movements.Add(MapDbObjectToModel(movement));

               
            }

            return Movements;

        }


        public MovementModel GetMovementById (Guid id)
        {
            return MapDbObjectToModel(dbContext.Movements.FirstOrDefault(x => x.MovementId == id));

        }


        public List<MovementModel> GetMovementByUserAndDateInterval(Guid id, DateTime startDate, DateTime endDate)
        {
            List<MovementModel> Movements = GetMovementsByUser(id);
            List<MovementModel> MovementByDate = new List<MovementModel>();

            foreach (var movement in Movements.Where(x => x.Date >= startDate && x.Date <= endDate))
            {
                MovementByDate.Add(movement);
            }

            return MovementByDate;
        }

        public List<MovementModel> GetMovementByUserAndCategory(Guid id, Guid categoryID)
        {
            List<MovementModel> Movements = GetMovementsByUser(id);
            List<MovementModel> MovementsByCategory = new List<MovementModel>();

            foreach (var movement in Movements.Where(x => x.CategoryId == categoryID))
            {
                MovementsByCategory.Add(movement);
            }

            return MovementsByCategory;
        }

        public List<MovementModel> GetMovementsByUserAndType(Guid id, Guid typeID)
        {
            List<MovementModel> Movements = GetMovementsByUser(id);
            List<MovementModel> MovementsByType = new List<MovementModel>();

            foreach (var movement in Movements.Where(x => x.MovementTypeId == typeID))
            {
                MovementsByType.Add(movement);
            }

            return MovementsByType;
        }

        public void CreateMovement(MovementModel movementModel)
        {
            movementModel.MovementId = Guid.NewGuid();

            if (movementModel.MovementTypeId==dbContext.MovementTypes.FirstOrDefault(x=>x.Name=="Expense").MovementTypeId)
            {
                movementModel.VALUE = movementModel.VALUE * -1;
            }
            dbContext.Movements.InsertOnSubmit(MapModelToDbObject(movementModel));
            dbContext.SubmitChanges();

        }


        public void UpdateMovement(MovementModel movementModel)
        {
            var existingMovement = dbContext.Movements.FirstOrDefault(x => x.MovementId == movementModel.MovementId);

            if (existingMovement != null)
            {
                existingMovement.MovementId = movementModel.MovementId;
                existingMovement.Date = movementModel.Date;

                if(movementModel.MovementTypeId == dbContext.MovementTypes.FirstOrDefault(x => x.Name == "Expense").MovementTypeId && movementModel.VALUE>0)
                { existingMovement.VALUE = movementModel.VALUE * -1; }
                else
                {
                    existingMovement.VALUE = movementModel.VALUE;
                }
                
                existingMovement.CategoryId = movementModel.CategoryId;
                existingMovement.UserId = movementModel.UserId;
                existingMovement.Notes = movementModel.Notes;
                existingMovement.MovementTypeId = movementModel.MovementTypeId;

                dbContext.SubmitChanges();
            }
        }

        public void DeleteMovement(Guid id)
        {
            var existingMovement = dbContext.Movements.FirstOrDefault(x => x.MovementId == id);
            if (existingMovement != null)
            {
                dbContext.Movements.DeleteOnSubmit(existingMovement);
                dbContext.SubmitChanges();
            }
        }


        public List<UserMovementsNamesViewModel> GetAllMovementsCustomView()
        {
            List<UserMovementsNamesViewModel>  userMovementsNamesViewModel = new List<UserMovementsNamesViewModel>();
            List<MovementModel> AllMovements = GetAllMovements();
            //UserMovementsNamesViewModel tempNewModel = new UserMovementsNamesViewModel();



         for (var i=0;i< AllMovements.Count;i++)
            {
                userMovementsNamesViewModel.Add(new UserMovementsNamesViewModel());

                userMovementsNamesViewModel[i].MovementId = AllMovements[i].MovementId;
                userMovementsNamesViewModel[i].Date = AllMovements[i].Date;
                userMovementsNamesViewModel[i].Category = dbContext.Categories.FirstOrDefault(x => x.CategoryId == AllMovements[i].CategoryId).Name;
                userMovementsNamesViewModel[i].MovementType = dbContext.MovementTypes.FirstOrDefault(x => x.MovementTypeId == AllMovements[i].MovementTypeId).Name;
                userMovementsNamesViewModel[i].Notes = AllMovements[i].Notes;
                userMovementsNamesViewModel[i].VALUE = AllMovements[i].VALUE;                
            };                    
            return userMovementsNamesViewModel;           
        }



        public List<UserMovementsNamesViewModel> GetAllMovementsCustomViewByUser(string Email)
        {
            List<UserMovementsNamesViewModel> userMovementsNamesViewModel = new List<UserMovementsNamesViewModel>();

            List<MovementModel> AllMovements = GetMovementsByUser(dbContext.Users.FirstOrDefault(x => x.Email ==
            Email).UserId);

            //Guid aGuid = new Guid("457D7162-5370-4C2D-B61C-EFFDE897B20");
            //List<MovementModel> AllMovements = GetMovementsByUser(aGuid);
            //UserMovementsNamesViewModel tempNewModel = new UserMovementsNamesViewModel();


            for (var i = 0; i < AllMovements.Count; i++)
            {
                userMovementsNamesViewModel.Add(new UserMovementsNamesViewModel());

                userMovementsNamesViewModel[i].MovementId = AllMovements[i].MovementId;
                userMovementsNamesViewModel[i].Date = AllMovements[i].Date;
                userMovementsNamesViewModel[i].Category = dbContext.Categories.FirstOrDefault(x => x.CategoryId == AllMovements[i].CategoryId).Name;
                userMovementsNamesViewModel[i].MovementType = dbContext.MovementTypes.FirstOrDefault(x => x.MovementTypeId == AllMovements[i].MovementTypeId).Name;
                userMovementsNamesViewModel[i].Notes = AllMovements[i].Notes;
                userMovementsNamesViewModel[i].VALUE = AllMovements[i].VALUE;
            };
            return userMovementsNamesViewModel;
        }




        private Models.DBObjects.Movement MapModelToDbObject(MovementModel movementModel)
        {
            Models.DBObjects.Movement dbMovementModel = new Models.DBObjects.Movement();

            if (movementModel!=null)
            {
                dbMovementModel.MovementId = movementModel.MovementId;
                dbMovementModel.Date = movementModel.Date;
                dbMovementModel.VALUE = movementModel.VALUE;
                dbMovementModel.UserId = movementModel.UserId;
                dbMovementModel.CategoryId = movementModel.CategoryId;
                dbMovementModel.Notes = movementModel.Notes;
                dbMovementModel.MovementTypeId = movementModel.MovementTypeId;
                

                return dbMovementModel;
            }
            return null;
        }
                    
        
        
          
          
    

        private MovementModel MapDbObjectToModel(Models.DBObjects.Movement dbMovement)
        {
            var movementModel = new Models.MovementModel();

            if(dbMovement!=null)
            {
                movementModel.MovementId = dbMovement.MovementId;
                movementModel.Date = dbMovement.Date;
                movementModel.VALUE = dbMovement.VALUE;
                movementModel.UserId = dbMovement.UserId;
                movementModel.CategoryId = dbMovement.CategoryId;
                movementModel.Notes = dbMovement.Notes;
                movementModel.MovementTypeId = dbMovement.MovementTypeId;

                return movementModel;
            }

            return null;
        }



    }
}