//using ColossalFramework.UI;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace CSAssetUsage
//{
//    public class UICustomCheckbox : UISprite
//    {
//        private bool _isChecked;

//        public bool Checked
//        {
//            get { return _isChecked; }
//            set
//            {
//                _isChecked = value;
//                SetSprite();
//            }
//        }

//        public override void Awake()
//        {
//            base.Awake();
//            SetSprite();
//        }

//        private void SetSprite()
//        {
//            spriteName = _isChecked ? "AchievementCheckedTrue" : "AchievementCheckedFalse";
//        }
//    }
//}
