
using System;

/// <summary>
/// All the event categories and its types of streamer.bot one can subscribe to.
/// </summary>
public struct StreamerbotEventTypes
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

    public enum Application {ActionAdded, ActionUpdated, ActionDeleted}
    public enum CrowdControl {
        GameSessionStart,
        GameSessionEnd,
        EffectRequest,
        EffectSuccess,
        EffectFailure,
        TimedEffectStarted,
        TimedEffectEnded,
        TimedEffectUpdated,
    }
    public enum Custom {Event, CodeEvent}
    public enum DonorDrive {Donation, ProfileUpdated, Incentive}
public enum Elgato {
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
    public enum FileTail {Changed}
    public enum FileWatcher {Changed, Created, Deleted, Renamed}
    public enum Fourthwall {
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
    public enum General {Custom}
    public enum HotKey {Press}
    public enum HypeRate {HeartRatePulse}
    public enum Kofi {Donation, Subscription, Resubscription, ShopOrder, Commission}
    public enum Midi {Message}
    public enum Misc {
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
    public enum Obs {
        Connected,
        Disconnected,
        Event,
        SceneChanged,
        StreamingStarted,
        StreamingStopped,
        RecordingStarted,
        RecordingStopped,
    }
    public enum Patreon {FollowCreated, FollowDeleted, PledgeCreated, PledgeUpdated, PledgeDeleted}
    public enum Pulsoid {HeartRatePulse}
    public enum Quote {Added, Show}
    public enum Raw {Action, SubAction, ActionCompleted}
    public enum Shopify {OrderCreated, OrderPaid}
    public enum SpeakerBot {Connected, Disconnected}
    public enum SpeechToText {Dictation, Command}
    public enum StreamDeck {Action, Connected, Disconnected, Info}
    public enum StreamElements {Tip, Merch, Connected, Disconnected}
    public enum Streamlabs {Donation, Merchandise, Connected, Disconnected}
    public enum StreamlabsDesktop {
        Connected,
        Disconnected,
        SceneChanged,
        StreamingStarted,
        StreamingStopped,
        RecordingStarted,
        RecordingStopped,
    }
    public enum ThrowingSystem {
        Connected,
        WebsocketConnected,
        WebsocketDisconnected,
        EventsConnected,
        EventsDisconnected,
        ItemHit,
        TriggerActivated,
        TriggerEnded,
    }
    public enum TipeeeStream {Donation}
    public enum TreatStream {Treat}
    public enum Trovo {
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
    public enum VStream {
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
    public enum VTubeStudio {
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
    public enum WebsocketClient {Open, Close, Message}
    public enum WebsocketCustomServer {Open, Close, Message}
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
        NewSubscriber,
    }
}


