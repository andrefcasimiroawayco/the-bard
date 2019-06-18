using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IKBone {
    CHEST,
    LEFT_ARM,
    RIGHT_ARM,
}

public class IKHandler
{
    private Animator animator;
    private IKBone bone;

    public IKHandler(Animator animator, IKBone bone)
    {
        this.animator = animator;
        this.bone = bone;
    }

    public void LookAt(Quaternion target, Vector3 offset)
    {
        Transform boneTransform = GetBoneTransform(bone);

        boneTransform.rotation = target * Quaternion.Euler(offset);
    }

    Transform GetBoneTransform(IKBone bone)
    {
        switch (bone) {
            case IKBone.LEFT_ARM:
                return animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
            case IKBone.RIGHT_ARM:
                return animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
            default:
                return null;
        }
    }
}
