using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerProfile {



    //options
    public bool music_on;
    public float music_volume;
    public bool sfx_on;
    public float sfx_volume;
    public bool voice_on;
    public float voice_volume;



    public void InitiateMeOptions()
    {
        music_on = true;
        music_volume = 1;
        sfx_on = true;
        sfx_volume = 1;
        voice_on = true;
        voice_volume = 1;
    }




    void Save_options()
    {
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_music_on_off", Convert.ToInt32(music_on));
        PlayerPrefs.SetFloat("profile_" + profile_slot.ToString() + "_music_volume", music_volume);
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_sfx_on_off", Convert.ToInt32(sfx_on));
        PlayerPrefs.SetFloat("profile_" + profile_slot.ToString() + "_sfx_volume", sfx_volume);
        PlayerPrefs.SetInt("profile_" + profile_slot.ToString() + "_voice_on_off", Convert.ToInt32(voice_on));
        PlayerPrefs.SetFloat("profile_" + profile_slot.ToString() + "_voice_volume", voice_volume);
    }
    



    void Load_options()
    {
        music_on = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_music_on_off"));
        music_volume = PlayerPrefs.GetFloat("profile_" + profile_slot.ToString() + "_music_volume");
        sfx_on = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_sfx_on_off"));
        sfx_volume = PlayerPrefs.GetFloat("profile_" + profile_slot.ToString() + "_sfx_volume");
        voice_on = Convert.ToBoolean(PlayerPrefs.GetInt("profile_" + profile_slot.ToString() + "_voice_on_off"));
        voice_volume = PlayerPrefs.GetFloat("profile_" + profile_slot.ToString() + "_voice_volume");
    }


    


    public void Delete_this_profile_options()
    {

        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_music_on_off");
        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_music_volume");
        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_sfx_on_off");
        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_sfx_volume");
        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_voice_on_off");
        PlayerPrefs.DeleteKey("profile_" + profile_slot.ToString() + "_voice_volume");

    }

}
