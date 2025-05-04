using Godot;
using System;
using Godot.Collections;

public partial class StreamerbotClient : WebsocketClient
{
    [Export]
    private Dictionary<String,Array<String>>  eventRequestList = new Dictionary<string, Array<string>>();

    //private Dictionary<StreamerWidget, Dictionary<String, Array<String>>> WidgetEventRequests ;
    private Array<StreamerWidget> ConnectedWidgets = new Array<StreamerWidget>();


    /// <summary>
    /// Change getEventDic to receive other signals from Streamerbot.
    /// </summary>
    public void SendEventRequests()
    {
        var getEventsDic = new Dictionary
        {
            {"id", "godot-subEvent"},
            {"request", "Subscribe"},
            {"events", eventRequestList}
        };

        if (!SendToServer(Json.Stringify(getEventsDic, "\t")))
			GD.Print("Event Request could not be sent.");
    }

    public void AddEventRequests ( Enum[] eventTypes, StreamerWidget widget)
    {
        GD.PrintRich("[b]" + widget.Name + " added Requests [/b]:");
        //var eventTypeList = new Godot.Collections.Array();
        foreach (var et in eventTypes)
        {
            GD.PrintRich("[rainbow]+[/rainbow] " +et.GetType().Name + " " + new Array<String>() { et.ToString() });

            bool hasEventSource = eventRequestList.ContainsKey(et.GetType().Name);
            bool hasEventType = hasEventSource ? eventRequestList[et.GetType().Name].Contains(et.ToString()) : false;
            if (hasEventSource)
            {
                if (!hasEventType)
                    eventRequestList[et.GetType().Name].Add(et.ToString());
            }
            else
            {
                if (!hasEventType)
                    eventRequestList.Add(et.GetType().Name, new Array<String>() { et.ToString() });
            }

        }

        ConnectedWidgets.Add(widget);
    }

    protected override void _OnConnectionEstablished()
    {
        base._OnConnectionEstablished();
        SendEventRequests();
    }

    protected override void _OnAnswerReceived(string answer)
    {
        if (answer == "") return;
        base._OnAnswerReceived(answer);
        _FilterAnswer(answer);
    }

    private void _FilterAnswer(string answer)
    {
        var answerDic = Json.ParseString(answer).AsGodotDictionary();
        if (!answerDic.ContainsKey("event")) return;

        foreach (var widget in ConnectedWidgets)
        {
            foreach (var eventType in widget.StreamerbotEventRequests)
            {
                if ((answerDic["event"].AsGodotDictionary())?["source"].AsString() != eventType.GetType().Name)
                    continue;

                GD.Print("Answer is " + eventType.GetType().Name +"/"+eventType.ToString());
                widget.OnAnswerReceived(answer);
                break;
            }
        }
        
    }

    public struct EventTypes
    {
        public enum Command { 
            Triggered, 
            Cooldown }
        public enum Twitch {
            Follow,
            Cheer,
            Sub,
            ReSub,
            GiftSub,
            GiftBomb,
            Raid,
            HypeTrainStart,
            HypeTrainUpdate,
            HypeTrainLevelUp,
            HypeTrainEnd,
            RewardRedemption,
            RewardCreated,
            RewardUpdated,
            RewardDeleted,
            CommunityGoalContribution,
            CommunityGoalEnded,
            StreamUpdate,
            Whisper,
            FirstWord,
            SubCounterRollover,
            BroadcastUpdate,
            StreamUpdateGameOnConnect,
            PresentViewers,
            PollCreated,
            PollUpdated,
            PollCompleted,
            PredictionCreated,
            PredictionUpdated,
            PredictionCompleted,
            PredictionCanceled,
            PredictionLocked,
            ChatMessage,
            ChatMessageDeleted,
            UserTimedOut,
            UserBanned,
            Announcement,
            AdRun,
            BotWhisper,
            CharityDonation,
            CharityCompleted,
            CoinCheer,
            ShoutoutCreated,
            UserUntimedOut,
            CharityStarted,
            CharityProgress,
            GoalBegin,
            GoalProgress,
            GoalEnd,
            ShieldModeBegin,
            ShieldModeEnd,
            AdMidRoll,
            StreamOnline,
            StreamOffline,
            ShoutoutReceived,
            ChatCleared,
            RaidStart,
            RaidSend,
            RaidCancelled,
            PollTerminated,
            PyramidSuccess,
            PyramidBroken,
            ViewerCountUpdate,
            GuestStarSessionBegin,
            GuestStarSessionEnd,
            GuestStarGuestUpdate,
            GuestStarSlotUpdate,
            GuestStarSettingsUpdate,
            HypeChat,
            RewardRedemptionUpdated,
            HypeChatLevel,
            BroadcasterAuthenticated,
            BroadcasterChatConnected,
            BroadcasterChatDisconnected,
            BroadcasterPubSubConnected,
            BroadcasterPubSubDisconnected,
            BroadcasterEventSubConnected,
            BroadcasterEventSubDisconnected,
            SevenTVEmoteAdded,
            SevenTVEmoteRemoved,
            BetterTTVEmoteAdded,
            BetterTTVEmoteRemoved,
            BotChatConnected,
            BotChatDisconnected,
            UpcomingAd}
    }

   
        /*
        Application: ['ActionAdded', 'ActionUpdated', 'ActionDeleted'],
        Command: ['Triggered', 'Cooldown'],
        CrowdControl: [
            'GameSessionStart',
            'GameSessionEnd',
            'EffectRequest',
            'EffectSuccess',
            'EffectFailure',
            'TimedEffectStarted',
            'TimedEffectEnded',
            'TimedEffectUpdated',
        ],
        Custom: ['Event', 'CodeEvent'],
        DonorDrive: ['Donation', 'ProfileUpdated', 'Incentive'],
        Elgato: [
            'WaveLinkOutputSwitched',
            'WaveLinkOutputVolumeChanged',
            'WaveLinkOutputMuteChanged',
            'WaveLinkSelectedOutputChanged',
            'WaveLinkInputVolumeChanged',
            'WaveLinkInputMuteChanged',
            'WaveLinkInputNameChanged',
            'WaveLinkMicrophoneGainChanged',
            'WaveLinkMicrophoneOutputVolumeChanged',
            'WaveLinkMicrophoneBalanceChanged',
            'WaveLinkMicrophoneMuteChanged',
            'WaveLinkMicrophoneSettingChanged',
            'WaveLinkFilterAdded',
            'WaveLinkFilterChanged',
            'WaveLinkFilterDeleted',
            'WaveLinkFilterBypassStateChanged',
            'WaveLinkConnected',
            'WaveLinkDisconnected',
            'WaveLinkInputLevelMeterChanged',
            'WaveLinkOutputLevelMeterChanged',
        ],
        FileTail: ['Changed'],
        FileWatcher: ['Changed', 'Created', 'Deleted', 'Renamed'],
        Fourthwall: [
            'ProductCreated',
            'ProductUpdated',
            'GiftPurchase',
            'OrderPlaced',
            'OrderUpdated',
            'Donation',
            'SubscriptionPurchased',
            'SubscriptionExpired',
            'SubscriptionChanged',
        ],
        General: ['Custom'],
        HotKey: ['Press'],
        HypeRate: ['HeartRatePulse'],
        Kofi: ['Donation', 'Subscription', 'Resubscription', 'ShopOrder', 'Commission'],
        Midi: ['Message'],
        Misc: [
            'TimedAction',
            'Test',
            'ProcessStarted',
            'ProcessStopped',
            'ChatWindowAction',
            'StreamerbotStarted',
            'StreamerbotExiting',
            'ToastActivation',
            'GlobalVariableUpdated',
            'ApplicationImport',
        ],
        Obs: [
            'Connected',
            'Disconnected',
            'Event',
            'SceneChanged',
            'StreamingStarted',
            'StreamingStopped',
            'RecordingStarted',
            'RecordingStopped',
        ],
        Patreon: ['FollowCreated', 'FollowDeleted', 'PledgeCreated', 'PledgeUpdated', 'PledgeDeleted'],
        Pulsoid: ['HeartRatePulse'],
        Quote: ['Added', 'Show'],
        Raw: ['Action', 'SubAction', 'ActionCompleted'],
        Shopify: ['OrderCreated', 'OrderPaid'],
        SpeakerBot: ['Connected', 'Disconnected'],
        SpeechToText: ['Dictation', 'Command'],
        StreamDeck: ['Action', 'Connected', 'Disconnected', 'Info'],
        StreamElements: ['Tip', 'Merch', 'Connected', 'Disconnected'],
        Streamlabs: ['Donation', 'Merchandise', 'Connected', 'Disconnected'],
        StreamlabsDesktop: [
            'Connected',
            'Disconnected',
            'SceneChanged',
            'StreamingStarted',
            'StreamingStopped',
            'RecordingStarted',
            'RecordingStopped',
        ],
        ThrowingSystem: [
            'Connected',
            'WebsocketConnected',
            'WebsocketDisconnected',
            'EventsConnected',
            'EventsDisconnected',
            'ItemHit',
            'TriggerActivated',
            'TriggerEnded',
        ],
        TipeeeStream: ['Donation'],
        TreatStream: ['Treat'],
        Trovo: [
            'BroadcasterAuthenticated',
            'BroadcasterChatConnected',
            'BroadcasterChatDisconnected',
            'FirstWords',
            'PresentViewers',
            'ChatMessage',
            'Follow',
            'SpellCast',
            'CustomSpellCast',
            'Raid',
            'Subscription',
            'Resubscription',
            'GiftSubscription',
            'MassGiftSubscription',
            'StreamOnline',
            'StreamOffline',
        ],
    VStream: [
        "BroadcasterAuthenticated',
        'BroadcasterChatConnected',
        'BroadcasterChatDisconnected',
        'FirstWords',
        'PresentViewers',
        'ChatMessage',
        'NewFollower',
        'StreamOnline',
        'StreamOffline',
    ],
    VTubeStudio: [
        'ModelLoaded',
        'ModelUnloaded',
        'BackgroundChanged',
        'ModelConfigChanged',
        'HotkeyTriggered',
        'ModelAnimation',
        'Connected',
        'Disconnected',
        'TrackingStatusChanged',
    ],
    WebsocketClient: ['Open', 'Close', 'Message'],
    WebsocketCustomServer: ['Open', 'Close', 'Message'],
    YouTube: [
        'BroadcastStarted',
        'BroadcastEnded',
        'Message',
        'MessageDeleted',
        'UserBanned',
        'SuperChat',
        'SuperSticker',
        'NewSponsor',
        'MemberMileStone',
        'NewSponsorOnlyStarted',
        'NewSponsorOnlyEnded',
        'StatisticsUpdated',
        'BroadcastUpdated',
        'MembershipGift',
        'GiftMembershipReceived',
        'FirstWords',
        'PresentViewers',
        'NewSubscriber',
    ],*/

}
