﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;
using TARge21Shop.Models.Spaceship;
using TARge21Shop.Core.Dto;
using TARge21Shop.Models.Spaceship;
using TARge21Shop.Core.Domain;

namespace TARge21Shop.Controllers
{
    public class SpaceshipController : Controller
    {
        private readonly TARge21ShopContext _context;
        private readonly ISpaceshipsServices _spaceshipServices;

        public SpaceshipController
            (
                TARge21ShopContext context,
                ISpaceshipsServices spaceshipServices
            )
        {
            _context = context;
            _spaceshipServices = spaceshipServices;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Spaceship
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new SpaceshipListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ModelType = x.ModelType,
                    Passengers = x.Passengers,
                    BuildOfDate = x.BuildOfDate,
                    LaunchDate = x.LaunchDate
                });

            return View(result);
        }


        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipEditViewModel spaceship = new SpaceshipEditViewModel();

            return View("CreateUpdate", spaceship);
        }


        [HttpPost]
        public async Task<IActionResult> Create(SpaceshipViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                ModelType = vm.ModelType,
                SpaceshipBuilder = vm.SpaceshipBuilder,
                PlaceOfBuild = vm.PlaceOfBuild,
                EnginePower = vm.EnginePower,
                LiftUpToSpaceByTonn = vm.LiftUpToSpaceByTonn,
                Crew = vm.Crew,
                Passengers = vm.Passengers,
                LaunchDate = vm.LaunchDate,
                BuildOfDate = vm.BuildOfDate,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId
                }).ToArray()
            };

            var result = await _spaceshipServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var spaceship = await _spaceshipServices.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabase
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    ImageData = y.ImageData,
                    ImageId = y.Id,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData)),
                    ImageTitle = y.ImageTitle,
                    SpaceshipId = y.Id
                }).ToArrayAsync();

            var vm = new SpaceshipEditViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.ModelType = spaceship.ModelType;
            vm.SpaceshipBuilder = spaceship.SpaceshipBuilder;
            vm.PlaceOfBuild = spaceship.PlaceOfBuild;
            vm.EnginePower = spaceship.EnginePower;
            vm.LiftUpToSpaceByTonn = spaceship.LiftUpToSpaceByTonn;
            vm.Crew = spaceship.Crew;
            vm.Passengers = spaceship.Passengers;
            vm.LaunchDate = spaceship.LaunchDate;
            vm.BuildOfDate = spaceship.BuildOfDate;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;
            vm.Image.AddRange(photos);


            return View("CreateUpdate", vm);
        }


        [HttpPost]
        public async Task<IActionResult> Update(SpaceshipEditViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                ModelType = vm.ModelType,
                SpaceshipBuilder = vm.SpaceshipBuilder,
                PlaceOfBuild = vm.PlaceOfBuild,
                EnginePower = vm.EnginePower,
                LiftUpToSpaceByTonn = vm.LiftUpToSpaceByTonn,
                Crew = vm.Crew,
                Passengers = vm.Passengers,
                LaunchDate = vm.LaunchDate,
                BuildOfDate = vm.BuildOfDate,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId
                })
            };

            var result = await _spaceshipServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spaceship = await _spaceshipServices.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipViewModel()
            {
                Id = spaceship.Id,
                Name = spaceship.Name,
                ModelType = spaceship.ModelType,
                SpaceshipBuilder = spaceship.SpaceshipBuilder,
                PlaceOfBuild = spaceship.PlaceOfBuild,
                EnginePower = spaceship.EnginePower,
                LiftUpToSpaceByTonn = spaceship.LiftUpToSpaceByTonn,
                Crew = spaceship.Crew,
                Passengers = spaceship.Passengers,
                LaunchDate = spaceship.LaunchDate,
                BuildOfDate = spaceship.BuildOfDate,
                CreatedAt = spaceship.CreatedAt,
                ModifiedAt = spaceship.ModifiedAt,
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var product = await _spaceshipServices.Delete(id);

            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId
            };

            var image = await _spaceshipServices.RemoveImage(dto);

            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}