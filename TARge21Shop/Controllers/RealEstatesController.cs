using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;
using TARge21Shop.Models.RealEstate;

namespace TARge21Shop.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly IRealEstatesServices _realEstatesServices;
        private readonly TARge21ShopContext _context;

        public RealEstatesController
            (
                IRealEstatesServices realEstatesServices,
                TARge21ShopContext context
            )
        {
            _realEstatesServices = realEstatesServices;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.RealEstates
                 .OrderByDescending(y => y.CreatedAt)
                 .Select(x => new RealEstateIndexViewModel
                 {
                     Id = x.Id,
                     Address = x.Address,
                     City = x.City,
                     Country = x.Country,
                     Size = x.Size,
                     Price = x.Price,
                 });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel vm = new();

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Address = vm.Address,
                City = vm.City,
                Country = vm.Country,
                Size = vm.Size,
                Price = vm.Price,
                Floor = vm.Floor,
                Region = vm.Region,
                Phone = vm.Phone,
                Fax = vm.Fax,
                PostalCode = vm.PostalCode,
                RoomCount = vm.RoomCount,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _realEstatesServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", vm);
        }

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			var realestate = await _realEstatesServices.GetAsync(id);

			if (realestate == null)
			{
				return NotFound();
			}

			var vm = new RealEstateCreateUpdateViewModel()
			{
				Id = realestate.Id,
				Address = realestate.Address,
				City = realestate.City,
				Country = realestate.Country,
				Size = realestate.Size,
				Price = realestate.Price,
				Floor = realestate.Floor,
				Region = realestate.Region,
				Phone = realestate.Phone,
				Fax = realestate.Fax,
				PostalCode = realestate.PostalCode,
				RoomCount = realestate.RoomCount,
				CreatedAt = realestate.CreatedAt,
				ModifiedAt = realestate.ModifiedAt
			};

			return View("CreateUpdate", vm);
		}


		[HttpPost]
		public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
		{
			var dto = new RealEstateDto()
			{
				Id = vm.Id,
				Address = vm.Address,
				City = vm.City,
				Country = vm.Country,
				Size = vm.Size,
				Price = vm.Price,
				Floor = vm.Floor,
				Region = vm.Region,
				Phone = vm.Phone,
				Fax = vm.Fax,
				PostalCode = vm.PostalCode,
				RoomCount = vm.RoomCount,
				CreatedAt = vm.CreatedAt,
				ModifiedAt = vm.ModifiedAt
			};

			var result = await _realEstatesServices.Update(dto);

			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index), vm);
		}


		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var realestate = await _realEstatesServices.GetAsync(id);

			if (realestate == null)
			{
				return NotFound();
			}

			var vm = new RealEstateDetailsViewModel()
			{
				Id = realestate.Id,
				Address = realestate.Address,
				City = realestate.City,
				Country = realestate.Country,
				Size = realestate.Size,
				Price = realestate.Price,
				Floor = realestate.Floor,
				Region = realestate.Region,
				Phone = realestate.Phone,
				Fax = realestate.Fax,
				PostalCode = realestate.PostalCode,
				RoomCount = realestate.RoomCount,
				CreatedAt = realestate.CreatedAt,
				ModifiedAt = realestate.ModifiedAt
			};

			return View(vm);
		}


		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			var realestate = await _realEstatesServices.GetAsync(id);

			if (realestate == null)
			{
				return NotFound();
			}

			var vm = new RealEstateDeleteViewModel()
			{
				Id = realestate.Id,
				Address = realestate.Address,
				City = realestate.City,
				Country = realestate.Country,
				Size = realestate.Size,
				Price = realestate.Price,
				Floor = realestate.Floor,
				Region = realestate.Region,
				Phone = realestate.Phone,
				Fax = realestate.Fax,
				PostalCode = realestate.PostalCode,
				RoomCount = realestate.RoomCount,
				CreatedAt = realestate.CreatedAt,
				ModifiedAt = realestate.ModifiedAt
			};

			return View(vm);
		}


		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var realestateId = await _realEstatesServices.Delete(id);

			if (realestateId == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
