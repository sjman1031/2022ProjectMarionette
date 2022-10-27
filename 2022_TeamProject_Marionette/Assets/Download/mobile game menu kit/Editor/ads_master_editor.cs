using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(ads_master)), CanEditMultipleObjects]
public class ads_master_editor : Editor
{

    public override void OnInspectorGUI()
    {

        ads_master my_target = (ads_master)target;
        EditorGUI.BeginChangeCheck();
        Undo.RecordObject(my_target, "ads_edit");


        my_target.enable_ads = EditorGUILayout.Toggle("enable ads", my_target.enable_ads);
        if (my_target.enable_ads)
        {
            EditorGUI.indentLevel++;

            my_target.ads_test_mode = EditorGUILayout.Toggle("test mode", my_target.ads_test_mode);
            if (my_target.ads_test_mode)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("Remember to set -test mode- FALSE for your final app build for store");
                EditorGUI.indentLevel--;
            }
            else
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField(" - WARNING - ");
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("Set -test mode- FALSE only for your final app build for store");
                EditorGUILayout.LabelField("instead if you are just test your app, use TRUE");
                EditorGUILayout.LabelField("if you forgot it TRUE in your final app, you can set it FALSE from http://unityads.unity32.com/admin/");
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
            }

            my_target.rewardedVideoZone = EditorGUILayout.TextField("rewardedVideoZone", my_target.rewardedVideoZone);
            my_target.iOS_ads_app_id = EditorGUILayout.TextField("iOS app id", my_target.iOS_ads_app_id);
            my_target.android_ads_app_id = EditorGUILayout.TextField("android app id", my_target.android_ads_app_id);

            EditorGUILayout.Space();
            my_target.minimum_time_from_app_start_before_show_the_first_ad = EditorGUILayout.FloatField("minimum time, from app start start, to show the first ad (in seconds)", my_target.minimum_time_from_app_start_before_show_the_first_ad);
            my_target.minimum_time_interval_between_ads = EditorGUILayout.FloatField("minimum time interval between ads (in seconds)", my_target.minimum_time_interval_between_ads);
            my_target.reward_feedback_after_ad = EditorGUILayout.Toggle("show reward feedback after ad", my_target.reward_feedback_after_ad);
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            UnityAdsOption(my_target.adsDefaultSettings, "Defalut Settings", true);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("When call an ad:");
            EditorGUI.indentLevel++;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("* In home scene:");
                EditorGUI.indentLevel++;
                UnityAdsOption(my_target.justAfterLogo, "- Just after logo");
                UnityAdsOption(my_target.beforeStartToPlayAStage, "- Before load a stage");
                UnityAdsOption(my_target.whenReturnToHomeSceneFromAStage, "- Return to Home Scene from a stage");
                EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("* In game scene:");
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("* During the game:");
                    EditorGUI.indentLevel++;
                    UnityAdsOption(my_target.whenStageStart, "- Stage start");
                    UnityAdsOption(my_target.whenReachACheckpoint, "- Reach a checkpoint");
                    UnityAdsOption(my_target.whenPlayerOpenAGiftPackage, "- Open a gift package");
                    UnityAdsOption(my_target.askIfDoubleIntScore, "- Ask if double the score");
                    EditorGUI.indentLevel--;
                EditorGUILayout.LabelField("* Win screen:");
                    EditorGUI.indentLevel++;
                    UnityAdsOption(my_target.whenShowWinScreen, "- Show win screen");
                    //UnityAdsOption(my_target.whenGainVirtualMoneyInTheCurrentStage, "- Gain virtual money");
                    UnityAdsOption(my_target.winWith2Stars, "- If win with 2 stars (ask to see an ad to gain the third)");
                    UnityAdsOption(my_target.winWith3Stars , "- If win with 3 stars");
                    EditorGUI.indentLevel--;
                EditorGUILayout.LabelField("* Lose screen:");
                    EditorGUI.indentLevel++;
                    UnityAdsOption(my_target.whenLoseScreen, "- Show lose screen");
                    UnityAdsOption(my_target.whenContinueScreenAppear, "- Show continue screen");
                    EditorGUI.indentLevel--;
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorGUI.indentLevel--;
        }




        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(my_target);
        }
    }

    void AdReward(ads_master.AdReward target_reward, int id)
    {
        EditorGUI.indentLevel++;

        target_reward.adRewardTypeSelected = (ads_master.AdReward.AdRewardType)EditorGUILayout.EnumPopup("Type", target_reward.adRewardTypeSelected);
        EditorGUI.indentLevel++;
        if (target_reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.consumable)
        {
            if (target_reward.myConsumable == null)
                GUI.color = Color.red;
            else
                GUI.color = Color.white;

            target_reward.myConsumable = EditorGUILayout.ObjectField("item", target_reward.myConsumable, typeof(ConsumableItem), false) as ConsumableItem;

            GUI.color = Color.white;
        }
        else /*if (target_reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.virtualMoney 
            || target_reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.live
            || target_reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.continueToken)*/
        {
            target_reward.myName = EditorGUILayout.TextField("name", target_reward.myName);
            target_reward.myDescription = EditorGUILayout.TextField("description", target_reward.myDescription);
        }

        if (target_reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.consumable
            || target_reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.virtualMoney
            || target_reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.live
            || target_reward.adRewardTypeSelected == ads_master.AdReward.AdRewardType.continueToken)
        {
            if (target_reward.maxQuantity < target_reward.minQuantity)
                GUI.color = Color.red;
            else
                GUI.color = Color.white;
            target_reward.minQuantity = EditorGUILayout.IntField("min quantity", target_reward.minQuantity);
            target_reward.maxQuantity = EditorGUILayout.IntField("max quantity", target_reward.maxQuantity);

            if (target_reward.minQuantity < 1)
                target_reward.minQuantity = 1;

            if (target_reward.maxQuantity < 1)
                target_reward.maxQuantity = 1;

        }

        EditorGUI.indentLevel--;
        EditorGUI.indentLevel--;
    }

    void UnityAdsOption(ads_master.AdOptions target_ad, string my_name, bool thisIsTheDefalutAd = false)
    {
        if (thisIsTheDefalutAd)
        {
            target_ad.thisAdIsEnabled = true;
            target_ad.UseCustomSettings = true;
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            target_ad.thisAdIsEnabled = EditorGUILayout.Toggle(my_name, target_ad.thisAdIsEnabled);
            if (target_ad.thisAdIsEnabled)
                target_ad.UseCustomSettings = EditorGUILayout.Toggle("Customize", target_ad.UseCustomSettings);
            EditorGUILayout.EndHorizontal();
        }

        if (target_ad.thisAdIsEnabled && target_ad.UseCustomSettings)
        {
            EditorGUI.indentLevel++;
            EditorGUI.indentLevel++;
            string header = "Custom settings (ignore Default Settings)";
            if (thisIsTheDefalutAd)
                header = "Default settings:";

            target_ad.showCustomSettingsInInspector = EditorGUILayout.Foldout(target_ad.showCustomSettingsInInspector, header);
            if (target_ad.showCustomSettingsInInspector)
            {
                EditorGUI.indentLevel++;

                target_ad.ignoreMinimumTimeIntervalBetweenAds = EditorGUILayout.Toggle("ignore minimum time interval between ads", target_ad.ignoreMinimumTimeIntervalBetweenAds);
                target_ad.chanceToOpenAnAdHere = EditorGUILayout.IntSlider("% chance to show this ad", target_ad.chanceToOpenAnAdHere, 1, 100);
                target_ad.askToPlayerIfHeWantToWatchAnAdBeforeStartIt = EditorGUILayout.Toggle("Ask to player if he want to watch an ad before start it", target_ad.askToPlayerIfHeWantToWatchAnAdBeforeStartIt);

                if (target_ad.askToPlayerIfHeWantToWatchAnAdBeforeStartIt)
                {
                    EditorGUI.indentLevel++;
                    target_ad.askingText = EditorGUILayout.TextField("what say:", target_ad.askingText);
                    target_ad.virtualMoneyCostToGetTheRewardWithoutWatchTheAd = EditorGUILayout.IntField("How much virtual money pay to get the reward without play the ad", target_ad.virtualMoneyCostToGetTheRewardWithoutWatchTheAd);
                    target_ad.chanceToRewardIfTheAdIsSkipped = EditorGUILayout.IntSlider("% chance to get the reward if the ad is skipped", target_ad.chanceToRewardIfTheAdIsSkipped, 0, 100);
                    target_ad.numberOfAvaiblesChoices = EditorGUILayout.IntSlider("avaible choices", target_ad.numberOfAvaiblesChoices, 1, 4);


                    EditorGUILayout.LabelField("Rewards:");
                    if (target_ad.myRewards == null)
                        target_ad.myRewards = new System.Collections.Generic.List<ads_master.AdReward>();
                    if (target_ad.myRewards.Count == 0)
                    {
                        ads_master.AdReward newReward = new ads_master.AdReward();
                        target_ad.myRewards.Add(newReward);
                    }

                    EditorGUI.indentLevel++;
                    for (int i = 0; i < target_ad.myRewards.Count; i++)
                        {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("[" + i + "]");
                        if (i > 0 || target_ad.myRewards.Count > 1)
                        {
                            if (GUILayout.Button("Delete [" + i + "]"))
                            {
                                target_ad.myRewards.RemoveAt(i);
                                break;
                            }

                        }
                        EditorGUILayout.EndHorizontal();

                        AdReward(target_ad.myRewards[i], i);
                        
                        if (i == target_ad.myRewards.Count - 1)
                            {
                            GUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("[" + target_ad.myRewards.Count + "]");
                            if (GUILayout.Button("Add [" + target_ad.myRewards.Count + "]"))
                                {
                                ads_master.AdReward newReward = new ads_master.AdReward();
                                target_ad.myRewards.Add(newReward);
                                }
                            GUILayout.EndHorizontal();
                            }
                        }
                    EditorGUI.indentLevel--;

                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();

            }
            EditorGUI.indentLevel--;
            EditorGUI.indentLevel--;
        }

    }
}

