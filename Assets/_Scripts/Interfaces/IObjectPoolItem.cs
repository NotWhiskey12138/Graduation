using System;
using UnityEngine;
using Whiskey.ObjectPoolSystem;

namespace Whiskey.Interfaces
{
    public interface IObjectPoolItem
    {
        void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component;

        void Release();
    }
}