using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 

public static class Util {
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> _list, Action<T> _action)
    {
        foreach (T _item in _list)
        {
            _action(_item);
        }
        return _list;
    }
    public static IEnumerable<T> EnumList<T>()
    {
        return (T[])Enum.GetValues(typeof(T));
    }
    public static List<string>  Stringify<K,V>(this Dictionary<K,V> dict)
    {
        return dict.Select((kvp) => kvp.Key + ": " + kvp.Value).ToList(); 
    }
    public static List<KeyValuePair<K,V>> DictToList<K,V>(this Dictionary<K,V> dict)
    {
        List<KeyValuePair<K, V>> list = new List<KeyValuePair<K, V>>();
        dict.ForEach((kvp) =>
        {
            list.Add(new KeyValuePair<K, V>(kvp.Key, kvp.Value));
        });
        return list; 
    }
    public static bool HasCollider(this LayerMask mask, Collider coll)
    {
        return (mask.value & (1 << coll.gameObject.layer)) > 0; 
    }
}
