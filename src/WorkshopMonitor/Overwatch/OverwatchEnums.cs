using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor
{
    [Flags]
    public enum BuildingType
    {
        None = 0,

        // Player buildings
        Electricity = 1,
        WaterAndSewage = 2,
        Garbage = 4,
        Healthcare = 8,
        FireDepartment = 16,
        Police = 32,
        Education = 64,
        PublicTransport = 128,
        Beautification = 256,
        Monuments = 512,
        PlayerOther = 1024,

        // Zoned buildings
        Residential = 2048,
        Commercial = 4096,
        Industrial = 8192,
        Office = 16384,
        ZonedOther = 32768,

        // Other
        Other = 65536,

        // Combinations
        PlayerBuilding = Electricity | WaterAndSewage | Garbage | Healthcare | FireDepartment | Police | Education | PublicTransport | Beautification | Monuments | PlayerOther,
        ZonedBuilding = Residential | Commercial | Industrial | Office | ZonedOther,

        All = PlayerBuilding | ZonedBuilding | Other
    }
}
