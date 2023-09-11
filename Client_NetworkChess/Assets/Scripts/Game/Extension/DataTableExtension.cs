//@LeeTools
//------------------------
//Filename：DataTableExtension.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/11 20:23:34
//Function：Nothing
//------------------------

using GameFramework;
using GameFramework.DataTable;
using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public static class DataTableExtension
    {
        public static void LoadDataTable(this DataTableComponent dataTableComponent, string dataTableName, string dataTableAssetName, object userData = null)
        {
            if (string.IsNullOrEmpty(dataTableName))
            {
                Log.Warning("Data table name is invalid.");
                return;
            }

            string[] splitedNames = dataTableName.Split('_');
            if (splitedNames.Length > 2)
            {
                Log.Warning("Data table name '{0}' is invalid.", dataTableName);
                return;
            }

            string dataRowClassName = splitedNames.Length > 1 ? Utility.Text.Format("{0}.{1}", splitedNames[0], splitedNames[1]) : dataTableName;

            Type dataRowType = Type.GetType(dataRowClassName);
            if (dataRowType == null)
            {
                Log.Warning("Can not get data row type '{0}'.", dataRowClassName);
                return;
            }

            DataTableBase dataTable = dataTableComponent.CreateDataTable(dataRowType);

            dataTable.ReadData(dataTableAssetName, Constant.AssetPriority.DataTableAsset, userData);
        }

        public static Color32 ParserColor32(string value)
        {
            string[] split = value.Split(',');
            if (split.Length != 4)
            {
                Log.Warning("Can not parse Color32 '{0}'.", value);
                return new Color32();
            }
            try
            {
                byte r = byte.Parse(split[0]);
                byte g = byte.Parse(split[1]);
                byte b = byte.Parse(split[2]);
                byte a = byte.Parse(split[3]);
                return new Color32(r, g, b, a);
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse Color32 '{0}' with exception '{1}'.", value, exception.ToString());
                return new Color32();
            }
        }

        public static Color ParserColor(string value)
        {
            string[] split = value.Split(',');
            if (split.Length != 4)
            {
                Log.Warning("Can not parse Color '{0}'.", value);
                return new Color();
            }
            try
            {
                float r = float.Parse(split[0]);
                float g = float.Parse(split[1]);
                float b = float.Parse(split[2]);
                float a = float.Parse(split[3]);
                return new Color(r, g, b, a);
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse Color '{0}' with exception '{1}'.", value, exception.ToString());
                return new Color();
            }
        }

        public static Vector2 ParserVector2(string value)
        {
            string[] split = value.Split(',');
            if (split.Length != 2)
            {
                Log.Warning("Can not parse Vector2 '{0}'.", value);
                return Vector2.zero;
            }
            try
            {
                float x = float.Parse(split[0]);
                float y = float.Parse(split[1]);
                return new Vector2(x, y);
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse Vector2 '{0}' with exception '{1}'.", value, exception.ToString());
                return Vector2.zero;
            }
        }

        public static Vector3 ParserVector3(string value)
        {
            string[] split = value.Split(',');
            if (split.Length != 3)
            {
                Log.Warning("Can not parse Vector3 '{0}'.", value);
                return Vector3.zero;
            }
            try
            {
                float x = float.Parse(split[0]);
                float y = float.Parse(split[1]);
                float z = float.Parse(split[2]);
                return new Vector3(x, y, z);
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse Vector3 '{0}' with exception '{1}'.", value, exception.ToString());
                return Vector3.zero;
            }
        }

        public static Vector4 ParserVector4(string value)
        {
            string[] split = value.Split(',');
            if (split.Length != 4)
            {
                Log.Warning("Can not parse Vector4 '{0}'.", value);
                return Vector4.zero;
            }
            try
            {
                float x = float.Parse(split[0]);
                float y = float.Parse(split[1]);
                float z = float.Parse(split[2]);
                float w = float.Parse(split[3]);
                return new Vector4(x, y, z, w);
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse Vector4 '{0}' with exception '{1}'.", value, exception.ToString());
                return Vector4.zero;
            }
        }

        public static Quaternion ParserQuaternion(string value)
        {
            string[] split = value.Split(',');
            if (split.Length != 4)
            {
                Log.Warning("Can not parse Quaternion '{0}'.", value);
                return Quaternion.identity;
            }
            try
            {
                float x = float.Parse(split[0]);
                float y = float.Parse(split[1]);
                float z = float.Parse(split[2]);
                float w = float.Parse(split[3]);
                return new Quaternion(x, y, z, w);
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse Quaternion '{0}' with exception '{1}'.", value, exception.ToString());
                return Quaternion.identity;
            }
        }

        public static Rect ParserRect(string value)
        {
            string[] split = value.Split(',');
            if (split.Length != 4)
            {
                Log.Warning("Can not parse Rect '{0}'.", value);
                return new Rect();
            }
            try
            {
                float x = float.Parse(split[0]);
                float y = float.Parse(split[1]);
                float width = float.Parse(split[2]);
                float height = float.Parse(split[3]);
                return new Rect(x, y, width, height);
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse Rect '{0}' with exception '{1}'.", value, exception.ToString());
                return new Rect();
            }
        }

        public static RectOffset ParserRectOffset(string value)
        {
            string[] split = value.Split(',');
            if (split.Length != 4)
            {
                Log.Warning("Can not parse RectOffset '{0}'.", value);
                return new RectOffset();
            }
            try
            {
                int left = int.Parse(split[0]);
                int right = int.Parse(split[1]);
                int top = int.Parse(split[2]);
                int bottom = int.Parse(split[3]);
                return new RectOffset(left, right, top, bottom);
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse RectOffset '{0}' with exception '{1}'.", value, exception.ToString());
                return new RectOffset();
            }
        }
    }
}