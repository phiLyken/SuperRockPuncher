using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class ViewList_Anchored <Item, View> : ViewList<Item, View> where View : MonoBehaviour
{

    List<Transform> Anchors;

    Dictionary<Transform, Item> Transform_Map;

    public ViewList_Anchored<Item, View> Init(ViewBuilderDelegate view_builder, List<Item> list, List<Transform> _anchors, Action<View> RemoveCallback, int count)
    {
        Anchors = _anchors;
        Transform_Map = new Dictionary<Transform, Item>();
        base.Init(view_builder, GetTransform, list, RemoveCallback, count);
        return this;
    }

    public ViewList_Anchored<Item, View> Init(ViewBuilderDelegate view_builder,  List<Transform> _anchors, Action<View> RemoveCallback, int count)
    {

        Anchors = _anchors;
        Transform_Map = new Dictionary<Transform, Item>();
        base.Init(view_builder, GetTransform, RemoveCallback,count);
        return this;

    }
    
    public override Dictionary<Item, View> UpdateList(List<Item> items)
    {
        
        return base.UpdateList(items.GetRange(0, Mathf.Min( Anchors.Count, items == null ? 0 : items.Count)));
    }

    protected override void OnUpdated(Dictionary<Item, View> OnUpdate)
    {

        List<Transform> toRemove = new List<Transform>();

        foreach(var kvp in Transform_Map)
        {
            Item item = kvp.Value;
            if(item != null && !views.ContainsKey(item))
            {
                toRemove.Add(kvp.Key);
            }
        }

        toRemove.ForEach(tr => Transform_Map.Remove(tr));
    }

    protected Transform GetTransform(Item item)
    {
        if (!Transform_Map.ContainsValue(item))
        {
            Transform Free = GetFreeTransform(Transform_Map, Anchors);
            if(Free != null)
            {
                Transform_Map.Add(Free, item);
                return Free;
            }
        }

        Debug.Log("no free transform found");
        return null;
    }

    protected Transform GetFreeTransform(Dictionary<Transform, Item> map, List<Transform> transforms)
    {
        return transforms.FirstOrDefault(transform => !map.ContainsKey(transform));
    }
    
}