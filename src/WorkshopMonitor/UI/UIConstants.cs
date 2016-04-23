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
        public const int MainWindowWidth = 1300;
        public const int MainWindowHeight = 600;
        
        // Defaults
        public const int DefaultPanelWidth = MainWindowWidth - 15;

        // Columns/rows
        public const int WorkshopAssetTypeLabelXOffset = 10;
        public const int WorkshopAssetTypeIconXOffset = 18;
        public const int WorkshopAssetTypeYOffset = 3;
        public const int WorkshopAssetWorkshopIdLabelXOffset = WorkshopAssetTypeLabelXOffset + 60;
        public const int WorkshopAssetNameLabelXOffset = WorkshopAssetWorkshopIdLabelXOffset + 100;
        public const int NumberUsedLabelXOffset = WorkshopAssetNameLabelXOffset + 500;
        public const int LabelYOffset = 6;
        public const int WorkshopAssetInfoButtonXOffset = MainWindowWidth - 60;
        public const int WorkshopAssetUnsubscribeButtonXOffset = WorkshopAssetInfoButtonXOffset - 30;
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
        public const string FilterSpriteParksnPlazas = "SubBarBeautificationParksnPlazas";
        public const string FilterSpriteMonuments = "ToolbarIconMonuments";
        public const string FilterSpriteRoads = "ToolbarIconRoads";
        public const string FilterSpriteResidential = "InfoIconOutsideConnectionsPressed";
        public const string FilterSpriteCommercial = "InfoIconOutsideConnectionsPressed";
        public const string FilterSpriteIndustrial = "InfoIconOutsideConnectionsPressed";
        public const string FilterSpriteOffice = "InfoIconOutsideConnectionsPressed";
        public const string FilterSpriteTrees = "ToolbarIconBeautification";
        public const string FilterSpriteProps = "ToolbarIconProps";
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

        // WorkshopAsset row
        public const int WorkshopAssetRowWidth = DefaultPanelWidth - 17;
        public const int WorkshopAssetRowHeight = 30;
        public const float WorkshopAssetRowTextScale = 1f;
        public static Color32 WorkshopAssetRowTextColor = new Color32(185, 221, 254, 255);
        public static Color32 WorkshopAssetRowOddColor = new Color32(150, 150, 150, 255);
        public static Color32 WorkshopAssetRowEvenColor = new Color32(120, 130, 130, 255);
        public const string WorkshopAssetRowBackgroundSprite = "GenericPanelLight";

        // WorkshopAsset Icon
        public const int WorkshopAssetTypeIconSize = 25;

        // WorkshopAsset Info Button
        public const string WorkshopAssetInfoButtonNormalSprite = "CityInfo";
        public const string WorkshopAssetInfoButtonPressedSprite = "CityInfoPressed";
        public const string WorkshopAssetInfoButtonHoveredSprite = "CityInfoHovered";
        public const int WorkshopAssetInfoButtonSize = 25;
        
        // WorkshopAssetUnsubscribe Button
        public const string WorkshopAssetUnsubscribeButtonNormalSprite = "buttonclose";
        public const string WorkshopAssetUnsubscribeButtonPressedSprite = "buttonclosepressed";
        public const string WorkshopAssetUnsubscribeButtonHoveredSprite = "buttonclosehover";
        public const int WorkshopAssetUnsubscribeButtonSize = 25;
        
        public static string GetAssetTypeSprite(AssetType assetType)
        {
            switch (assetType)
            {
                case AssetType.Electricity:
                    return FilterSpriteElectricity;
                case AssetType.WaterAndSewage:
                    return FilterSpriteWaterAndSewage;
                case AssetType.Garbage:
                    return FilterSpriteGarbage;
                case AssetType.Healthcare:
                    return FilterSpriteHealthcare;
                case AssetType.FireDepartment:
                    return FilterSpriteFireDepartment;
                case AssetType.Police:
                    return FilterSpritePolice;
                case AssetType.Education:
                    return FilterSpriteEducation;
                case AssetType.PublicTransport:
                    return FilterSpritePublicTransport;
                case AssetType.Beautification:
                    return FilterSpriteParksnPlazas;
                case AssetType.Monuments:
                    return FilterSpriteMonuments;
                case AssetType.Roads:
                    return FilterSpriteRoads;
                case AssetType.Residential:
                    return FilterSpriteResidential;
                case AssetType.Commercial:
                    return FilterSpriteCommercial;
                case AssetType.Industrial:
                    return FilterSpriteIndustrial;
                case AssetType.Office:
                    return FilterSpriteOffice;
                case AssetType.Prop:
                    return FilterSpriteProps;
                case AssetType.Tree:
                    return FilterSpriteTrees;
                case AssetType.Other:
                    return FilterSpriteOther;
                default: return string.Empty;
            }
        }

        public static Color32 GetAssetTypeColor(AssetType AssetType)
        {
            switch (AssetType)
            {
                case AssetType.Residential:
                    return FilterColorResidential;
                case AssetType.Commercial:
                    return FilterColorCommercial;
                case AssetType.Industrial:
                    return FilterColorIndustrial;
                case AssetType.Office:
                    return FilterColorOffice;
                default:
                    return new Color32(255, 255, 255, 255);
            }
        }
    }
}