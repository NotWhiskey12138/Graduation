using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;

namespace MyGameEntry
{
    public class ProcedureLaunch : ProcedureBase
    {
        public override bool UseNativeDialog
        {
            get
            {
                return true;
            }
        }
 
        protected override void OnUpdate(IFsm<GameFramework.Procedure.IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            //一帧后直接转到菜单流程
           // ChangeState<ProcedureMenu>(procedureOwner);
        }
    }
                  
}

