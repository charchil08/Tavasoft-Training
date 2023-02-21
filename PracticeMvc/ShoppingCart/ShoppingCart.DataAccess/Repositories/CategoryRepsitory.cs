using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.Repositories
{
    public class CategoryRepsitory : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepsitory(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        void ICategoryRepository.Update(Category category)
        {
            Category categoryDb = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            if (categoryDb != null)
            {
                categoryDb.Name = category.Name;
                categoryDb.DisplayOrder = category.DisplayOrder;
            }
        }
    }
}