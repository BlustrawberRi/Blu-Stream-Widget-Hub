
using Godot.Collections;

public class TwitchChatMessage : StreamerbotTrigger
{
    // todo: add all the other things
    public string Text;
    public TCMUser User = new();
    public struct Meta
    {
        public string FirstMessage;
        public bool ReturningChatter;
        public bool IsHighlighted;
        public bool IsMe;
        public bool IsCustomReward;
        public bool IsInSharedChat;
        public bool IsSharedChatHost;
        public bool IsFromSharedChatGuest;
        public string CreatedAt;
        public bool IsTest;
    }
    
    public class TCMUser
    {
        public string Color;
        public bool Subscribed;
        public int MonthsSubscribed;
        public int Id;
        public string Login;
        public string Name;
        public string Type;

    }

    public TwitchChatMessage(Dictionary data) : base(data) { }
    public override void CreateDataObject(Dictionary data)
    {
        Text = DataToString(data, ["text"]);
        User.Login = DataToString(data, ["user", "login"]);
        User.Name = DataToString(data, ["user", "name"]);
        //User.Subscribed = DataToBool(data, ["user", "subscribed"]);
    }

    // { "user": { 
    //      "role": 4.0, 
    // //      "badges": [{ 
    //             "name": "broadcaster", 
    //             "version": "1", 
    //             "imageUrl": "https://static-cdn.jtvnw.net/badges/v1/5527c58c-fb7d-422d-b71b-f309dcb85cc1/3", 
    //             "info": "" 
    //         }, 
    //         { 
    //             "name": "subscriber", "version": "0", "imageUrl": "https://static-cdn.jtvnw.net/badges/v1/5d9f2208-5dd8-11e7-8513-2ff4adfae661/3", "info": "15" }, { "name": "pichu", "version": "1", "imageUrl": "https://static-cdn.jtvnw.net/badges/v1/20f214cf-36b0-4b42-8992-3b769bcb0461/3", "info": ""
    //         }], 
    //         "color": "#5F9EA0", 
    //         "subscribed": true, 
    //         "subscriptionTier": "3000", 
    //         "monthsSubscribed": 15.0, 
    //         "id": "47111292", 
    //         "login": "blustrawberri", 
    //         "name": "Blustrawberri", 
    //         "type": "twitch" 
    //     },  
    //     "messageId": "fe602650-8b69-4cdd-a188-a4acf64c2abe", 
    //     "meta": { 
    //         "internal": false, 
    //         "firstMessage": false, 
    //         "firstMessageTimestamp": "2026-09-21T21:18:54.0151369+02:00", 
    //         "returningChatter": false, 
    //         "isHighlighted": false, 
    //         "isMe": false, 
    //         "isCustomReward": false, 
    //         "isInSharedChat": false, 
    //         "isSharedChatHost": false, 
    //         "isFromSharedChatGuest": false, 
    //         "createdAt": "2026-10-07T11:28:19.053645Z", 
    //         "isTest": false 
    //     }, 
    //     "anonymous": false, 
    //     "text": "test#", 
    //     "emotes": [], 
    //     "parts": [{ "type": "text", "text": "test#" }], 
    //     "isReply": false, 
    //     "broadcaster": {
    //          "id": "47111292", 
    //         "login": "blustrawberri", 
    //         "name": "Blustrawberri", 
    //         "type": "twitch"
    //     }, 
    //     "isInSharedChat": false, 
    //     "isSharedChatHost": false, 
    //     "isFromSharedChatGuest": false, 
    //     "createdAt": "2026-10-07T11:28:19.053645Z", 
    //     "isTest": false }

}