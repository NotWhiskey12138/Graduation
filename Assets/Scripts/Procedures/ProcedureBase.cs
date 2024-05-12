using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameEntry
{
    public abstract class ProcedureBase : GameFramework.Procedure.ProcedureBase
    {
        /// <summary>
        /// 流程抽象类
        /// </summary>
        public abstract bool UseNativeDialog
        {
            get;
        }
    }
}
