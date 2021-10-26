using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheresMyMoney.Models;
using WheresMyMoney.Models.DBObjects;

namespace WheresMyMoney.Repository
{
    public class CategoryRepository
    {
        Models.DBObjects.WheresMyMoneyModelsDataContext dbContext;

        public CategoryRepository()
        {
            dbContext = new Models.DBObjects.WheresMyMoneyModelsDataContext();
        }

        public CategoryRepository(Models.DBObjects.WheresMyMoneyModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<CategoryModel> GetCategoryByMovementType(Guid id)
        {
            var categories = new List<CategoryModel>();

            foreach (Models.DBObjects.Category category in dbContext.Categories.Where(x => x.MovementTypeId == id))
            {
                categories.Add(MapObjectToModel(category));
            }

            return categories;
        }

        public void CreateCategory(CategoryModel categoryModel, Guid MovementTypeId)
        {
            categoryModel.CategoryId = Guid.NewGuid();
            categoryModel.MovementTypeId = MovementTypeId;
            dbContext.Categories.InsertOnSubmit(MapModelToObject(categoryModel));

        }

        private Category MapModelToObject(CategoryModel categoryModel)
        {
            throw new NotImplementedException();
        }

        private CategoryModel MapObjectToModel(object p)
        {
            throw new NotImplementedException();
        }
    }
}