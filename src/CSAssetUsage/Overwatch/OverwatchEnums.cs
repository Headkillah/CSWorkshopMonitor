using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSAssetUsage
{
    [Flags]
    public enum BuildingType
    {
        None = 0, 

        // Player buildings
        Cemetery = 1,
        LandfillSite = 2,
        FireStation = 4,
        PoliceStation = 8,
        Hospital = 16,
        Park = 32,
        PowerPlant = 64,
        WaterFacility = 128,
        PlayerOther = 256,

        // Zoned buildings
        ResidentialBuilding = 512,
        CommercialBuilding = 1024,
        IndustrialBuilding = 2048,
        OfficeBuilding = 4096,
        ZonedOther = 8192,

        // Other

        Other = 16384,

        PlayerBuilding = Cemetery | LandfillSite | FireStation | PoliceStation | Hospital | Park | PowerPlant | WaterFacility | PlayerOther,
        ZonedBuilding = ResidentialBuilding | CommercialBuilding | IndustrialBuilding | OfficeBuilding | ZonedOther
    }
}
