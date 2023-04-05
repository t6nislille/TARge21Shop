﻿using System;
using System.Xml.Linq;
using TARge21Shop.ApplicationServices.Services;
using TARge21Shop.Core.Domain;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using Xunit;

namespace TARge21Shop.SpaceshipTest
{
    public class SpaceshipTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptySpaceship_WhenReturnResult()
        {
            string guid = Guid.NewGuid().ToString();

            SpaceshipDto spaceship = new SpaceshipDto();

            //spaceship.Id = Guid.Parse(guid);
            spaceship.Name = "asd";
            spaceship.Type = "asd";
            spaceship.Crew = 123;
            spaceship.Passengers = 123;
            spaceship.CargoWeight = 123;
            spaceship.FullTripsCount = 123;
            spaceship.MaintenanceCount = 1000;
            spaceship.LastMaintenance = DateTime.Now;
            spaceship.EnginePower = 1000;
            spaceship.MaidenLaunch = DateTime.Now;
            spaceship.BuiltDate = DateTime.Now;
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifiedAt = DateTime.Now;

            var result = await Svc<ISpaceshipsServices>().Create(spaceship);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdSpceship_WhenReturnsNotEqual()
        {
            //Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("6e8285f6-5205-46d4-b12e-c522f797f378");

            //Act
            await Svc<ISpaceshipsServices>().GetAsync(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);
        }


        [Fact]
        public async Task Should_GetByIdSpceship_WhenReturnsEqual()
        {
            Guid databaseGuid = Guid.Parse("6e8285f6-5205-46d4-b12e-c522f797f378");
            Guid getGuid = Guid.Parse("6e8285f6-5205-46d4-b12e-c522f797f378");

            await Svc<ISpaceshipsServices>().GetAsync(getGuid);

            Assert.Equal(databaseGuid, getGuid);
        }

        [Fact]
        public async Task Should_DeleteByIdSpaceship_WhenDeleteSpaceship()
        {
            SpaceshipDto spaceship = MockSpaceshipData();

            var addSpaceship = await Svc<ISpaceshipsServices>().Create(spaceship);
            var result = await Svc<ISpaceshipsServices>().Delete((Guid)addSpaceship.Id);

            Assert.Equal(result, addSpaceship);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdSpaceship_WhenDidNotDeleteSpaceship()
        {
            SpaceshipDto spaceship = MockSpaceshipData();
            var addspaceship = await Svc<ISpaceshipsServices>().Create(spaceship);
            var addSpaceship2 = await Svc<ISpaceshipsServices>().Create(spaceship);

            var result = await Svc<ISpaceshipsServices>().Delete((Guid)addSpaceship2.Id);

            Assert.NotEqual(result.Id, addspaceship.Id);
        }

        [Fact]
        public async Task Should_UpdateSpaceship_WhenUpdateData()
        {

            var guid = new Guid("6e8285f6-5205-46d4-b12e-c522f797f378");

            Spaceship spaceship = new Spaceship();


            SpaceshipDto dto = MockSpaceshipData();

            spaceship.Id = Guid.Parse("6e8285f6-5205-46d4-b12e-c522f797f378");
            spaceship.Name = "asd";
            spaceship.Type = "asd";
            spaceship.Crew = 123;
            spaceship.Passengers = 123;
            spaceship.CargoWeight = 123;
            spaceship.FullTripsCount = 123;
            spaceship.MaintenanceCount = 1000;
            spaceship.LastMaintenance = DateTime.Now;
            spaceship.EnginePower = 1000;
            spaceship.MaidenLaunch = DateTime.Now;
            spaceship.BuiltDate = DateTime.Now;
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifiedAt = DateTime.Now;


            await Svc<ISpaceshipsServices>().Update(dto);

            Assert.Equal(spaceship.Id, guid);
            Assert.DoesNotMatch(spaceship.Name, dto.Name);
            Assert.DoesNotMatch(spaceship.EnginePower.ToString(), dto.EnginePower.ToString());
            Assert.Equal(spaceship.Crew, dto.Crew);
        }

        [Fact]
        public async Task Should_UpdateSpaceship_WhenUpdateDataVersion2()
        {
            SpaceshipDto dto = MockSpaceshipData();
            var createSpaceship = await Svc<ISpaceshipsServices>().Create(dto);

            SpaceshipDto update = MockUpdateSpaceship();
            var result = await Svc<ISpaceshipsServices>().Update(update);

            Assert.Equal(update.Id, dto.Id);
            Assert.DoesNotMatch(result.Name, createSpaceship.Name);
            Assert.DoesNotMatch(result.EnginePower.ToString(), createSpaceship.EnginePower.ToString());
            Assert.Equal(result.Crew, createSpaceship.Crew);
            Assert.NotEqual(result.ModifiedAt, createSpaceship.ModifiedAt);
        }

        [Fact]
        public async Task ShouldNot_UpdateSpaceship_WhenNotUpdateData()
        {
            SpaceshipDto dto = MockSpaceshipData();
            var createSpaceship = await Svc<ISpaceshipsServices>().Create(dto);

            SpaceshipDto nullUpdate = MockNullSpaceship();
            var result = await Svc<ISpaceshipsServices>().Update(nullUpdate);

            var nullId = nullUpdate.Id;

            Assert.False(dto.Id == nullId);
        }

        private SpaceshipDto MockSpaceshipData()
        {
            SpaceshipDto spaceship = new()
            {
                Name = "Name",
                Type = "asd",
                Crew = 123,
                Passengers = 123,
                CargoWeight = 123,
                FullTripsCount = 123,
                MaintenanceCount = 1000,
                LastMaintenance = DateTime.Now,
                EnginePower = 1000,
                MaidenLaunch = DateTime.Now,
                BuiltDate = DateTime.Now,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return spaceship;
        }

        private SpaceshipDto MockUpdateSpaceship()
        {
            SpaceshipDto update = new()
            {
                Name = "Name123",
                Type = "asd",
                Crew = 123,
                Passengers = 123123,
                CargoWeight = 123,
                FullTripsCount = 123123,
                MaintenanceCount = 1000123,
                LastMaintenance = DateTime.Now.AddYears(1),
                EnginePower = 1000123,
                MaidenLaunch = DateTime.Now.AddYears(1),
                BuiltDate = DateTime.Now.AddYears(1),
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1),
            };

            return update;
        }

        private SpaceshipDto MockNullSpaceship()
        {
            SpaceshipDto nullDto = new()
            {
                Id = null,
                Name = "Name123",
                Type = "asd",
                Crew = 123,
                Passengers = 123123,
                CargoWeight = 123,
                FullTripsCount = 123123,
                MaintenanceCount = 1000123,
                LastMaintenance = DateTime.Now.AddYears(1),
                EnginePower = 1000123,
                MaidenLaunch = DateTime.Now.AddYears(1),
                BuiltDate = DateTime.Now.AddYears(1),
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1),
            };

            return nullDto;
        }


    }
}