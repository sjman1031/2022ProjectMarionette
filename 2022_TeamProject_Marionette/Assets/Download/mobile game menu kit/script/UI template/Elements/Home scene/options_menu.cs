using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class options_menu : MonoBehaviour {

	public OptionSoundElementSkin music_ico;
	public Slider music_slider;

	public OptionSoundElementSkin sfx_ico;
	public Slider sfx_slider;

	public OptionSoundElementSkin voice_ico;
	public Slider voice_slider;

	game_master my_game_master;

	// Use this for initialization
	void Start () {
	}

	public void Start_me()
	{

		if (game_master.game_master_obj)
		{
			my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");

			//music
			my_game_master.Music_on_off(my_game_master.GetCurrentProfile().music_on);

			//sfx
			my_game_master.Sfx_on_off(my_game_master.GetCurrentProfile().sfx_on);

			//voice
			my_game_master.Voice_on_off(my_game_master.GetCurrentProfile().voice_on);

			Update_sound_icons();


		}
	}
	
	public void Music_on_off()
	{
		if (my_game_master.GetCurrentProfile().music_on)
		{
			my_game_master.Music_on_off(false);

		}
		else
		{
			my_game_master.Music_on_off(true);
		}
		
		Update_sound_icons();
		my_game_master.Gui_sfx(my_game_master.tap_sfx);
	}

	public void Sfx_on_off()
	{
		if (my_game_master.GetCurrentProfile().sfx_on)
		{
			my_game_master.Sfx_on_off(false);
		}
		else
		{
			my_game_master.Sfx_on_off(true);
		}
		
		Update_sound_icons();
		my_game_master.Gui_sfx(my_game_master.tap_sfx);
	}

	public void Voice_on_off()
	{
		if (my_game_master.GetCurrentProfile().voice_on)
		{
			my_game_master.Voice_on_off(false);
		}
		else
		{
			my_game_master.Voice_on_off(true);
		}
		
		Update_sound_icons();
		my_game_master.Gui_sfx(my_game_master.tap_sfx);
	}

	void Update_sound_icons()
	{
		if (my_game_master.GetCurrentProfile().music_on)
			{
            music_ico.UpdateIcon(true);
			music_slider.interactable = true;
			}
		else
			{
			music_ico.UpdateIcon(false);
            music_slider.interactable = false;
			}
		
		if (my_game_master.GetCurrentProfile().sfx_on)
			{
			sfx_ico.UpdateIcon(true);
            sfx_slider.interactable = true;
			}
		else
			{
			sfx_ico.UpdateIcon(false);
            sfx_slider.interactable = false;
			}
		
		if (my_game_master.GetCurrentProfile().voice_on)
			{
			voice_ico.UpdateIcon(true);
            voice_slider.interactable = true;
			}
		else
			{
			voice_ico.UpdateIcon(false);
            voice_slider.interactable = false;
			}
	}

	public void Update_music_volume()
	{
        my_game_master.GetCurrentProfile().music_volume = music_slider.value;
		my_game_master.Music_on_off(true);
	}

	public void Update_sfx_volume()
	{
        my_game_master.GetCurrentProfile().sfx_volume = sfx_slider.value;
		my_game_master.Sfx_on_off(true);
	}

	public void Update_voice_volume()
	{
        my_game_master.GetCurrentProfile().voice_volume = voice_slider.value;
		my_game_master.Voice_on_off(true);
	}
}
