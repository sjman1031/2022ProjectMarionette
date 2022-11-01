using UnityEngine;

namespace BaseFrame
{
    //오브젝트가 생성이 안되는 메니져
    public class SingletonT<T> where T : class, new()
    {
        protected static T s_instance = null;

        //생성은 따로 시키고 Null판별 하도록 
        public static T Instance{ get{ return s_instance != null ? s_instance : SingletonT<T>.Init(); } }
        
        //생성보단 초기화라고 봐야겠지...?
        public static T Init()
        {
            if (s_instance == null)
            {
                s_instance = new T();
            }

            (s_instance as SingletonT<T>).onInit();

            return s_instance;
        }


        protected virtual void onInit()
        {
            var str = typeof(T).Name;
            Debug.Log("[" + str + "] onInit");
        }
    }
}
