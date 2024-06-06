using System;
using Whiskey.ObjectPoolSystem;
using UnityEngine;

namespace Whiskey.Interfaces
{
    public interface IObjectPoolItem
    {
        void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component;
            
        void Release();
    }
}