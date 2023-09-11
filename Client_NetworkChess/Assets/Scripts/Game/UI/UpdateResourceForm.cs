//@LeeTools
//------------------------
//Filename：UpdateResourceForm.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 17:01:11
//Function：Nothing
//------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UpdateResourceForm : MonoBehaviour
    {
        [SerializeField]
        private Slider m_ProgressSlider = null;

        [SerializeField]
        private Text m_DescriptionText = null;

        public void SetProgress(float value, string description)
        {
            m_ProgressSlider.value = value;
            m_DescriptionText.text = description;
        }
    }
}