using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PontoColeta.Data;
using PontoColeta.Models;

namespace PontoColeta.Repositories
{
    public class CategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public List<Category> Get()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        public Category Get(int id)
        {
            return 
                _context.Categories
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public void Save(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public void Delete(Category category)
        {
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}