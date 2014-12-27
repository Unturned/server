using System;

namespace MoPhoGames.USpeak.Interface
{
	public interface ISpeechDataHandler
	{
		void USpeakInitializeSettings(int data);

		void USpeakOnSerializeAudio(byte[] data);
	}
}