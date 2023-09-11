//@LeeTools
//------------------------
//Filename：UIFormTemplate.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 17:28:26
//Function：Nothing
//------------------------

using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class UIFormTemplate : GameFrameworkComponent
    {
        [SerializeField]
        public UpdateResourceForm m_UpdateResourceForm = null;

        [SerializeField]
        public GameObject m_DialogForm = null;

        [SerializeField]
        public GameObject m_MessageForm = null;

        public UpdateResourceForm UpdateResourceFormTemplate
        {
            get => m_UpdateResourceForm;
        }

        public GameObject DialogFormTemplate
        {
            get => m_DialogForm;
        }

        public GameObject MessageFormTemplate
        {
            get => m_MessageForm;
        }
    }
}