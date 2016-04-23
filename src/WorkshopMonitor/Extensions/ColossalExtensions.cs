using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkshopMonitor.Overwatch;

namespace WorkshopMonitor.Extensions
{
    public static class ColossalExtensions
    {
        public static AssetType ToAssetType(this ItemClass.Service service)
        {
            switch (service)
            {
                case ItemClass.Service.None:
                    return AssetType.None;
                case ItemClass.Service.Residential:
                    return AssetType.Residential;
                case ItemClass.Service.Commercial:
                    return AssetType.Commercial;
                case ItemClass.Service.Industrial:
                    return AssetType.Industrial;
                case ItemClass.Service.Office:
                    return AssetType.Office;
                case ItemClass.Service.Electricity:
                    return AssetType.Electricity;
                case ItemClass.Service.Water:
                    return AssetType.WaterAndSewage;
                case ItemClass.Service.Beautification:
                    return AssetType.Beautification;
                case ItemClass.Service.Garbage:
                    return AssetType.Garbage;
                case ItemClass.Service.HealthCare:
                    return AssetType.Healthcare;
                case ItemClass.Service.PoliceDepartment:
                    return AssetType.Police;
                case ItemClass.Service.Education:
                    return AssetType.Education;
                case ItemClass.Service.Monument:
                    return AssetType.Monuments;
                case ItemClass.Service.FireDepartment:
                    return AssetType.FireDepartment;
                case ItemClass.Service.PublicTransport:
                    return AssetType.PublicTransport;
                case ItemClass.Service.Road:
                    return AssetType.Roads;

                case ItemClass.Service.Unused1:
                    return AssetType.Other;
                case ItemClass.Service.Unused2:
                    return AssetType.Other;

                case ItemClass.Service.Citizen:
                    return AssetType.Other;
                case ItemClass.Service.Tourism:
                    return AssetType.Other;
                case ItemClass.Service.Government:
                    return AssetType.Other;

                default:
                    return AssetType.None;
            }
        }
    }
}
