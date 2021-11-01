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


        public List<CategoryModel> getAllCategories()
        {
            var categories = new List<CategoryModel>();

            foreach(var category in dbContext.Categories)
            {
                categories.Add(MapObjectToModel(category));
            }

            return categories;
        }

        public List<CategoryModel> GetCategoryByMovementType(Guid id)
        {
            var categories = new List<CategoryModel>();

            foreach (var category in dbContext.Categories.Where(x => x.MovementTypeId == id))
            {
                categories.Add(MapObjectToModel(category));
            }

            return categories;
        }

        public CategoryModel getCategorybyId(Guid id)
        {
            return MapObjectToModel(dbContext.Categories.FirstOrDefault(x => x.CategoryId == id));

        }



        public void CreateCategory(CategoryModel categoryModel)
        {
            //categoryModel.CategoryId = Guid.NewGuid();
         
            dbContext.Categories.InsertOnSubmit(MapModelToObject(categoryModel));
            dbContext.SubmitChanges();
        }


        public void UpdateCategory (CategoryModel categoryModel)
        {
            var existingCategory = dbContext.Categories.FirstOrDefault(x => x.CategoryId == categoryModel.CategoryId);


            if (existingCategory != null)
                {

                existingCategory.CategoryId = categoryModel.CategoryId;
                existingCategory.Name = categoryModel.Name;
                existingCategory.MovementTypeId = categoryModel.MovementTypeId;

                dbContext.SubmitChanges();
            }
        }

        public void DeleteCategory(Guid id)
        {
            var existingCategory = dbContext.Categories.FirstOrDefault(x => x.CategoryId == id);
            dbContext.Categories.DeleteOnSubmit(existingCategory);
            dbContext.SubmitChanges();
        }


        private Category MapModelToObject(Models.CategoryModel categoryModel)
        {
            var dbCategory = new Models.DBObjects.Category();
            dbCategory.CategoryId = categoryModel.CategoryId;
            dbCategory.Name = categoryModel.Name;
            dbCategory.MovementTypeId = categoryModel.MovementTypeId;

            return dbCategory;

        }

        private CategoryModel MapObjectToModel(Models.DBObjects.Category dbCategoryModel)
        {
            var categoryModel = new CategoryModel();
            categoryModel.CategoryId = dbCategoryModel.CategoryId;
            categoryModel.Name = dbCategoryModel.Name;
            categoryModel.MovementTypeId = dbCategoryModel.MovementTypeId;

            return categoryModel;
        }
    }
}