using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFrame
{

    public class ResourceManager : SingletonT<ResourceManager>
    {
        //Dictionary<Path, Object>
        //임시 - 생각 더 해봐야 할듯
        private struct ResourcePath
        {
            public const string Character   = "Prefabs/Characters/{0}";
            public const string UI          = "Prefabs/UI/{0}";
            public const string Stage       = "Prefabs/Stages/{0}";
            public const string Normal      = "Prefabs/{0}";
            public const string Animation   = "Animations/{0}";
            public const string Table       = "Table/{0}";
        }

        //20.04.17 HT^^
        //실행되고 한번이라도 로드된 오브젝트들은 여기로 들어가진다.
        //그럴필요가 있을까 싶다.. 각 캐릭터들만이라도 모아둘까? 괜찮은 생각같은데...
        //일단 사용하지말고 더 살펴볼것
        //Dictionary<string, Object> m_dicObject = null;

        #region Table Func
        
        #endregion

        #region Static Func
        public static T Load<T>(string path) where T : Object
        {
            return s_instance != null ? s_instance.doLoad<T>(path) : null;
        }

        public static T Load<T>(eResourceType resourceType, string prefabName) where T : Object
        {
            return s_instance != null ? s_instance.doLoadToName<T>(resourceType, prefabName) : null;
        }

        public static List<T> LoadAll<T>(eResourceType resourceType) where T : Object
        {
            return s_instance != null ? s_instance.doLoadAll<T>(resourceType) : null;
        }

        //안쓰이는 코드
        //public static List<AnimationClip> LoadAnimations(string characterName)
        //{
        //    return s_instance != null ? s_instance.doLoadAnimations(characterName) : null;
        //}

        //public static void Clear()
        //{
        //    if (s_instance != null)
        //        s_instance.doClear();
        //}
        #endregion Static Func

        protected override void onInit()
        {
            base.onInit();

            //m_dicObject = new Dictionary<string, Object>();
        }
       

        //void doClear()
        //{
        //    m_dicObject.Clear();
        //}

        T doLoad<T>(string path) where T : Object
        {
            T objLoad = null;
            if (string.IsNullOrEmpty(path) != true)
            {
                //Object obj;
                //if (m_dicObject.TryGetValue(path, out obj))
                //{
                //    objLoad = obj as T;
                //}
                //else
                //{
                    objLoad = Resources.Load<T>(path);
                //  m_dicObject.Add(path, objLoad);
                //}
            }

            return objLoad;
        }

        //코드 개선 필요
        T doLoadToName<T>(eResourceType resourceType, string resourceName) where T : Object
        {
            string path;
            string type = resourceType.ToString();

            if (type.Equals(nameof(ResourcePath.Character)))
                path = ResourcePath.Character;
            else if (type.Equals(nameof(ResourcePath.UI)))
                path = ResourcePath.UI;
            else if (type.Equals(nameof(ResourcePath.Stage)))
                path = ResourcePath.Stage;
            else if (type.Equals(nameof(ResourcePath.Animation)))
                path = ResourcePath.Animation;
            else if (type.Equals(nameof(ResourcePath.Table)))
                path = ResourcePath.Table;
            else
                path = ResourcePath.Normal;

            path = string.Format(path, resourceName);

            return doLoad<T>(path);
        }

        //T doLoadTableData<T>() where T : TableData
        //{
        //    T tableData;

        //    return tableData;

        //}

        List<TableData> doLoadAllTableData()
        {

            List<TableData> lstData = new List<TableData>();

            return lstData;
        }


        List<T> doLoadAll<T>(eResourceType resourceType) where T : Object
        {
            string path;
            string type = resourceType.ToString();

            if (type.Equals(nameof(ResourcePath.Character)))
                path = ResourcePath.Character;
            else if (type.Equals(nameof(ResourcePath.UI)))
                path = ResourcePath.UI;
            else
            {
                Debug.Log("[Error Code] doLoadAll<T> ");
                return null;
            }
            
            var data = Resources.LoadAll<T>(path);
            List<T> lstData = new List<T>();

            for (int i = 0; i < data.Length; i++)
            {
                lstData.Add(data[i]);
            }

            return lstData;
        }

        //안쓰이는 코드
        //List<AnimationClip> doLoadAnimations(string characterName)
        //{
        //    List<AnimationClip> lstAnimation = new List<AnimationClip>();
        //    var animations = Resources.LoadAll(string.Format(ResourcePath.Animations,characterName));
        //    if (animations != null)
        //    {
        //        for (int i = 0; i < animations.Length; i++)
        //        {
        //            lstAnimation.Add((AnimationClip)animations[i]);
        //        }
        //    }
        //    else
        //        Debug.LogError("[Error]ResourceLoader.doLoadAnimationClips - Clip is null");

        //    return lstAnimation;
        //}
    }
}
