using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheresMyMoney.Models;

namespace WheresMyMoney.Repository
{
    public class MovementTypeRepository
    {
        private Models.DBObjects.WheresMyMoneyModelsDataContext dbContext;

        public MovementTypeRepository()
        {
            this.dbContext = new Models.DBObjects.WheresMyMoneyModelsDataContext();
        }

        public MovementTypeRepository(Models.DBObjects.WheresMyMoneyModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<MovementTypeModel> GetAllMovementsTypes()
        {
            var AllMovementTypes = new List<MovementTypeModel>();

            foreach (var Type in dbContext.MovementTypes)
            {
                AllMovementTypes.Add(MapObjectToModel(Type));
            }

            return AllMovementTypes;
        }


        

        private MovementTypeModel MapObjectToModel(Models.DBObjects.MovementType dbMovementType)
        {
            var movementType = new MovementTypeModel();
            movementType.MovementTypeId = dbMovementType.MovementTypeId;
            movementType.Name = dbMovementType.Name;

            return movementType;
        }
    }
}