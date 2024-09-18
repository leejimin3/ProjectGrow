using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface I_Pool
{
    void SetPoolEvent(Action<I_Pool> poolevent);

    void SetDisableOneEvent(Action<I_Pool> disableEvent);
}

public class ObjectPooling<T> where T : Component, I_Pool
{
    Transform Parent;
    Stack<T> itemPool = new Stack<T>();  // Stack�� ����Ͽ� LIFO ������ ����

    public void Initialize(T item, Transform Parent, int Count)
    {
        this.Parent = Parent;
        for (int i = 0; i < Count; i++)
        {
            itemPool.Push(CreateNewObject(item));  // Stack�� Push
        }
    }

    private T CreateNewObject(T obj)
    {
        var newObj = GameObject.Instantiate(obj);
        newObj.GetComponent<I_Pool>().SetPoolEvent((item) =>
        {
            itemPool.Push(newObj);  // Stack�� Push
            newObj.gameObject.SetActive(false);
            newObj.gameObject.transform.SetParent(null);
        });
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(Parent);
        return newObj;
    }

    public T GetObject(T prefab)
    {
        T newObj = null;
        if (itemPool.Count > 0)
        {
            newObj = itemPool.Pop();  // Stack���� Pop
        }
        else
        {
            newObj = CreateNewObject(prefab);
        }

        newObj.gameObject.SetActive(true);
        return newObj;
    }
}