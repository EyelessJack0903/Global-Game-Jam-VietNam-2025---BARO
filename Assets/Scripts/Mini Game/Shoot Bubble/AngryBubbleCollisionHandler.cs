using UnityEngine;

public class AngryBubbleCollisionHandler : BaseBubbleCollisionHandler
{
    protected override bool IsMatchingType(BubbleType.BubbleEmotion thisEmotion, BubbleType.BubbleEmotion otherEmotion)
    {
        return thisEmotion == BubbleType.BubbleEmotion.Angry && otherEmotion == BubbleType.BubbleEmotion.Angry;
    }
}
