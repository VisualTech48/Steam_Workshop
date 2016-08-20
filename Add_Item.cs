using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using GMWare.Steamworks.Workshop;
using UnityEngine.UI;

public class Workshop_Man : MonoBehaviour {
	public InputField 	Title_E;
	public InputField 	Description_E;
	public Toggle		Private;
	public InputField   FilePath;
	public InputField   HeaderPath;
//	public InputField 	ChangeLog;
	public Button 		SubmitItem;
	public Text			UpdatingM;
public void Awake(){
		if (SteamManager.Initialized) {
			Debug.LogWarning ("Loaded SteamManager!");
			ChangeState("{<color=red>SUCCESS</color>}::Steam loaded");
		} 
		else {
			ChangeState("{<color=red>ERROR</color>}::Steam not Initialized, please turn on steam, and restart the program!");
			SubmitItem.enabled 		= false;
			Title_E.enabled 		= false;
			Description_E.enabled	= false;
			Private.enabled			= false;
		}
		SteamWorkshopManager.Instance.ItemUpdated += HandleCustomEvent;
		SteamWorkshopManager.Instance.ItemCreated += HandleCreating;
		FilePath.text 	= Application.dataPath + "/Item_Data";
		HeaderPath.text = Application.dataPath + "/header.jpg";

	}
	// Use this for initialization
	public void AddItem(){
		UpdatingM.text = "";
		UpdatingM.text = "[" + System.DateTime.Now + "] " + "Status::{<color=yellow>PENDING</color>}";
		bool Proceed = true;
		if (Title_E.text == "") {
			ChangeState("{<color=red>ERROR</color>}::Title missing!");
			Proceed = false;
		}
		if (Description_E.text == "") {
			ChangeState("{<color=red>ERROR</color>}::Description missing!");
			Proceed = false;
		}
		if (FilePath.text == "") {
			ChangeState("{<color=red>ERROR</color>}::Files path missing!");
			Proceed = false;
		}
		if (HeaderPath.text == null) {
			ChangeState("{<color=red>ERROR</color>}::Header image path missing!");
			Proceed = false;
		}

		if (Proceed) {
			SubmitItem.enabled = false;
			Debug.LogError ("Adding item");
			ChangeState ("Creating {<color=orange>" + Title_E.text + "</color>}");
			ERemoteStoragePublishedFileVisibility Visable = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic;
			if (Private.isOn) {
				Visable = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate;
			}
			SteamWorkshopItem item = new SteamWorkshopItem{
			Title 				= Title_E.text,
			Description 		= Description_E.text,
			Metadata			= "This is the test metadata.",
			UpdateLanguage		= "english",
			Visibility			= Visable,
			Tags 				= new List<string> { "Skin" },
			KeyValues			= new Dictionary<string, string> { { "TestKey", "TestValue" } },
			UpdateContentPath	= FilePath.text,
			UpdatePreviewPath	= HeaderPath.text
			
		};
			SteamWorkshopManager.Instance.CreateFromWorkshopItem (item, EWorkshopFileType.k_EWorkshopFileTypeCommunity);
		}
	}
	void HandleCustomEvent(object sender, GMWare.Steamworks.Workshop.SteamWorkshopManager.ItemUpdatedEventArgs e)
	{
		Debug.LogError ("Item has been uploaded");
		ChangeState("{<color=green>SUCCESS</color>}::Item created and updated!");
	}
	void ChangeState(string NewString){
		if (UpdatingM.text != "") {
			UpdatingM.text += "\n";
		}
		UpdatingM.text += "["+ System.DateTime.Now+ "] " + NewString;
	}
	void HandleCreating(object sender, GMWare.Steamworks.Workshop.SteamWorkshopManager.ItemCreatedEventArgs e)
	{
		Debug.LogError ("Item created!");
		ChangeState("{<color=yellow>Updating</color>}::Item...");
	}
//	public void Create () {
//		SteamUGCTest.GetUGC ();
//	}
//	public void SetItem(){
//		SteamUGCTest.SetItem ();
//	}
//	public void UpdateItem(){
//		SteamUGCTest.CreateItem ();
//	}
	// Update is called once per frame
	void Update () {
		
	}
}
