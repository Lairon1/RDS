using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IRDSSlotDetail : MonoBehaviour {

    [System.Serializable]
    public class IRDSSlotComponentDetail
    {
        public string name;
        public Graphic UIComponent;
        private Text myText;
        private Image myImage;

        /// <summary>
        /// Inititialization of the class, to get the text and image components of the slot
        /// </summary>
        public void Init()
        {
            myText = UIComponent.GetComponent<Text>();
            myImage =UIComponent.GetComponent<Image>();
        }

        /// <summary>
        /// Sets the text to the Text component if it was found
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            if (myText != null)
            {
                myText.text = text;
            }
        }

        /// <summary>
        /// Sets the sprite to the Image component if it was found
        /// </summary>
        /// <param name="sprite"></param>
        public void SetImage(Sprite sprite)
        {
            if (myImage != null)
            {
                myImage.sprite = sprite;
            }
        }

        /// <summary>
        /// Sets the texture to the Image Component if it was found
        /// </summary>
        /// <param name="texture"></param>
        public void SetImage(Texture texture)
        {
            if (myImage != null)
            {
                Sprite newSprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                myImage.sprite = newSprite;
            }
        }
    }

    public IRDSSlotComponentDetail[] components;

    public void InitComponents()
    {
        for (int i = 0; i < components.Length; i++)
        {
            components[i].Init();
        }
    }

    public IRDSSlotComponentDetail GetSlotComponent(string name)
    {
        for (int i = 0; i < components.Length;i++)
        {
            if (name.ToLower() == components[i].name.ToLower())
            {
                return components[i];
            }
        }
        return null;
    }
}
