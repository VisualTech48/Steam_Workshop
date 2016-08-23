		for(int x = 0; x < pCallback.m_unNumResultsReturned;x++){
			SteamUGCDetails_t Details;
			bool ret = SteamUGC.GetQueryUGCResult(SteamUGCTest.m_UGCQueryHandle, (uint)x, out Details);
			Debug.Log (Details.m_rgchTitle);
		}
