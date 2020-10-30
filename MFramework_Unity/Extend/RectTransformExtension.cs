using UnityEngine;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// RectTransformExtension
    /// </summary>
    public static partial class RectTransformExtension
    {
        /// <summary>
        /// GetPosInRootTrans
        /// </summary>
        /// <param name="self"></param>
        /// <param name="rootTrans"></param>
        /// <returns></returns>
		public static Vector2 GetPosInRootTrans(this RectTransform self, Transform rootTrans)
		{
			return RectTransformUtility.CalculateRelativeRectTransformBounds(rootTrans, self).center;
		}

        /// <summary>
        /// AnchorPosX
        /// </summary>
        /// <param name="self"></param>
        /// <param name="anchorPosX"></param>
        /// <returns></returns>
		public static RectTransform AnchorPosX(this RectTransform self, float anchorPosX)
		{
			var anchorPos = self.anchoredPosition;
			anchorPos.x = anchorPosX;
            self.anchoredPosition = anchorPos;
			return self;
		}

        /// <summary>
        /// SetSizeWidth
        /// </summary>
        /// <param name="self"></param>
        /// <param name="sizeWidth"></param>
        /// <returns></returns>
        public static RectTransform SetSizeWidth(this RectTransform self, float sizeWidth)
		{
			var sizeDelta = self.sizeDelta;
			sizeDelta.x = sizeWidth;
            self.sizeDelta = sizeDelta;
			return self;
		}

        /// <summary>
        /// SetSizeHeight
        /// </summary>
        /// <param name="self"></param>
        /// <param name="sizeHeight"></param>
        /// <returns></returns>
		public static RectTransform SetSizeHeight(this RectTransform self, float sizeHeight)
		{
			var sizeDelta = self.sizeDelta;
			sizeDelta.y = sizeHeight;
            self.sizeDelta = sizeDelta;
			return self;
		}

        /// <summary>
        /// GetWorldSize
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Vector2 GetWorldSize(this RectTransform self)
	    {
		    return RectTransformUtility.CalculateRelativeRectTransformBounds(self).size;
	    }
	}
}