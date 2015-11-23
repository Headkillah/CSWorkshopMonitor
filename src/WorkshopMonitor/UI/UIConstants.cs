using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorkshopMonitor.Overwatch;

namespace WorkshopMonitor.UI
{
    public class UIConstants
    {
        // Main window
        public const int MainWindowMainPanelWidthOffset = 6;
        public const float MainWindowScrollablePanelWidthOffset = 5f;
        public static Color32 MainWindowColor = new Color32(58, 88, 104, 255);
        public const string MainWindowBackgroundSprite = "MenuPanel2";
        public const int MainWindowWidth = 1100;
        public const int MainWindowHeight = 600;
        
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
        public const int WorkshopItemUnsubscribeButtonXOffset = WorkshopItemInfoButtonXOffset - 30;
        public const int RowButtonFieldYOffset = 3;

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
        public const string FilterSpriteRoads = "ToolbarIconRoads";
        public const string FilterSpriteResidential = "InfoIconOutsideConnectionsPressed";
        public const string FilterSpriteCommercial = "InfoIconOutsideConnectionsPressed";
        public const string FilterSpriteIndustrial = "InfoIconOutsideConnectionsPressed";
        public const string FilterSpriteOffice = "InfoIconOutsideConnectionsPressed";
        public const string FilterSpriteOther = "ToolbarIconHelp";
        public static Color FilterColorResidential = Color.green;
        public static Color FilterColorCommercial = new Color32(100, 100, 255, 255);
        public static Color FilterColorIndustrial = Color.yellow;
        public static Color FilterColorOffice = new Color32(0, 255, 255, 255);
        public const int FilterFirstOptionXOffset = 5;
        public const int FilterOptionYOffset = 7;
        public const int FilterCheckBoxSize = 12;
        public const int FilterCheckBoxYOffset = 10;
        public const int FilterIconSize = 30;
        public const int FilterCheckBoxIconDistance = 15;
        public const int FilterGroupedOptionsOffset = 20;
        public const int FilterSelectAllButtonXOffset = 5;
        public const int FilterSelectNoneButtonXOffset = 5;
        public const int FilterButtonYOffset = 7;
        public const string FilterButtonNormalSprite = "ButtonMenu";
        public const string FilterButtonHoveredSprite = "ButtonMenuHovered";
        public const float FilterButtonTextScale = 0.85f;
        public const int FilterButtenWidth = 50;
        public const int FilterButtenHeight = 30;
        public const string FilterPanelBackgroundSprite = "CursorInfoBack";

        // Filter option
        public const string CheckboxCheckedSprite = "AchievementCheckedTrue";
        public const string CheckboxUnCheckedSprite = "AchievementCheckedFalse";

        // WorkshopItem row
        public const int WorkshopItemRowWidth = DefaultPanelWidth - 17;
        public const int WorkshopItemRowHeight = 30;
        public const float WorkshopItemRowTextScale = 1f;
        public static Color32 WorkshopItemRowTextColor = new Color32(185, 221, 254, 255);
        public static Color32 WorkshopItemRowOddColor = new Color32(150, 150, 150, 255);
        public static Color32 WorkshopItemRowEvenColor = new Color32(120, 130, 130, 255);
        public const string WorkshopItemRowBackgroundSprite = "GenericPanelLight";

        // WorkshopItem Icon
        public const int WorkshopItemTypeIconSize = 25;

        // WorkshopItem Info Button
        public const string WorkshopItemInfoButtonNormalSprite = "CityInfo";
        public const string WorkshopItemInfoButtonPressedSprite = "CityInfoPressed";
        public const string WorkshopItemInfoButtonHoveredSprite = "CityInfoHovered";
        public const int WorkshopItemInfoButtonSize = 25;
        
        // WorkshopItemUnsubscribe Button
        public const string WorkshopItemUnsubscribeButtonNormalSprite = "buttonclose";
        public const string WorkshopItemUnsubscribeButtonPressedSprite = "buttonclosepressed";
        public const string WorkshopItemUnsubscribeButtonHoveredSprite = "buttonclosehover";
        public const int WorkshopItemUnsubscribeButtonSize = 25;
        
        public static string GetBuildingTypeSprite(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.Electricity:
                    return FilterSpriteElectricity;
                case BuildingType.WaterAndSewage:
                    return FilterSpriteWaterAndSewage;
                case BuildingType.Garbage:
                    return FilterSpriteGarbage;
                case BuildingType.Healthcare:
                    return FilterSpriteHealthcare;
                case BuildingType.FireDepartment:
                    return FilterSpriteFireDepartment;
                case BuildingType.Police:
                    return FilterSpritePolice;
                case BuildingType.Education:
                    return FilterSpriteEducation;
                case BuildingType.PublicTransport:
                    return FilterSpritePublicTransport;
                case BuildingType.Beautification:
                    return FilterSpriteBeautification;
                case BuildingType.Monuments:
                    return FilterSpriteMonuments;
                case BuildingType.Roads:
                    return FilterSpriteRoads;
                case BuildingType.Residential:
                    return FilterSpriteResidential;
                case BuildingType.Commercial:
                    return FilterSpriteCommercial;
                case BuildingType.Industrial:
                    return FilterSpriteIndustrial;
                case BuildingType.Office:
                    return FilterSpriteOffice;
                case BuildingType.Other:
                    return FilterSpriteOther;
                default: return string.Empty;
            }
        }

        public static Color32 GetBuildingTypeColor(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.Residential:
                    return FilterColorResidential;
                case BuildingType.Commercial:
                    return FilterColorCommercial;
                case BuildingType.Industrial:
                    return FilterColorIndustrial;
                case BuildingType.Office:
                    return FilterColorOffice;
                default:
                    return new Color32(255, 255, 255, 255);
            }
        }
    }
}