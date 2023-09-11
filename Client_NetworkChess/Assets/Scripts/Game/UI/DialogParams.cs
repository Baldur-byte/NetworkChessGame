//@LeeTools
//------------------------
//Filename：DialogParams.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 14:49:26
//Function：Nothing
//------------------------

using GameFramework;

namespace Game
{
    public class DialogParams
    {
        /// <summary>
        /// 按钮数量 取值 1，2，3
        /// </summary>
        public int Mode
        {
            get; 
            set; 
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get; 
            set; 
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Message
        {
            get; 
            set; 
        }

        /// <summary>
        /// 是否暂停游戏
        /// </summary>
        public bool PauseGame
        {
            get; 
            set; 
        }

        /// <summary>
        /// 确认按钮文本
        /// </summary>
        public string ConfirmText
        {
            get; 
            set; 
        }

        /// <summary>
        /// 确认按钮点击回调
        /// </summary>
        public GameFrameworkAction<object> OnClickConfirm
        {
            get; 
            set; 
        }

        /// <summary>
        /// 取消按钮文本
        /// </summary>
        public string CancelText
        {
            get; 
            set; 
        }

        /// <summary>
        /// 取消按钮点击回调
        /// </summary>
        public GameFrameworkAction<object> OnClickCancel
        {
            get; 
            set; 
        }

        /// <summary>
        /// 其他按钮文本
        /// </summary>
        public string OtherText
        {
            get; 
            set; 
        }

        /// <summary>
        /// 其他按钮点击回调
        /// </summary>
        public GameFrameworkAction<object> OnClickOther
        {
            get; 
            set; 
        }

        /// <summary>
        /// 用户自定义数据
        /// </summary>
        public object UserData
        {
            get; 
            set; 
        }
    }
}