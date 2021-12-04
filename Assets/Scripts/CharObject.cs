using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharObject : MonoBehaviour
{
	public char character; //A,B,C..
	public TMPro.TMP_Text text;
	public RectTransform rectTransform;
	public int index;
	public Image image;

	bool isSelected = false;

	[Header("Appearance")]
	public Color normalColor;
	public Color selectedColor;

	public CharObject Init (char c) {
		character = c;
		text.text = c.ToString();
		gameObject.SetActive(true);
		return this;
	}


	public void Select ()
	{
		isSelected = !isSelected;

		image.color = isSelected ? selectedColor : normalColor;

		if (isSelected)
		{
			WordScramble.main.Select(this);
		} else
		{
			WordScramble.main.UnSelect();
		}
	}
}
