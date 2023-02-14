using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class WeightAdjustor : StateMachineBehaviour
{
    public static readonly int RunHashed = Animator.StringToHash("Run");
    public int layerIndexLeg = 2;
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex, controller);
        
        if (animator.GetBool(RunHashed))
        {
            controller.SetLayerWeight(layerIndex, layerIndexLeg == layerIndex ? 1 : 0);
        }
        else
        {
            controller.SetLayerWeight(layerIndex, layerIndexLeg == layerIndex ? 0 : 1);
        }
    }
}
