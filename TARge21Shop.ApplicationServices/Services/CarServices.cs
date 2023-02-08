using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARge21Shop.Core.Domain;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;
using TARge21Shop.Core.Domain.Car;


namespace TARge21Shop.ApplicationServices.Services
{
    public class CarServices : ICarServices
    {
        private readonly TARge21ShopContext _context;
        private readonly IFilesServices _files;
        public CarServices
            (
                TARge21ShopContext context,
                IFilesServices files
            )
        {
            _context = context;
            _files = files;
        }

        public async Task<Car> Create(CarDto dto)
        {
            Car Car = new Car();
            FileToDatabase file = new FileToDatabase();
            Car.Id = Guid.NewGuid();
            Car. Name = dto.Name;
            Car.Type = dto.Type;
            Car.EnginePower = dto.EnginePower;
            Car.Weight = dto.Weight;
            Car.Lenght = dto.Lenght;
            Car.Width = dto.Width;
            Car.MaxSpeed = dto.MaxSpeed;
            Car.Mileage = dto.Mileage;
            Car.FuelType = dto.FuelType;
            Car.FuelConsumption = dto.FuelConsumption;
            Car.ProductionDate = dto.ProductionDate;
            
                 if (dto.Files != null)
            {
                _files.UploadFilesToDatabase(dto, Car);
            }

            await _context.Cars.AddAsync(Car);
            await _context.SaveChangesAsync();

            return Car;

        }

        public async Task<Car> Update(CarDto dto)
        {
            var domain = new Car()
            {
                Id = dto.Id,
                Name = dto.Name,
                Type = dto.Type,
                EnginePower = dto.EnginePower,
                Weight = dto.Weight,
                Lenght = dto.Lenght,
                Width = dto.Width,
                MaxSpeed = dto.MaxSpeed,
                Mileage = dto.Mileage,
                FuelType = dto.FuelType,
                FuelConsumption = dto.FuelConsumption,
                ProductionDate = dto.ProductionDate
            };

            if (dto.Files != null)
            {
                _files.UploadFilesToDatabase(dto, domain);
            }

            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<Car> Delete(Guid id)
        {
            var carId = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.FileToDatabases
                .Where(x => x.CarId == id)
                .Select(y => new FileToDatabaseDto
                {
                    Id = y.Id,
                    ImageTitle = y.ImageTitle,
                    CarId = y.CarId
                })
                .ToArrayAsync();

            await _files.RemoveImagesFromDatabase(images);
            _context.Cars.Remove(carId);
            await _context.SaveChangesAsync();

            return carId;
        }

        public async Task<Car> GetAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }
}