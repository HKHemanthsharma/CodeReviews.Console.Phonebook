using HkHemanthSharma.Phonebook.Models;

namespace HkHemanthSharma.Phonebook.Controllers
{
    internal class CategoryController
    {

        internal static void AddNewCategory(Category newCategory)
        {
            using (PhoneBookDbContext context = new())
            {
                context.Categories.Add(newCategory);
                context.SaveChanges();
            }
        }

        internal static void DeleteCategory(Category singleCategory)
        {
            using (PhoneBookDbContext context = new())
            {
                context.Categories.Remove(singleCategory);
                context.SaveChanges();
            }
        }

        internal static List<Category> GetAllCategory()
        {
            using (PhoneBookDbContext context = new())
            {
                return context.Categories.ToList();
            }
        }

        internal static Category GetCategoryById(int Id)
        {
            using (PhoneBookDbContext context = new())
            {
                return context.Categories.FirstOrDefault(x => x.Id == Id);
            }
        }

        internal static void UpdateCategory(Category UpdatedCategory)
        {
            using (PhoneBookDbContext context = new())
            {
                context.Categories.Update(UpdatedCategory);
                context.SaveChanges();
            }
        }
    }
}

