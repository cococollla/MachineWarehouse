﻿namespace MachineWarehouse.Models.Entities
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
    }
}
