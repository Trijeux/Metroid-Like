// Script by : Nanatchy
// Project : Metroid Like

using System.Collections.Generic;
using UnityEngine;

namespace Script.My_Tool_Script
{
    public static class ToolScriptUnity
    { 
        public static T MyGetComponentObject<T>(this Transform parent) where T : Component
        {
            var componentParent = parent.GetComponent<T>();
            if (componentParent != null)
            {
                return componentParent;
            }
            for (var i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                var componentChild = child.GetComponent<T>();
                if (componentChild != null)
                {
                    return componentChild;
                }
            }
            return null;
        }
        
        public static List<T> MyGetComponentsObject<T>(this Transform parent) where T : Component
        {
            var list = new List<T>();
            
            var componentParent = parent.GetComponent<T>();
            if (componentParent != null)
            {
                list.Add(componentParent);
            }
            for (var i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                var componentChild = child.GetComponent<T>();
                if (componentChild != null)
                {
                    list.Add(componentChild);
                }
            }
            return list;
        }
    }
}
