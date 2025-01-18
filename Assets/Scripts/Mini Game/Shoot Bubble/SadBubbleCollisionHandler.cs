using UnityEngine;

public class SadBubbleCollisionHandler : BaseBubbleCollisionHandler
{
    protected override bool IsMatchingType(BubbleType.BubbleEmotion thisEmotion, BubbleType.BubbleEmotion otherEmotion)
    {
        return thisEmotion == BubbleType.BubbleEmotion.Sad && otherEmotion == BubbleType.BubbleEmotion.Sad;
    }
}
