using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PontoColeta.Data;
using PontoColeta.Models;
using PontoColeta.ViewModels.CoordinateViewModels;

namespace PontoColeta.Repositories
{
    public class CoordinateRepository
    {
        private readonly DataContext _context;

        public CoordinateRepository(DataContext context)
        {
            _context = context;
        }

        public List<ListCoordinateViewModel> Get() => _context.Coordinates
                .Include(x => x.Category)
                .Select(x => new ListCoordinateViewModel
                {
                    Id = x.Id,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    NameOfPlace = x.NameOfPlace,
                    Category = x.Category.Title,
                    CategoryId = x.CategoryId
                })
                .AsNoTracking()
                .ToList();

        public ListCoordinateViewModel Get(int id) => _context.Coordinates
                .Select(x => new ListCoordinateViewModel
                {
                    Id = x.Id,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    NameOfPlace = x.NameOfPlace,
                    Category = x.Category.Title,
                    CategoryId = x.CategoryId
                })
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

        public List<ListCoordinateViewModel> GetByCategory(int categoryId) => _context.Coordinates
                .Include(x => x.Category)
                .Select(x => new ListCoordinateViewModel
                {
                    Id = x.Id,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    NameOfPlace = x.NameOfPlace,
                    Category = x.Category.Title,
                    CategoryId = x.CategoryId
                })
                .AsNoTracking()
                .Where(x => x.CategoryId == categoryId)
                .ToList();

        public void Save(Coordinate coordinate)
        {
            _context.Coordinates.Add(coordinate);
            _context.SaveChanges();
        }

        public void Delete(ListCoordinateViewModel coordinateModel)
        {
            Coordinate coordinate = new Coordinate();
            coordinate.Id = coordinateModel.Id;
            coordinate.Latitude = coordinateModel.Latitude;
            coordinate.Longitude = coordinateModel.Longitude;
            coordinate.NameOfPlace = coordinateModel.NameOfPlace;
            coordinate.CategoryId = coordinateModel.CategoryId;

            _context.Coordinates.Remove(coordinate);
            _context.SaveChanges();
        }
    }
}