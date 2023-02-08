﻿using System.ComponentModel.DataAnnotations;

namespace TARge21Shop.Core.Domain.Car
{
    public class Car
    {
        [Key]
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int EnginePower { get; set; }
        public decimal Weight { get; set; }
        public decimal Lenght { get; set; }
        public decimal Width { get; set; }
        public int MaxSpeed { get; set; }
        public decimal Mileage { get; set; }
        public string FuelType { get; set; }
        public decimal FuelConsumption { get; set; }
        public DateTime ProductionDate { get; set; }

        // only in database
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}