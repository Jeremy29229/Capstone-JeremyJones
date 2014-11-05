using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
public enum DialogMode { DynamiclyFitOptions, StaticFitMaxOptions };

/// <summary>
/// Unimplemented script -- to be added in future D_Quester version
/// </summary>
public class ConversationDisplayer : MonoBehaviour
{

	public float NPCDialogSizePercentage = 0.25f;
	public Vector2 NPCDialogPaddingPercentage = new Vector2(0.05f, 0.10f);
	public float NPCNameHeightPercentage = 0.2f;
	public float NPCDialgTextHeightPercentage = 0.9f;

	public Vector2 ButtonPaddingPercentage = new Vector2(0.2f, 0.05f);
	
	public DialogMode DisplayMode;
	public int MaxStaticDialogResponses = 4;
	public int CurrentDynamicDialogResponses = 1;

	private List<Button> Buttons;
	//private int buttonCount = 1;

	void Start()
	{
		Buttons = new List<Button>();
		//var dialogBackground = GetComponentsInChildren<Image>().First(x => x.gameObject.name == "Dialog Background");
		//dialogBackground.rectTransform.position = new Vector3(dialogBackground.rectTransform.position.x, -Screen.height / 2, dialogBackground.rectTransform.position.z);
		//dialogBackground.rectTransform.sizeDelta += new Vector2(0, Screen.height / 4);
		//dialogBackground.

	}

	void Update()
	{
		var dialogBackground = GetComponentsInChildren<Image>().First(x => x.gameObject.name == "Dialog Background");
		dialogBackground.rectTransform.position = new Vector3(dialogBackground.rectTransform.position.x, Screen.height * 0.25f, dialogBackground.rectTransform.position.z);
		dialogBackground.rectTransform.sizeDelta = new Vector2(dialogBackground.rectTransform.sizeDelta.x, Screen.height / 2);

		var npcDialogBackground = GetComponentsInChildren<Image>().First(x => x.gameObject.name == "NPCTextBackground");
		npcDialogBackground.rectTransform.position = new Vector3(Screen.width / 2, Screen.height * (0.5f - NPCDialogPaddingPercentage.y), npcDialogBackground.rectTransform.position.z);//
		npcDialogBackground.rectTransform.sizeDelta = new Vector2(Screen.width * (1 - (NPCDialogPaddingPercentage.x )), Screen.height * 0.5f * NPCDialogSizePercentage);

		var npcName =  GetComponentsInChildren<Text>().First(x => x.gameObject.name == "NPCName");
		npcName.rectTransform.sizeDelta = new Vector2(Screen.width * (1 - (NPCDialogPaddingPercentage.x)), (Screen.height * 0.5f * NPCDialogSizePercentage) * NPCNameHeightPercentage);
		npcName.rectTransform.position = new Vector3(Screen.width / 2, (Screen.height * (0.5f - (NPCDialogPaddingPercentage.y * (1.5f - 1.0f + NPCNameHeightPercentage)))) + Screen.height * 0.0169f, npcDialogBackground.rectTransform.position.z);

		var npcText =  GetComponentsInChildren<Text>().First(x => x.gameObject.name == "NPCText");
		npcText.rectTransform.sizeDelta = new Vector2(Screen.width * (1 - (NPCDialogPaddingPercentage.x)), (Screen.height * 0.5f * NPCDialogSizePercentage) * NPCDialgTextHeightPercentage);
		npcText.rectTransform.position = new Vector3(Screen.width / 2, (Screen.height * (0.5f - (NPCDialogPaddingPercentage.y * (1.5f - 1.0f + NPCDialgTextHeightPercentage)))) + Screen.height * 0.0338f, npcDialogBackground.rectTransform.position.z);

		var dialogButton = GetComponentsInChildren<Image>().First(x => x.gameObject.name == "DialogButton");
		dialogButton.rectTransform.position = new Vector3(Screen.width / 2, Screen.height * (0.5f - NPCDialogSizePercentage - ButtonPaddingPercentage.y), dialogButton.rectTransform.position.z);
		dialogButton.rectTransform.sizeDelta = new Vector2(Screen.width * (1 - (ButtonPaddingPercentage.x )), Screen.height * 0.5f * ButtonPaddingPercentage.y);

	}
}
