using UnityEngine;

public class FearBubbleCollisionHandler : BaseBubbleCollisionHandler
{
    protected override bool IsMatchingType(BubbleType.BubbleEmotion thisEmotion, BubbleType.BubbleEmotion otherEmotion)
    {
        return thisEmotion == BubbleType.BubbleEmotion.Fear && otherEmotion == BubbleType.BubbleEmotion.Fear;
    }
}
