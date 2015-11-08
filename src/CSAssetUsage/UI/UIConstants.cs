using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorkshopMonitor
{
    public class UIConstants
    {
        // Defaults
        public const int DefaultPanelWidth = MainWindowWidth - 15;

        // Columns/rows
        public const int WorkshopItemTypeLabelXOffset = 10;
        public const int WorkshopItemTypeIconXOffset = 18;
        public const int WorkshopItemTypeYOffset = 3;
        public const int WorkshopItemNameLabelXOffset = WorkshopItemTypeLabelXOffset + 70;
        public const int NumberUsedLabelXOffset = WorkshopItemNameLabelXOffset + 500;
        public const int LabelYOffset = 6;
        public const int WorkshopItemInfoButtonXOffset = MainWindowWidth - 60;
        public const int ButtonFieldYOffset = 3;
        public const string WorkshopItemInfoButtonNormalSprite = "CityInfo";
        public const string WorkshopItemInfoButtonPressedSprite = "CityInfoPressed";

        public const string WorkshopItemInfoButtonHoveredSprite = "CityInfoHovered";
        public const int WorkshopItemInfoButtonSize = 25;

        public const int WorkshopItemTypeIconSize = 25;


        // Main window
        public const int MainWindowMainPanelWidthOffset = 6;
        public const float MainWindowScrollablePanelWidthOffset = 5f;
        public static Color32 MainWindowColor = new Color32(58, 88, 104, 255);
        public const string MainWindowBackgroundSprite = "MenuPanel2";
        public const int MainWindowWidth = 1000;
        public const int MainWindowHeight = 600;

        // Scrollbar
        public const int ScrollbarIncrementCount = 50;
        public const int ScrollbarMouseWheelOffset = 50;
        public const string ScrollbarTrackSprite = "ScrollbarTrack";
        public const string ScrollbarThumbSprite = "ScrollbarThumb";

        // Title
        public static Vector3 TitleRelativePosition = new Vector3(50, 13);
        public const int TitlePanelWidth = DefaultPanelWidth;
        public const int TitlePanelHeight = 40;

        // Title draghandle
        public const int DragHandleWidthOffset = 50;

        // Title closebutton
        public const int CloseButtonRelativePositionOffset = 35;
        public const string CloseButtonNormalBgSprite = "buttonclose";
        public const string CloseButtonHoveredBgSprite = "buttonclosehover";
        public const string CloseButtonPressedBgSprite = "buttonclosepressed";

        // Title icon 
        public static Vector3 TitleIconRelativePosition = new Vector3(5, 10);

        // Autolayout
        public static RectOffset AutoLayoutPadding = new RectOffset(0, 0, 1, 1);

        // Caption panel
        public const float CaptionPanelTextScale = 1.2f;
        public const int CaptionPanelWidth = DefaultPanelWidth;
        public const int CaptionPanelHeight = 30;
        public const int CaptionPanelLabelOffset = 5;

        // Filter panel
        public const int FilterPanelWidth = MainWindowWidth;
        public const int FilterPanelHeight = 45;
        public const string FilterSpriteAll = "ToolbarIconZoomOutGlobe";
        public const string FilterSpriteElectricity = "ToolbarIconElectricity";
        public const string FilterSpriteWaterAndSewage = "ToolbarIconWaterAndSewage";
        public const string FilterSpriteGarbage = "ToolbarIconGarbage";
        public const string FilterSpriteHealthcare = "ToolbarIconHealthcare";
        public const string FilterSpriteFireDepartment = "ToolbarIconFireDepartment";
        public const string FilterSpritePolice = "ToolbarIconPolice";
        public const string FilterSpriteEducation = "ToolbarIconEducation";
        public const string FilterSpritePublicTransport = "ToolbarIconPublicTransport";
        public const string FilterSpriteBeautification = "ToolbarIconBeautification";
        public const string FilterSpriteMonuments = "ToolbarIconMonuments";
        public const string FilterSpriteResidential = "InfoIconOutsideConnectionsPressed";
        public const string FilterSpriteCommercial = "InfoIconOutsideConnectionsPressed";
        public const string FilterSpriteIndustrial = "InfoIconOutsideConnectionsPressed";
        public const string FilterSpriteOffice = "InfoIconOutsideConnectionsPressed";
        public static Color FilterColorResidential = Color.green;
        public static Color FilterColorCommercial = new Color32(100, 100, 255, 255);
        public static Color FilterColorIndustrial = Color.yellow;
        public static Color FilterColorOffice = new Color32(0, 255, 255, 255);
        public const int FilterFirstOptionXOffset = 10;
        public const int FilterOptionYOffset = 7;
        public const int FilterCheckBoxSize = 12;
        public const int FilterCheckBoxYOffset = 10;
        public const int FilterIconSize = 30;
        public const int FilterCheckBoxIconDistance = 15;
        public const int FilterGroupedOptionsOffset = 20;

        // WorkshopItem row
        public const int WorkshopItemRowWidth = DefaultPanelWidth - 17;
        public const int WorkshopItemRowHeight = 30;
        public const float WorkshopItemRowTextScale = 1f;
        public static Color32 WorkshopItemRowTextColor = new Color32(185, 221, 254, 255);
        public static Color32 WorkshopItemRowOddColor = new Color32(150, 150, 150, 255);
        public static Color32 WorkshopItemRowEvenColor = new Color32(120, 130, 130, 255);
        public const string WorkshopItemRowBackgroundSprite = "GenericPanelLight";

        public static string GetBuildingTypeSprite(BuildingType buildingType)
        {
            switch (buildingType)
            {

                case BuildingType.Electricity:
                    return UIConstants.FilterSpriteElectricity;
                case BuildingType.WaterAndSewage:
                    return UIConstants.FilterSpriteWaterAndSewage;
                case BuildingType.Garbage:
                    return UIConstants.FilterSpriteGarbage;
                case BuildingType.Healthcare:
                    return UIConstants.FilterSpriteHealthcare;
                case BuildingType.FireDepartment:
                    return UIConstants.FilterSpriteFireDepartment;
                case BuildingType.Police:
                    return UIConstants.FilterSpritePolice;
                case BuildingType.Education:
                    return UIConstants.FilterSpriteEducation;
                case BuildingType.PublicTransport:
                    return UIConstants.FilterSpritePublicTransport;
                case BuildingType.Beautification:
                    return UIConstants.FilterSpriteBeautification;
                case BuildingType.Monuments:
                    return UIConstants.FilterSpriteMonuments;
                case BuildingType.Residential:
                    return UIConstants.FilterSpriteResidential;
                case BuildingType.Commercial:
                    return UIConstants.FilterSpriteCommercial;
                case BuildingType.Industrial:
                    return UIConstants.FilterSpriteIndustrial;
                case BuildingType.Office:
                    return UIConstants.FilterSpriteOffice;
                default: return string.Empty;
            }
        }

        public static Color32 GetBuildingTypeColor(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.Residential:
                    return UIConstants.FilterColorResidential;
                case BuildingType.Commercial:
                    return UIConstants.FilterColorCommercial;
                case BuildingType.Industrial:
                    return UIConstants.FilterColorIndustrial;
                case BuildingType.Office:
                    return UIConstants.FilterColorOffice;
                default:
                    return new Color32(255, 255, 255, 255);
            }
        }
    }
}