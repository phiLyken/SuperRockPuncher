using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public static class M_Weightables {

   
    public class WeightAble <T> : IWeightable
    {
        T weightable;
        float weight;

        public WeightAble(T obj, float _weight)
        {
            weightable = obj;
            weight = _weight;
        }

        public float Weight
        {
            get
            {
                return weight;
            }

            set
            {
                
            }
        }
    }
    public static IWeightable GetWeighted(List<IWeightable> WeightableObjects) {

        float totalChance = 0;
        foreach (IWeightable w in WeightableObjects) {
            totalChance += w.Weight;
        }

        float r = UnityEngine.Random.value * totalChance;
        float last = 0;

        for (int i = 0; i < WeightableObjects.Count; i++) {
            if (r >= last && r <= last + WeightableObjects[i].Weight) {
                return WeightableObjects[i];
            }
            last += WeightableObjects[i].Weight;
        }

        return null;
    }

 

    public static T GetObjectByweight<T>(this List<T> weighted)
    {
        return GetWeighted(weighted);
    }
    public static T GetWeighted<T>(List<T> weighted)
    {
      
        return  (T) GetWeighted(weighted.Cast<IWeightable>().ToList()) ;
    }

    public static List<IWeightable> GetWeighted(List<IWeightable> WeightableObjects, int count){

        List<IWeightable> items = new List<IWeightable>();

        for(int i = 0; i < count && i < WeightableObjects.Count; i++)
        {
            IWeightable item = GetWeighted(WeightableObjects);
            items.Add(item);
            WeightableObjects.Remove(item);
        }

        return items;
    }

    public static void UnitTest(List<IWeightable> WeightableObjects){
		Debug.Log("test: "+WeightableObjects.Count);
		List<IWeightable> Result = new List<IWeightable>();

		for(int i = 0; i< 100; i++) Result.Add(GetWeighted(WeightableObjects ));
	
		int count = 0;
		for(int i = 0; i < WeightableObjects.Count; i++){
			//	Debug.Log( (WeightableObjects[i]).WeightableID+" checking");
			foreach(IWeightable w in Result)if(WeightableObjects[i] == w) count++;
			//Debug.Log( (WeightableObjects[i]).WeightableID+" : "+count);
			count = 0;
		}
	}

}
