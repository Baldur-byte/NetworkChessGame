//@LeeTools
//------------------------
//Filename：PlayerComponent.cs
//Auther：Admin
//Device：NORAHCATHY
//Email：346679447@qq.com
//CreateDate：2023/09/17 14:17:15
//Function：Nothing
//------------------------
using UnityGameFramework.Runtime;

namespace ChessGame
{
    public class PlayerComponent : GameFrameworkComponent
    {
        public string Name
        {
            get;
            private set;
        }

        public string Id
        {
            get;
            private set;
        }

        public void Init()
        {

        }
    }
}