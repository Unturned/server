using System;

namespace MoPhoGames.USpeak.Interface
{
	public interface IUSpeakTalkController
	{
		void OnInspectorGUI();

		bool ShouldSend();
	}
}