using UnityEngine;

namespace BaseFrame
{
    public class MonoSingletonObject<T> : MonoBehaviour where T : MonoBehaviour, new()
    {
        protected static T s_instance = null;
        public static T Instance
        {
            get
            {
                //������ �ȵǾ� �ִٸ� ���� �� Instance ��������
                if (s_instance == null)
                {
                    GameObject obj = new GameObject();

                    obj.name = typeof(T).Name;
                    s_instance = obj.AddComponent<T>();

                    Debug.Log("Create {0}", obj);
                }

                return s_instance;
            }
        }

        #region MonoBehaviour
        protected virtual void Awake()
        {
            var objs = FindObjectsOfType<T>();
            if (objs.Length != 1)
            {
                Destroy(gameObject);
            }

        }

        protected virtual void OnDestroy()
        {
            s_instance = null;
        }

        protected virtual void OnApplicationQuit()
        {
            s_instance = null;
        }
        #endregion MonoBehaviour
    }
}
