using UnityEngine;

public class FindAnimationClip : MonoBehaviour 
{ 
   public AnimationClip FindAnimation (Animator animator, string name) 
   {  
      foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
      {
         if (clip.name == name)
         {
            return clip;
         }           
      }

      return null;
   }
}