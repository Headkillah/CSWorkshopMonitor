using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CSAssetUsage
{
    public class UIConstants
    {
        // Defaults
        public const int DefaultPanelWidth = MainWindowWidth - 15;

        // Columns
        public const int TextFieldRowPosition = 6;
        public const int ButtonFieldRowPosition = 3;
        public const int AssetNameColumnPosition = 10;
        public const int NumberUsedColumnPosition = AssetNameColumnPosition + 500;
        public const int AssetInfoButtonPosition = MainWindowWidth - 60;
        public const string AssetInfoButtonNormalSprite = "CityInfo";
        public const string AssetInfoButtonPressedSprite = "CityInfoPressed";
        public const string AssetInfoButtonHoveredSprite = "CityInfoHovered";
        public const int AssetInfoButtonSize = 25;

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

        // Asset row
        public const int AssetRowWidth = DefaultPanelWidth - 17;
        public const int AssetRowHeight = 30;
        public const float AssetRowTextScale = 1f;
        public static Color32 AssetRowTextColor = new Color32(185, 221, 254, 255);
        public static Color32 AssetRowOddColor = new Color32(150, 150, 150, 255);
        public static Color32 AssetRowEvenColor = new Color32(120, 130, 130, 255);
        public const string AssetRowBackgroundSprite = "GenericPanelLight";
    }
}
