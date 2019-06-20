using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IKBone {
    HEAD,
    CHEST,
    LEFT_ARM,
    RIGHT_ARM,
}

public class IKHandler
{
    private Animator animator;
    private IKBone bone;
    private Quaternion lastLookDestinationAllowed;

    public IKHandler(Animator animator, IKBone bone)
    {
        this.animator = animator;
        this.bone = bone;
    }

    public void LookAt(Quaternion target, Vector3 offset, float xMaxAngle = 45f, float yMaxAngle = 45f)
    {
        Transform boneTransform = GetBoneTransform(bone);

        Quaternion lookDestination = target * Quaternion.Euler(offset);
        boneTransform.rotation = lookDestination;
    }


    Transform GetBoneTransform(IKBone bone)
    {
        switch (bone) {
            case IKBone.HEAD:
                return animator.GetBoneTransform(HumanBodyBones.Head);
            case IKBone.CHEST:
                return animator.GetBoneTransform(HumanBodyBones.Chest);
            case IKBone.LEFT_ARM:
                return animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
            case IKBone.RIGHT_ARM:
                return animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
            default:
                return null;
        }
    }
}
