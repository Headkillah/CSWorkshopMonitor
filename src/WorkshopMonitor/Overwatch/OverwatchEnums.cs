using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkshopMonitor.Overwatch
{
    [Flags]
    public enum AssetType : int
    {
        None = 0,

        // Player buildings
        Electricity = 1,
        WaterAndSewage = Electricity * 2,
        Garbage = WaterAndSewage * 2,
        Healthcare = Garbage * 2,
        FireDepartment = Healthcare * 2,
        Police = FireDepartment * 2,
        Education = Police * 2,
        PublicTransport = Education * 2,
        Beautification = PublicTransport * 2,
        Monuments = Beautification * 2,
        PlayerOther = Monuments * 2,
        Roads = PlayerOther * 2,

        // Zoned buildings
        Residential = Roads * 2,
        Commercial = Residential * 2,
        Industrial = Commercial * 2,
        Office = Industrial * 2,
        ZonedOther = Office * 2,

        Prop = ZonedOther * 2,
        Tree = Prop * 2,

        Other = Tree * 2,

        // Combinations
        PlayerBuilding = Electricity | WaterAndSewage | Garbage | Healthcare | FireDepartment | Police | Education | PublicTransport | Beautification | Monuments | PlayerOther,
        ZonedBuilding = Residential | Commercial | Industrial | Office | ZonedOther,

        All = PlayerBuilding | ZonedBuilding | Prop | Tree | Other,
    }
    
}