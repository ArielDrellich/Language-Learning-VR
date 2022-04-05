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

	private WordScramble parent;

	public CharObject Init (char c, WordScramble p) {
		character = c;
		text.text = c.ToString();
		gameObject.SetActive(true);
		parent = p;
		return this;
	}

	public void Start()
	{
	}

	public void Select ()
	{
		// Debug.Log("Selected: " + this.character);
		isSelected = !isSelected;

		image.color = isSelected ? selectedColor : normalColor;

		if (isSelected)
		{
			parent.Select(this);
			//WordScramble.main.Select(this);
		} else
		{
			parent.UnSelect();
			//WordScramble.main.UnSelect();
		}
	}


	


}
