	public void AddItem(){
		GMWare.Steamworks.Workshop.SteamWorkshopItem item = new GMWare.Steamworks.Workshop.SteamWorkshopItem{
			Title 				= "Test Item",
			Description 		= "Test Desc",
			Metadata			= "This is the test metadata.",
			Visibility			= ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate,
			UpdateChangeNotes 	= "New"
			
		};
		GMWare.Steamworks.Workshop.SteamWorkshopManager.CreateFromWorkshopItem (item, EWorkshopFileType.k_EWorkshopFileTypeCommunity);
	}
