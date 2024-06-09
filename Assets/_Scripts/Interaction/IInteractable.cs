using UnityEngine;

namespace Whiskey.Interaction
{
    public interface IInteractable
    {
        void EnableInteraction(); //启用与对象的交互

        void DisableInteraction(); //禁用与对象的交互

        Vector3 GetPosition(); //获取对象的位置

        void Interact(); //执行与对象的交互操作
    }

    public interface IInteractable<T> : IInteractable
    {
        T GetContext();

        void SetContext(T context);
    }
}