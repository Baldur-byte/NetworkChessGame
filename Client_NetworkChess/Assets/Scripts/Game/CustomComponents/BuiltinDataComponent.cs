using GameFramework;
using UnityGameFramework.Runtime;

namespace Game
{
    public class BuiltinDataComponent : GameFrameworkComponent
    {
        private BuildInfo m_buildInfo = null;
        public BuildInfo BuildInfo
        {
            get
            {
                return m_buildInfo;
            }
        }

        public void InitBuildInfo()
        {
            m_buildInfo = new BuildInfo();
            m_buildInfo.GameVersion = "1.0.0";
            m_buildInfo.InternalGameVersion = 1;
            m_buildInfo.CheckVersionUrl = "http://localhost:8080/CheckVersion";
            m_buildInfo.WindowsAppUrl = "http://localhost:8080/WindowsApp";
            m_buildInfo.MacOSAppUrl = "http://localhost:8080/MacOSApp";
            m_buildInfo.IOSAppUrl = "http://localhost:8080/IOSApp";
            m_buildInfo.AndroidAppUrl = "http://localhost:8080/AndroidApp";
        }

        public void InitDefaultDictionary()
        {

        }
    }
}