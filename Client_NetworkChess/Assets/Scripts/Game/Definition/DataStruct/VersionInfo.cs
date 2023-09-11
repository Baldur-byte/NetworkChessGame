//@LeeTools
//------------------------
//Filename：VersionInfo.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 12:08:52
//Function：Nothing
//------------------------
namespace Game
{
    public class VersionInfo 
    {
        /// <summary>
        /// 是否强制更新游戏
        /// </summary>
        public bool IsForceUpdateGame
        {
            get; set;
        }

        /// <summary>
        /// 最新的游戏版本号
        /// </summary>
        public string LastestGameVersion
        {
            get; set;
        }

        /// <summary>
        /// 最新的游戏内部版本号
        /// </summary>
        public int InternalGameVersion
        {
            get; set;
        }

        /// <summary>
        /// 最新的资源内部版本号
        /// </summary>
        public int InternalResourceVersion
        {
            get; set;
        }

        /// <summary>
        /// 资源更新地址
        /// </summary>
        public string UpdatePrefixUri
        {
            get; set;
        }

        /// <summary>
        /// 资源更新列表长度
        /// </summary>
        public int VersionListLength
        {
            get; set;
        }

        /// <summary>
        /// 资源更新列表哈希值
        /// </summary>
        public int VersionListHashCode
        {
            get; set;
        }

        /// <summary>
        /// 资源更新列表压缩后长度
        /// </summary>
        public int VersionListCompressedLength
        {
            get; set;
        }

        /// <summary>
        /// 资源更新列表压缩后哈希值
        /// </summary>
        public int VersionListCompressedHashCode
        {
            get; set;
        }
    }
}