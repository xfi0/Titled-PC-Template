using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Titled_PC_Template.Extensions
{
    public static class VRRigExtensions
    {
        public static bool IsTagged(this VRRig rig)
        {
            if (rig == null)
                return false;

            foreach (var gtm in GameObject.FindObjectsOfType<GorillaTagManager>())
            {
                if (gtm != null && gtm.currentInfected.Contains(rig.photonView.Owner))
                    return true;
            }

            return false;
        }
    }
}
