using HkHemanthSharma.Phonebook.Controllers;
using HkHemanthSharma.Phonebook.Models;

namespace HkHemanthSharma.Phonebook.Services
{
    internal class CategoryService
    {
        internal static void AddCategory()
        {
            Category NewCategory = UserInputs.GetNewCategory();
            CategoryController.AddNewCategory(NewCategory);
        }

        internal static void DeleteCategory()
        {
            int Id = UserInputs.GetSingleCategoryId();
            Category SingleCategory = CategoryController.GetCategoryById(Id);
            CategoryController.DeleteCategory(SingleCategory);
        }

        internal static void UpdateCategory()
        {
            int Id = UserInputs.GetSingleCategoryId();
            Category newCategory = UserInputs.GetNewCategory();
            newCategory.Id = Id;
            CategoryController.UpdateCategory(newCategory);
        }

        internal static void ViewAllCategory()
        {
            List<Category> Categories = CategoryController.GetAllCategory();
            UserInterface.ShowAllCategories(Categories);
        }

        internal static void ViewSingleCategory()
        {
            int Id = UserInputs.GetSingleCategoryId();
            Category SingleCategory = CategoryController.GetCategoryById(Id);
            UserInterface.ShowSigleCategory(SingleCategory);
        }
    }
}
