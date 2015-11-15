using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Overwatch
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
        Roads = 2048,

        // Zoned buildings
        Residential = 4096,
        Commercial = 8192,
        Industrial = 16384,
        Office = 32768,
        ZonedOther = 65536,

        // Other
        Other = 131072,

        // Combinations
        PlayerBuilding = Electricity | WaterAndSewage | Garbage | Healthcare | FireDepartment | Police | Education | PublicTransport | Beautification | Monuments | PlayerOther,
        ZonedBuilding = Residential | Commercial | Industrial | Office | ZonedOther,

        All = PlayerBuilding | ZonedBuilding | Other,
    }
}