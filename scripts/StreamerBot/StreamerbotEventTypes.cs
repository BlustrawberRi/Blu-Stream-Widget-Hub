
using System;
using Godot;

// regex: 
//\t*(\w*),
// public static string $1 = "$1";
[GlobalClass] [GodotClassName("StreamerBotTrigger")]
public partial class StreamerBotTrigger : GodotObject
{
    public string Type;
    public string Source;

    public struct Command
    {
        public static string Triggered = "Triggered";
        public static string Cooldown = "Cooldown";

        public override string ToString() => "Command";
    }
    public struct Twitch
    {
        public static string Follow = "Follow";
        public static string Cheer = "Cheer";
        public static string Sub = "Sub";
        public static string ReSub = "ReSub";
        public static string GiftSub = "GiftSub";
        public static string GiftBomb = "GiftBomb";
        public static string Raid = "Raid";
        public static string HypeTrainStart = "HypeTrainStart";
        public static string HypeTrainUpdate = "HypeTrainUpdate";
        public static string HypeTrainLevelUp = "HypeTrainLevelUp";
        public static string HypeTrainEnd = "HypeTrainEnd";
        public static string RewardRedemption = "RewardRedemption";
        public static string RewardCreated = "RewardCreated";
        public static string RewardUpdated = "RewardUpdated";
        public static string RewardDeleted = "RewardDeleted";
        public static string CommunityGoalContribution = "CommunityGoalContribution";
        public static string CommunityGoalEnded = "CommunityGoalEnded";
        public static string StreamUpdate = "StreamUpdate";
        public static string Whisper = "Whisper";
        public static string FirstWord = "FirstWord";
        public static string SubCounterRollover = "SubCounterRollover";
        public static string BroadcastUpdate = "BroadcastUpdate";
        public static string StreamUpdateGameOnConnect = "StreamUpdateGameOnConnect";
        public static string PresentViewers = "PresentViewers";
        public static string PollCreated = "PollCreated";
        public static string PollUpdated = "PollUpdated";
        public static string PollCompleted = "PollCompleted";
        public static string PredictionCreated = "PredictionCreated";
        public static string PredictionUpdated = "PredictionUpdated";
        public static string PredictionCompleted = "PredictionCompleted";
        public static string PredictionCanceled = "PredictionCanceled";
        public static string PredictionLocked = "PredictionLocked";
        public static string ChatMessage = "ChatMessage";
        public static string ChatMessageDeleted = "ChatMessageDeleted";
        public static string UserTimedOut = "UserTimedOut";
        public static string UserBanned = "UserBanned";
        public static string Announcement = "Announcement";
        public static string AdRun = "AdRun";
        public static string BotWhisper = "BotWhisper";
        public static string CharityDonation = "CharityDonation";
        public static string CharityCompleted = "CharityCompleted";
        public static string CoinCheer = "CoinCheer";
        public static string ShoutoutCreated = "ShoutoutCreated";
        public static string UserUntimedOut = "UserUntimedOut";
        public static string CharityStarted = "CharityStarted";
        public static string CharityProgress = "CharityProgress";
        public static string GoalBegin = "GoalBegin";
        public static string GoalProgress = "GoalProgress";
        public static string GoalEnd = "GoalEnd";
        public static string ShieldModeBegin = "ShieldModeBegin";
        public static string ShieldModeEnd = "ShieldModeEnd";
        public static string AdMidRoll = "AdMidRoll";
        public static string StreamOnline = "StreamOnline";
        public static string StreamOffline = "StreamOffline";
        public static string ShoutoutReceived = "ShoutoutReceived";
        public static string ChatCleared = "ChatCleared";
        public static string RaidStart = "RaidStart";
        public static string RaidSend = "RaidSend";
        public static string RaidCancelled = "RaidCancelled";
        public static string PollTerminated = "PollTerminated";
        public static string PyramidSuccess = "PyramidSuccess";
        public static string PyramidBroken = "PyramidBroken";
        public static string ViewerCountUpdate = "ViewerCountUpdate";
        public static string GuestStarSessionBegin = "GuestStarSessionBegin";
        public static string GuestStarSessionEnd = "GuestStarSessionEnd";
        public static string GuestStarGuestUpdate = "GuestStarGuestUpdate";
        public static string GuestStarSlotUpdate = "GuestStarSlotUpdate";
        public static string GuestStarSettingsUpdate = "GuestStarSettingsUpdate";
        public static string HypeChat = "HypeChat";
        public static string RewardRedemptionUpdated = "RewardRedemptionUpdated";
        public static string HypeChatLevel = "HypeChatLevel";
        public static string BroadcasterAuthenticated = "BroadcasterAuthenticated";
        public static string BroadcasterChatConnected = "BroadcasterChatConnected";
        public static string BroadcasterChatDisconnected = "BroadcasterChatDisconnected";
        public static string BroadcasterPubSubConnected = "BroadcasterPubSubConnected";
        public static string BroadcasterPubSubDisconnected = "BroadcasterPubSubDisconnected";
        public static string BroadcasterEventSubConnected = "BroadcasterEventSubConnected";
        public static string BroadcasterEventSubDisconnected = "BroadcasterEventSubDisconnected";
        public static string SevenTVEmoteAdded = "SevenTVEmoteAdded";
        public static string SevenTVEmoteRemoved = "SevenTVEmoteRemoved";
        public static string BetterTTVEmoteAdded = "BetterTTVEmoteAdded";
        public static string BetterTTVEmoteRemoved = "BetterTTVEmoteRemoved";
        public static string BotChatConnected = "BotChatConnected";
        public static string BotChatDisconnected = "BotChatDisconnected";
        public static string UpcomingAd = "UpcomingAd";
    }

    public struct Application
    {
        public static string ToString() => "Application";
        public static string ActionAdded = "ActionAdded"; public static string ActionUpdated = "ActionUpdated"; public static string ActionDeleted = "ActionDeleted";
    }
    public struct CrowdControl
    {
        public static string ToString() => "CrowdControl";

        public static string GameSessionStart = "GameSessionStart";
        public static string GameSessionEnd = "GameSessionEnd";
        public static string EffectRequest = "EffectRequest";
        public static string EffectSuccess = "EffectSuccess";
        public static string EffectFailure = "EffectFailure";
        public static string TimedEffectStarted = "TimedEffectStarted";
        public static string TimedEffectEnded = "TimedEffectEnded";
        public static string TimedEffectUpdated = "TimedEffectUpdated";
    }
    public struct Custom
    {
        public static string ToString() => "Custom";
        public static string Event = "Event";
        public static string CodeEvent = "CodeEvent";
    }
    public struct DonorDrive
    {
        public static string ToString() => "DonorDrive";
        public static string Donation = "Donation"; public static string ProfileUpdated = "ProfileUpdated"; public static string Incentive = "Incentive";
    }
    public struct Elgato
    {
        public static string ToString() => "Elgato";

        public static string WaveLinkOutputSwitched = "WaveLinkOutputSwitched";
        public static string WaveLinkOutputVolumeChanged = "WaveLinkOutputVolumeChanged";
        public static string WaveLinkOutputMuteChanged = "WaveLinkOutputMuteChanged";
        public static string WaveLinkSelectedOutputChanged = "WaveLinkSelectedOutputChanged";
        public static string WaveLinkInputVolumeChanged = "WaveLinkInputVolumeChanged";
        public static string WaveLinkInputMuteChanged = "WaveLinkInputMuteChanged";
        public static string WaveLinkInputNameChanged = "WaveLinkInputNameChanged";
        public static string WaveLinkMicrophoneGainChanged = "WaveLinkMicrophoneGainChanged";
        public static string WaveLinkMicrophoneOutputVolumeChanged = "WaveLinkMicrophoneOutputVolumeChanged";
        public static string WaveLinkMicrophoneBalanceChanged = "WaveLinkMicrophoneBalanceChanged";
        public static string WaveLinkMicrophoneMuteChanged = "WaveLinkMicrophoneMuteChanged";
        public static string WaveLinkMicrophoneSettingChanged = "WaveLinkMicrophoneSettingChanged";
        public static string WaveLinkFilterAdded = "WaveLinkFilterAdded";
        public static string WaveLinkFilterChanged = "WaveLinkFilterChanged";
        public static string WaveLinkFilterDeleted = "WaveLinkFilterDeleted";
        public static string WaveLinkFilterBypassStateChanged = "WaveLinkFilterBypassStateChanged";
        public static string WaveLinkConnected = "WaveLinkConnected";
        public static string WaveLinkDisconnected = "WaveLinkDisconnected";
        public static string WaveLinkInputLevelMeterChanged = "WaveLinkInputLevelMeterChanged";
        public static string WaveLinkOutputLevelMeterChanged = "WaveLinkOutputLevelMeterChanged";
    }
    public struct FileTail
    {
        public static string ToString() => "FileTail";
        public static string Changed = "Changed";
    }
    public struct FileWatcher
    {
        public static string ToString() => "FileWatcher";
        public static string Changed = "Changed"; public static string Created = "Created"; public static string Deleted = "Deleted"; public static string Renamed = "Renamed";
    }
    public struct Fourthwall
    {
        public static string ToString() => "Fourthwall";

        public static string ProductCreated = "ProductCreated";
        public static string ProductUpdated = "ProductUpdated";
        public static string GiftPurchase = "GiftPurchase";
        public static string OrderPlaced = "OrderPlaced";
        public static string OrderUpdated = "OrderUpdated";
        public static string Donation = "Donation";
        public static string SubscriptionPurchased = "SubscriptionPurchased";
        public static string SubscriptionExpired = "SubscriptionExpired";
        public static string SubscriptionChanged = "SubscriptionChanged";
    }
    public struct General
    {
        public static string ToString() => "General";

        public static string Custom = "Custom";
    }
    public struct HotKey
    {
        public static string ToString() => "HotKey";

        public static string Press = "Press";
    }
    public struct HypeRate
    {
        public static string ToString() => "HypeRate";

        public static string HeartRatePulse = "HeartRatePulse";
    }
    public struct Kofi
    {
        public static string ToString() => "Kofi";

        public static string Donation = "Donation";
        public static string Subscription = "Subscription";
        public static string Resubscription = "Resubscription";
        public static string ShopOrder = "ShopOrder";
        public static string Commission = "Commission";
    }
    public struct Midi
    {
        public static string ToString() => "Midi";

        public static string Message = "Message";
    }
    public struct Misc
    {
        public static string ToString() => "Misc";

        public static string TimedAction = "TimedAction";
        public static string Test = "Test";
        public static string ProcessStarted = "ProcessStarted";
        public static string ProcessStopped = "ProcessStopped";
        public static string ChatWindowAction = "ChatWindowAction";
        public static string StreamerbotStarted = "StreamerbotStarted";
        public static string StreamerbotExiting = "StreamerbotExiting";
        public static string ToastActivation = "ToastActivation";
        public static string GlobalVariableUpdated = "GlobalVariableUpdated";
        public static string ApplicationImport = "ApplicationImport";
    }
    public struct Obs
    {
        public static string ToString() => "Obs";

        public static string Connected = "Connected";
        public static string Disconnected = "Disconnected";
        public static string Event = "Event";
        public static string SceneChanged = "SceneChanged";
        public static string StreamingStarted = "StreamingStarted";
        public static string StreamingStopped = "StreamingStopped";
        public static string RecordingStarted = "RecordingStarted";
        public static string RecordingStopped = "RecordingStopped";
    }
    public struct Patreon
    {
        public static string ToString() => "Patreon";

        public static string FollowCreated = "FollowCreated";
        public static string FollowDeleted = "FollowDeleted";
        public static string PledgeCreated = "PledgeCreated";
        public static string PledgeUpdated = "PledgeUpdated";
        public static string PledgeDeleted = "PledgeDeleted";
    }
    public struct Pulsoid
    {
        public static string ToString() => "Pulsoid";

        public static string HeartRatePulse = "HeartRatePulse";
    }
    public struct Quote
    {
        public static string ToString() => "Quote";

        public static string Added = "Added";
        public static string Show = "Show";
    }
    public struct Raw
    {
        public static string ToString() => "Raw";

        public static string Action = "Action";
        public static string SubAction = "SubAction";
        public static string ActionCompleted = "ActionCompleted";
    }
    public struct Shopify
    {
        public static string ToString() => "Shopify";

        public static string OrderCreated = "OrderCreated";
        public static string OrderPaid = "OrderPaid";
    }
    public struct SpeakerBot
    {
        public static string ToString() => "SpeakerBot";

        public static string Connected = "Connected";
        public static string Disconnected = "Disconnected";
    }
    public struct SpeechToText
    {
        public static string ToString() => "SpeechToText";

        public static string Dictation = "Dictation";
        public static string Command = "Command";
    }
    public struct StreamDeck
    {
        public static string ToString() => "StreamDeck";

        public static string Action = "Action";
        public static string Connected = "Connected";
        public static string Disconnected = "Disconnected";
        public static string Info = "Info";
    }
    public struct StreamElements
    {
        public static string ToString() => "StreamElements";

        public static string Tip = "Tip";
        public static string Merch = "Merch";
        public static string Connected = "Connected";
        public static string Disconnected = "Disconnected";
    }
    public struct Streamlabs
    {
        public static string ToString() => "Streamlabs";

        public static string Donation = "Donation";
        public static string Merchandise = "Merchandise";
        public static string Connected = "Connected";
        public static string Disconnected = "Disconnected";
    }
    public struct StreamlabsDesktop
    {
        public static string ToString() => "StreamlabsDesktop";

        public static string Connected = "Connected";
        public static string Disconnected = "Disconnected";
        public static string SceneChanged = "SceneChanged";
        public static string StreamingStarted = "StreamingStarted";
        public static string StreamingStopped = "StreamingStopped";
        public static string RecordingStarted = "RecordingStarted";
        public static string RecordingStopped = "RecordingStopped";
    }
    public struct ThrowingSystem
    {
        public static string ToString() => "ThrowingSystem";

        public static string Connected = "Connected";
        public static string WebsocketConnected = "WebsocketConnected";
        public static string WebsocketDisconnected = "WebsocketDisconnected";
        public static string EventsConnected = "EventsConnected";
        public static string EventsDisconnected = "EventsDisconnected";
        public static string ItemHit = "ItemHit";
        public static string TriggerActivated = "TriggerActivated";
        public static string TriggerEnded = "TriggerEnded";
    }
    public struct TipeeeStream
    {
        public static string ToString() => "TipeeeStream";
        public static string Donation = "Donation";
    }

    public struct TreatStream
    {
        public static string ToString() => "TreatStream";
        public static string Treat = "Treat";
    }

    public struct Trovo
    {
        public static string ToString() => "Trovo";

        public static string BroadcasterAuthenticated = "BroadcasterAuthenticated";
        public static string BroadcasterChatConnected = "BroadcasterChatConnected";
        public static string BroadcasterChatDisconnected = "BroadcasterChatDisconnected";
        public static string FirstWords = "FirstWords";
        public static string PresentViewers = "PresentViewers";
        public static string ChatMessage = "ChatMessage";
        public static string Follow = "Follow";
        public static string SpellCast = "SpellCast";
        public static string CustomSpellCast = "CustomSpellCast";
        public static string Raid = "Raid";
        public static string Subscription = "Subscription";
        public static string Resubscription = "Resubscription";
        public static string GiftSubscription = "GiftSubscription";
        public static string MassGiftSubscription = "MassGiftSubscription";
        public static string StreamOnline = "StreamOnline";
        public static string StreamOffline = "StreamOffline";
    }
    public struct VStream
    {
        public static string ToString() => "VStream";

        public static string BroadcasterAuthenticated = "BroadcasterAuthenticated";
        public static string BroadcasterChatConnected = "BroadcasterChatConnected";
        public static string BroadcasterChatDisconnected = "BroadcasterChatDisconnected";
        public static string FirstWords = "FirstWords";
        public static string PresentViewers = "PresentViewers";
        public static string ChatMessage = "ChatMessage";
        public static string NewFollower = "NewFollower";
        public static string StreamOnline = "StreamOnline";
        public static string StreamOffline = "StreamOffline";
    }
    public struct VTubeStudio
    {
        public static string ToString() => "VTubeStudio";

        public static string ModelLoaded = "ModelLoaded";
        public static string ModelUnloaded = "ModelUnloaded";
        public static string BackgroundChanged = "BackgroundChanged";
        public static string ModelConfigChanged = "ModelConfigChanged";
        public static string HotkeyTriggered = "HotkeyTriggered";
        public static string ModelAnimation = "ModelAnimation";
        public static string Connected = "Connected";
        public static string Disconnected = "Disconnected";
        public static string TrackingStatusChanged = "TrackingStatusChanged";
    }
    public struct WebsocketClient
    {
        public static string ToString() => "WebsocketClient";
        public static string Open = "Open"; public static string Close = "Close"; public static string Message = "Message";
    }

    public struct WebsocketCustomServer
    {
        public static string ToString() => "WebsocketCustomServer";
        public static string Open = "Open"; public static string Close = "Close"; public static string Message = "Message";
    }

    public struct YouTube
    {
        public static string ToString() => "YouTube";

        public static string BroadcastStarted = "BroadcastStarted";
        public static string BroadcastEnded = "BroadcastEnded";
        public static string Message = "Message";
        public static string MessageDeleted = "MessageDeleted";
        public static string UserBanned = "UserBanned";
        public static string SuperChat = "SuperChat";
        public static string SuperSticker = "SuperSticker";
        public static string NewSponsor = "NewSponsor";
        public static string MemberMileStone = "MemberMileStone";
        public static string NewSponsorOnlyStarted = "NewSponsorOnlyStarted";
        public static string NewSponsorOnlyEnded = "NewSponsorOnlyEnded";
        public static string StatisticsUpdated = "StatisticsUpdated";
        public static string BroadcastUpdated = "BroadcastUpdated";
        public static string MembershipGift = "MembershipGift";
        public static string GiftMembershipReceived = "GiftMembershipReceived";
        public static string FirstWords = "FirstWords";
        public static string PresentViewers = "PresentViewers";
        public static string NewSubscriber = "NewSubscriber";
    }

}


/// <summary>
/// All the event categories and its types of streamer.bot one can subscribe to.
/// </summary>
public struct StreamerbotEventTypes
{
    public enum Command
    {
        Triggered,
        Cooldown
    }
    public enum Twitch
    {
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
        UpcomingAd
    }

    public enum Application { ActionAdded, ActionUpdated, ActionDeleted }
    public enum CrowdControl
    {
        GameSessionStart,
        GameSessionEnd,
        EffectRequest,
        EffectSuccess,
        EffectFailure,
        TimedEffectStarted,
        TimedEffectEnded,
        TimedEffectUpdated,
    }
    public enum Custom { Event, CodeEvent }
    public enum DonorDrive { Donation, ProfileUpdated, Incentive }
    public enum Elgato
    {
        WaveLinkOutputSwitched,
        WaveLinkOutputVolumeChanged,
        WaveLinkOutputMuteChanged,
        WaveLinkSelectedOutputChanged,
        WaveLinkInputVolumeChanged,
        WaveLinkInputMuteChanged,
        WaveLinkInputNameChanged,
        WaveLinkMicrophoneGainChanged,
        WaveLinkMicrophoneOutputVolumeChanged,
        WaveLinkMicrophoneBalanceChanged,
        WaveLinkMicrophoneMuteChanged,
        WaveLinkMicrophoneSettingChanged,
        WaveLinkFilterAdded,
        WaveLinkFilterChanged,
        WaveLinkFilterDeleted,
        WaveLinkFilterBypassStateChanged,
        WaveLinkConnected,
        WaveLinkDisconnected,
        WaveLinkInputLevelMeterChanged,
        WaveLinkOutputLevelMeterChanged,
    }
    public enum FileTail { Changed }
    public enum FileWatcher { Changed, Created, Deleted, Renamed }
    public enum Fourthwall
    {
        ProductCreated,
        ProductUpdated,
        GiftPurchase,
        OrderPlaced,
        OrderUpdated,
        Donation,
        SubscriptionPurchased,
        SubscriptionExpired,
        SubscriptionChanged,
    }
    public enum General { Custom }
    public enum HotKey { Press }
    public enum HypeRate { HeartRatePulse }
    public enum Kofi { Donation, Subscription, Resubscription, ShopOrder, Commission }
    public enum Midi { Message }
    public enum Misc
    {
        TimedAction,
        Test,
        ProcessStarted,
        ProcessStopped,
        ChatWindowAction,
        StreamerbotStarted,
        StreamerbotExiting,
        ToastActivation,
        GlobalVariableUpdated,
        ApplicationImport,
    }
    public enum Obs
    {
        Connected,
        Disconnected,
        Event,
        SceneChanged,
        StreamingStarted,
        StreamingStopped,
        RecordingStarted,
        RecordingStopped,
    }
    public enum Patreon { FollowCreated, FollowDeleted, PledgeCreated, PledgeUpdated, PledgeDeleted }
    public enum Pulsoid { HeartRatePulse }
    public enum Quote { Added, Show }
    public enum Raw { Action, SubAction, ActionCompleted }
    public enum Shopify { OrderCreated, OrderPaid }
    public enum SpeakerBot { Connected, Disconnected }
    public enum SpeechToText { Dictation, Command }
    public enum StreamDeck { Action, Connected, Disconnected, Info }
    public enum StreamElements { Tip, Merch, Connected, Disconnected }
    public enum Streamlabs { Donation, Merchandise, Connected, Disconnected }
    public enum StreamlabsDesktop
    {
        Connected,
        Disconnected,
        SceneChanged,
        StreamingStarted,
        StreamingStopped,
        RecordingStarted,
        RecordingStopped,
    }
    public enum ThrowingSystem
    {
        Connected,
        WebsocketConnected,
        WebsocketDisconnected,
        EventsConnected,
        EventsDisconnected,
        ItemHit,
        TriggerActivated,
        TriggerEnded,
    }
    public enum TipeeeStream { Donation }
    public enum TreatStream { Treat }
    public enum Trovo
    {
        BroadcasterAuthenticated,
        BroadcasterChatConnected,
        BroadcasterChatDisconnected,
        FirstWords,
        PresentViewers,
        ChatMessage,
        Follow,
        SpellCast,
        CustomSpellCast,
        Raid,
        Subscription,
        Resubscription,
        GiftSubscription,
        MassGiftSubscription,
        StreamOnline,
        StreamOffline,
    }
    public enum VStream
    {
        BroadcasterAuthenticated,
        BroadcasterChatConnected,
        BroadcasterChatDisconnected,
        FirstWords,
        PresentViewers,
        ChatMessage,
        NewFollower,
        StreamOnline,
        StreamOffline,
    }
    public enum VTubeStudio
    {
        ModelLoaded,
        ModelUnloaded,
        BackgroundChanged,
        ModelConfigChanged,
        HotkeyTriggered,
        ModelAnimation,
        Connected,
        Disconnected,
        TrackingStatusChanged,
    }
    public enum WebsocketClient { Open, Close, Message }
    public enum WebsocketCustomServer { Open, Close, Message }
    public enum YouTube
    {

        BroadcastStarted,
        BroadcastEnded,
        Message,
        MessageDeleted,
        UserBanned,
        SuperChat,
        SuperSticker,
        NewSponsor,
        MemberMileStone,
        NewSponsorOnlyStarted,
        NewSponsorOnlyEnded,
        StatisticsUpdated,
        BroadcastUpdated,
        MembershipGift,
        GiftMembershipReceived,
        FirstWords,
        PresentViewers,
        NewSubscriber
    }
}


