using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

// todo: interface for Triggers
public class RewardRedemption { //extends isTrigger


    //public int Counter;
    public string RawImput;
    //public string RawImputEscaped;
    public string RedemptionId;
    public int RewardCost;
    public string RewardId;
    public string RewardName;
    public string RewardPrompt;
    public RewardStatus Status;

    public int UserId;
    public string UserName;
    public string UserLogin;
    //public int UserCounter;

    public enum RewardStatus { unfulfilled, fullfilled }


    public RewardRedemption(Dictionary data)
    {
        try
        {
            RedemptionId = data.GetValueOrDefault("id").AsString();
            RawImput = data.GetValueOrDefault("user_input").AsString();
            Status = data.GetValueOrDefault("status").AsString() == "fulfilled"? RewardStatus.fullfilled: RewardStatus.unfulfilled;

            var reward = data.GetValueOrDefault("reward").AsGodotDictionary();
            RewardId = reward.GetValueOrDefault("id").AsString();
            RewardCost = reward.GetValueOrDefault("cost").AsInt32();
            RewardPrompt = reward.GetValueOrDefault("prompt").AsString();
            RewardName = reward.GetValueOrDefault("title").AsString();

            UserId = data.GetValueOrDefault("user_id").AsInt32();
            UserName = data.GetValueOrDefault("user_name").AsString();
            UserLogin = data.GetValueOrDefault("user_login").AsString();

        }
        catch (Exception e)
        {
            GD.PrintErr(e);
        }

    }


}