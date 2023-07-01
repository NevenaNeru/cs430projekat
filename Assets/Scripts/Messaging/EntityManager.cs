using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    static Hashtable allEntities = new Hashtable();

    private EntityManager() { }

    public static void RegisterEntity(int key, object entity)
    {
        EntityManager.allEntities.Add(key, entity);
    }

    public static GameObject getEntityGameObject(int key)
    {
        MonoBehaviour mb = (MonoBehaviour)EntityManager.getEntity(key);
        if (mb == null)
        {
            return null;
        }
        return mb.gameObject;
    }

    public static object getEntity(int key)
    {
        if (!EntityManager.allEntities.ContainsKey(key))
        {
            return null;
        }
        return (object)EntityManager.allEntities[key];
    }

    public static void removeEntity(int key)
    {
        EntityManager.allEntities.Remove(key);
    }

    public static void removeAllEntity()
    {
        EntityManager.allEntities.Clear();
    }
}
