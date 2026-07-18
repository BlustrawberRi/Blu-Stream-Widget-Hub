
using System;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using Godot;

namespace SB.Events
{
    public enum EventSource
    {
        Application,
        Command,
        CrowdControl,
        Twitch
    }

    public partial class EventType : GodotObject
    {
        public string Name { get; private set; }
        public Type source;
        public string SourceName => source.Name;
        private EventType() { }
        public EventType(string name, Type source)
        {
            this.source = source;
            this.Name = name;
        }

        public string FullName => SourceName + "/" + Name;

    }
    // regex for new class from event List:
    // (\w*)\.(.*)\n
    // public static EventType $2 =>new("$2", typeof($1));\n

    public static class Command
    {
        public static string Name => "Command";

        public enum eventTypeList
        {
            Triggered, Cooldown
        }

        public static EventType Triggered => new("Triggered", typeof(Command));
        public static EventType Cooldown => new("Cooldown", typeof(Command));

    }
    public static class Twitch
    {
        public static string Name => "Twitch";
        public static EventType AdRun => new("AdRun", typeof(Twitch));
        public static EventType Announcement => new("Announcement", typeof(Twitch));
        public static EventType AutomaticRewardRedemption => new("AutomaticRewardRedemption", typeof(Twitch));
        public static EventType AutoModMessageHeld => new("AutoModMessageHeld", typeof(Twitch));
        public static EventType AutoModMessageUpdate => new("AutoModMessageUpdate", typeof(Twitch));
        public static EventType BetterTTVEmoteAdded => new("BetterTTVEmoteAdded", typeof(Twitch));
        public static EventType BetterTTVEmoteRemoved => new("BetterTTVEmoteRemoved", typeof(Twitch));
        public static EventType BitsBadgeTier => new("BitsBadgeTier", typeof(Twitch));
        public static EventType BlockedTermsAdded => new("BlockedTermsAdded", typeof(Twitch));
        public static EventType BlockedTermsDeleted => new("BlockedTermsDeleted", typeof(Twitch));
        public static EventType BotEventSubConnected => new("BotEventSubConnected", typeof(Twitch));
        public static EventType BotEventSubDisconnected => new("BotEventSubDisconnected", typeof(Twitch));
        public static EventType BotWhisper => new("BotWhisper", typeof(Twitch));
        public static EventType BroadcasterAuthenticated => new("BroadcasterAuthenticated", typeof(Twitch));
        public static EventType BroadcasterChatConnected => new("BroadcasterChatConnected", typeof(Twitch));
        public static EventType BroadcasterChatDisconnected => new("BroadcasterChatDisconnected", typeof(Twitch));
        public static EventType BroadcasterEventSubConnected => new("BroadcasterEventSubConnected", typeof(Twitch));
        public static EventType BroadcasterEventSubDisconnected => new("BroadcasterEventSubDisconnected", typeof(Twitch));
        public static EventType CharityCompleted => new("CharityCompleted", typeof(Twitch));
        public static EventType CharityDonation => new("CharityDonation", typeof(Twitch));
        public static EventType CharityProgress => new("CharityProgress", typeof(Twitch));
        public static EventType CharityStarted => new("CharityStarted", typeof(Twitch));
        public static EventType ChatCleared => new("ChatCleared", typeof(Twitch));
        public static EventType ChatEmoteModeOff => new("ChatEmoteModeOff", typeof(Twitch));
        public static EventType ChatEmoteModeOn => new("ChatEmoteModeOn", typeof(Twitch));
        public static EventType ChatFollowerModeChanged => new("ChatFollowerModeChanged", typeof(Twitch));
        public static EventType ChatFollowerModeOff => new("ChatFollowerModeOff", typeof(Twitch));
        public static EventType ChatFollowerModeOn => new("ChatFollowerModeOn", typeof(Twitch));
        public static EventType ChatMessage => new("ChatMessage", typeof(Twitch));
        public static EventType ChatMessageDeleted => new("ChatMessageDeleted", typeof(Twitch));
        public static EventType ChatSlowModeChanged => new("ChatSlowModeChanged", typeof(Twitch));
        public static EventType ChatSlowModeOff => new("ChatSlowModeOff", typeof(Twitch));
        public static EventType ChatSlowModeOn => new("ChatSlowModeOn", typeof(Twitch));
        public static EventType ChatSubscriberModeOff => new("ChatSubscriberModeOff", typeof(Twitch));
        public static EventType ChatSubscriberModeOn => new("ChatSubscriberModeOn", typeof(Twitch));
        public static EventType ChatUniqueModeOff => new("ChatUniqueModeOff", typeof(Twitch));
        public static EventType ChatUniqueModeOn => new("ChatUniqueModeOn", typeof(Twitch));
        public static EventType Cheer => new("Cheer", typeof(Twitch));
        public static EventType CoinCheer => new("CoinCheer", typeof(Twitch));
        public static EventType CommunityGoalContribution => new("CommunityGoalContribution", typeof(Twitch));
        public static EventType CommunityGoalEnded => new("CommunityGoalEnded", typeof(Twitch));
        public static EventType CustomPowerUpRedemption => new("CustomPowerUpRedemption", typeof(Twitch));
        public static EventType FirstWord => new("FirstWord", typeof(Twitch));
        public static EventType Follow => new("Follow", typeof(Twitch));
        public static EventType GiftBomb => new("GiftBomb", typeof(Twitch));
        public static EventType GiftPaidUpgrade => new("GiftPaidUpgrade", typeof(Twitch));
        public static EventType GiftSub => new("GiftSub", typeof(Twitch));
        public static EventType GoalBegin => new("GoalBegin", typeof(Twitch));
        public static EventType GoalEnd => new("GoalEnd", typeof(Twitch));
        public static EventType GoalProgress => new("GoalProgress", typeof(Twitch));
        public static EventType GuestStarGuestUpdate => new("GuestStarGuestUpdate", typeof(Twitch));
        public static EventType GuestStarSessionBegin => new("GuestStarSessionBegin", typeof(Twitch));
        public static EventType GuestStarSessionEnd => new("GuestStarSessionEnd", typeof(Twitch));
        public static EventType GuestStarSettingsUpdate => new("GuestStarSettingsUpdate", typeof(Twitch));
        public static EventType GuestStarSlotUpdate => new("GuestStarSlotUpdate", typeof(Twitch));
        public static EventType HypeChat => new("HypeChat", typeof(Twitch));
        public static EventType HypeChatLevel => new("HypeChatLevel", typeof(Twitch));
        public static EventType HypeTrainEnd => new("HypeTrainEnd", typeof(Twitch));
        public static EventType HypeTrainLevelUp => new("HypeTrainLevelUp", typeof(Twitch));
        public static EventType HypeTrainStart => new("HypeTrainStart", typeof(Twitch));
        public static EventType HypeTrainUpdate => new("HypeTrainUpdate", typeof(Twitch));
        public static EventType ModeratorAdded => new("ModeratorAdded", typeof(Twitch));
        public static EventType ModeratorRemoved => new("ModeratorRemoved", typeof(Twitch));
        public static EventType Modiversary => new("Modiversary", typeof(Twitch));
        public static EventType PayItForward => new("PayItForward", typeof(Twitch));
        public static EventType PermittedTermsAdded => new("PermittedTermsAdded", typeof(Twitch));
        public static EventType PermittedTermsDeleted => new("PermittedTermsDeleted", typeof(Twitch));
        public static EventType PollArchived => new("PollArchived", typeof(Twitch));
        public static EventType PollCompleted => new("PollCompleted", typeof(Twitch));
        public static EventType PollCreated => new("PollCreated", typeof(Twitch));
        public static EventType PollTerminated => new("PollTerminated", typeof(Twitch));
        public static EventType PollUpdated => new("PollUpdated", typeof(Twitch));
        public static EventType PowerUpRedemption => new("PowerUpRedemption", typeof(Twitch));
        public static EventType PredictionCanceled => new("PredictionCanceled", typeof(Twitch));
        public static EventType PredictionCompleted => new("PredictionCompleted", typeof(Twitch));
        public static EventType PredictionCreated => new("PredictionCreated", typeof(Twitch));
        public static EventType PredictionLocked => new("PredictionLocked", typeof(Twitch));
        public static EventType PredictionUpdated => new("PredictionUpdated", typeof(Twitch));
        public static EventType PresentViewers => new("PresentViewers", typeof(Twitch));
        public static EventType PrimePaidUpgrade => new("PrimePaidUpgrade", typeof(Twitch));
        public static EventType PyramidBroken => new("PyramidBroken", typeof(Twitch));
        public static EventType PyramidSuccess => new("PyramidSuccess", typeof(Twitch));
        public static EventType Raid => new("Raid", typeof(Twitch));
        public static EventType RaidCancelled => new("RaidCancelled", typeof(Twitch));
        public static EventType RaidSend => new("RaidSend", typeof(Twitch));
        public static EventType RaidStart => new("RaidStart", typeof(Twitch));
        public static EventType ReSub => new("ReSub", typeof(Twitch));
        public static EventType RewardCreated => new("RewardCreated", typeof(Twitch));
        public static EventType RewardDeleted => new("RewardDeleted", typeof(Twitch));
        public static EventType RewardRedemption => new("RewardRedemption", typeof(Twitch));
        public static EventType RewardRedemptionUpdated => new("RewardRedemptionUpdated", typeof(Twitch));
        public static EventType RewardUpdated => new("RewardUpdated", typeof(Twitch));
        public static EventType SevenTVEmoteAdded => new("SevenTVEmoteAdded", typeof(Twitch));
        public static EventType SevenTVEmoteRemoved => new("SevenTVEmoteRemoved", typeof(Twitch));
        public static EventType SharedChatAnnouncement => new("SharedChatAnnouncement", typeof(Twitch));
        public static EventType SharedChatCommunitySubGift => new("SharedChatCommunitySubGift", typeof(Twitch));
        public static EventType SharedChatGiftPaidUpgrade => new("SharedChatGiftPaidUpgrade", typeof(Twitch));
        public static EventType SharedChatMessageDeleted => new("SharedChatMessageDeleted", typeof(Twitch));
        public static EventType SharedChatPayItForward => new("SharedChatPayItForward", typeof(Twitch));
        public static EventType SharedChatPrimePaidUpgrade => new("SharedChatPrimePaidUpgrade", typeof(Twitch));
        public static EventType SharedChatRaid => new("SharedChatRaid", typeof(Twitch));
        public static EventType SharedChatResub => new("SharedChatResub", typeof(Twitch));
        public static EventType SharedChatSessionBegin => new("SharedChatSessionBegin", typeof(Twitch));
        public static EventType SharedChatSessionEnd => new("SharedChatSessionEnd", typeof(Twitch));
        public static EventType SharedChatSessionUpdate => new("SharedChatSessionUpdate", typeof(Twitch));
        public static EventType SharedChatSub => new("SharedChatSub", typeof(Twitch));
        public static EventType SharedChatSubGift => new("SharedChatSubGift", typeof(Twitch));
        public static EventType SharedChatUserBanned => new("SharedChatUserBanned", typeof(Twitch));
        public static EventType SharedChatUserTimedout => new("SharedChatUserTimedout", typeof(Twitch));
        public static EventType SharedChatUserUnbanned => new("SharedChatUserUnbanned", typeof(Twitch));
        public static EventType SharedChatUserUntimedout => new("SharedChatUserUntimedout", typeof(Twitch));
        public static EventType SharedModiversary => new("SharedModiversary", typeof(Twitch));
        public static EventType ShieldModeBegin => new("ShieldModeBegin", typeof(Twitch));
        public static EventType ShieldModeEnd => new("ShieldModeEnd", typeof(Twitch));
        public static EventType ShoutoutCreated => new("ShoutoutCreated", typeof(Twitch));
        public static EventType ShoutoutReceived => new("ShoutoutReceived", typeof(Twitch));
        public static EventType StreamOffline => new("StreamOffline", typeof(Twitch));
        public static EventType StreamOnline => new("StreamOnline", typeof(Twitch));
        public static EventType StreamUpdate => new("StreamUpdate", typeof(Twitch));
        public static EventType StreamUpdateGameOnConnect => new("StreamUpdateGameOnConnect", typeof(Twitch));
        public static EventType Sub => new("Sub", typeof(Twitch));
        public static EventType SubCounterRollover => new("SubCounterRollover", typeof(Twitch));
        public static EventType SuspiciousUserMessage => new("SuspiciousUserMessage", typeof(Twitch));
        public static EventType SuspiciousUserUpdate => new("SuspiciousUserUpdate", typeof(Twitch));
        public static EventType UnbanRequestApproved => new("UnbanRequestApproved", typeof(Twitch));
        public static EventType UnbanRequestCreated => new("UnbanRequestCreated", typeof(Twitch));
        public static EventType UnbanRequestDenied => new("UnbanRequestDenied", typeof(Twitch));
        public static EventType UpcomingAd => new("UpcomingAd", typeof(Twitch));
        public static EventType UserBanned => new("UserBanned", typeof(Twitch));
        public static EventType UserTimedOut => new("UserTimedOut", typeof(Twitch));
        public static EventType UserUnbanned => new("UserUnbanned", typeof(Twitch));
        public static EventType UserUntimedOut => new("UserUntimedOut", typeof(Twitch));
        public static EventType ViewerCountUpdate => new("ViewerCountUpdate", typeof(Twitch));
        public static EventType VipAdded => new("VipAdded", typeof(Twitch));
        public static EventType VipRemoved => new("VipRemoved", typeof(Twitch));
        public static EventType WarnedUser => new("WarnedUser", typeof(Twitch));
        public static EventType WarningAcknowledged => new("WarningAcknowledged", typeof(Twitch));
        public static EventType WatchStreak => new("WatchStreak", typeof(Twitch));
        public static EventType Whisper => new("Whisper", typeof(Twitch));

    }
    public static class Obs
    {
        public static string Name => "Obs";
        public static EventType Connected => new("Connected", typeof(Obs));
        public static EventType Disconnected => new("Disconnected", typeof(Obs));
        public static EventType Event => new("Event", typeof(Obs));
        public static EventType RecordingStarted => new("RecordingStarted", typeof(Obs));
        public static EventType RecordingStopped => new("RecordingStopped", typeof(Obs));
        public static EventType SceneChanged => new("SceneChanged", typeof(Obs));
        public static EventType StreamingStarted => new("StreamingStarted", typeof(Obs));
        public static EventType StreamingStopped => new("StreamingStopped", typeof(Obs));
        public static EventType VendorEvent => new("VendorEvent", typeof(Obs));

    }
    public static class Kofi
    {
        public static string Name => "Kofi";
        public static EventType Commission =>new("Commission", typeof(Kofi));
        public static EventType Donation =>new("Donation", typeof(Kofi));
        public static EventType Resubscription =>new("Resubscription", typeof(Kofi));
        public static EventType ShopOrder =>new("ShopOrder", typeof(Kofi));
        public static EventType Subscription =>new("Subscription", typeof(Kofi));

    }
    public static class StreamElements
    {
        public static string Name => "StreamElements";
        public static EventType Authenticated =>new("Authenticated", typeof(StreamElements));
        public static EventType Connected =>new("Connected", typeof(StreamElements));
    
        public static EventType Disconnected =>new("Disconnected", typeof(StreamElements));
        public static EventType Merch =>new("Merch", typeof(StreamElements));
        public static EventType Tip =>new("Tip", typeof(StreamElements));
        
    }
}

// '
// // regex: 
// //\t*(\w*),
// // public static string $1 = "$1";
// [GlobalClass] [GodotClassName("StreamerBotTrigger")]
// public partial class StreamerBotTrigger : GodotObject
// {
    
//     public string Type;
//     public string Source;

//     public struct Command
//     {
//         public const string Triggered = "Triggered";
//         public static string Cooldown = "Cooldown";

//         public override string ToString() => "Command";
//     }
//     public struct Twitch
//     {
//         public static string Follow = "Follow";
//         public static string Cheer = "Cheer";
//         public static string Sub = "Sub";
//         public static string ReSub = "ReSub";
//         public static string GiftSub = "GiftSub";
//         public static string GiftBomb = "GiftBomb";
//         public static string Raid = "Raid";
//         public static string HypeTrainStart = "HypeTrainStart";
//         public static string HypeTrainUpdate = "HypeTrainUpdate";
//         public static string HypeTrainLevelUp = "HypeTrainLevelUp";
//         public static string HypeTrainEnd = "HypeTrainEnd";
//         public static string RewardRedemption = "RewardRedemption";
//         public static string RewardCreated = "RewardCreated";
//         public static string RewardUpdated = "RewardUpdated";
//         public static string RewardDeleted = "RewardDeleted";
//         public static string CommunityGoalContribution = "CommunityGoalContribution";
//         public static string CommunityGoalEnded = "CommunityGoalEnded";
//         public static string StreamUpdate = "StreamUpdate";
//         public static string Whisper = "Whisper";
//         public static string FirstWord = "FirstWord";
//         public static string SubCounterRollover = "SubCounterRollover";
//         public static string BroadcastUpdate = "BroadcastUpdate";
//         public static string StreamUpdateGameOnConnect = "StreamUpdateGameOnConnect";
//         public static string PresentViewers = "PresentViewers";
//         public static string PollCreated = "PollCreated";
//         public static string PollUpdated = "PollUpdated";
//         public static string PollCompleted = "PollCompleted";
//         public static string PredictionCreated = "PredictionCreated";
//         public static string PredictionUpdated = "PredictionUpdated";
//         public static string PredictionCompleted = "PredictionCompleted";
//         public static string PredictionCanceled = "PredictionCanceled";
//         public static string PredictionLocked = "PredictionLocked";
//         public static string ChatMessage = "ChatMessage";
//         public static string ChatMessageDeleted = "ChatMessageDeleted";
//         public static string UserTimedOut = "UserTimedOut";
//         public static string UserBanned = "UserBanned";
//         public static string Announcement = "Announcement";
//         public static string AdRun = "AdRun";
//         public static string BotWhisper = "BotWhisper";
//         public static string CharityDonation = "CharityDonation";
//         public static string CharityCompleted = "CharityCompleted";
//         public static string CoinCheer = "CoinCheer";
//         public static string ShoutoutCreated = "ShoutoutCreated";
//         public static string UserUntimedOut = "UserUntimedOut";
//         public static string CharityStarted = "CharityStarted";
//         public static string CharityProgress = "CharityProgress";
//         public static string GoalBegin = "GoalBegin";
//         public static string GoalProgress = "GoalProgress";
//         public static string GoalEnd = "GoalEnd";
//         public static string ShieldModeBegin = "ShieldModeBegin";
//         public static string ShieldModeEnd = "ShieldModeEnd";
//         public static string AdMidRoll = "AdMidRoll";
//         public static string StreamOnline = "StreamOnline";
//         public static string StreamOffline = "StreamOffline";
//         public static string ShoutoutReceived = "ShoutoutReceived";
//         public static string ChatCleared = "ChatCleared";
//         public static string RaidStart = "RaidStart";
//         public static string RaidSend = "RaidSend";
//         public static string RaidCancelled = "RaidCancelled";
//         public static string PollTerminated = "PollTerminated";
//         public static string PyramidSuccess = "PyramidSuccess";
//         public static string PyramidBroken = "PyramidBroken";
//         public static string ViewerCountUpdate = "ViewerCountUpdate";
//         public static string GuestStarSessionBegin = "GuestStarSessionBegin";
//         public static string GuestStarSessionEnd = "GuestStarSessionEnd";
//         public static string GuestStarGuestUpdate = "GuestStarGuestUpdate";
//         public static string GuestStarSlotUpdate = "GuestStarSlotUpdate";
//         public static string GuestStarSettingsUpdate = "GuestStarSettingsUpdate";
//         public static string HypeChat = "HypeChat";
//         public static string RewardRedemptionUpdated = "RewardRedemptionUpdated";
//         public static string HypeChatLevel = "HypeChatLevel";
//         public static string BroadcasterAuthenticated = "BroadcasterAuthenticated";
//         public static string BroadcasterChatConnected = "BroadcasterChatConnected";
//         public static string BroadcasterChatDisconnected = "BroadcasterChatDisconnected";
//         public static string BroadcasterPubSubConnected = "BroadcasterPubSubConnected";
//         public static string BroadcasterPubSubDisconnected = "BroadcasterPubSubDisconnected";
//         public static string BroadcasterEventSubConnected = "BroadcasterEventSubConnected";
//         public static string BroadcasterEventSubDisconnected = "BroadcasterEventSubDisconnected";
//         public static string SevenTVEmoteAdded = "SevenTVEmoteAdded";
//         public static string SevenTVEmoteRemoved = "SevenTVEmoteRemoved";
//         public static string BetterTTVEmoteAdded = "BetterTTVEmoteAdded";
//         public static string BetterTTVEmoteRemoved = "BetterTTVEmoteRemoved";
//         public static string BotChatConnected = "BotChatConnected";
//         public static string BotChatDisconnected = "BotChatDisconnected";
//         public static string UpcomingAd = "UpcomingAd";
//     }

//     public struct Application
//     {
//         public static string ToString() => "Application";
//         public static string ActionAdded = "ActionAdded"; public static string ActionUpdated = "ActionUpdated"; public static string ActionDeleted = "ActionDeleted";
//     }
//     public struct CrowdControl
//     {
//         public static string ToString() => "CrowdControl";

//         public static string GameSessionStart = "GameSessionStart";
//         public static string GameSessionEnd = "GameSessionEnd";
//         public static string EffectRequest = "EffectRequest";
//         public static string EffectSuccess = "EffectSuccess";
//         public static string EffectFailure = "EffectFailure";
//         public static string TimedEffectStarted = "TimedEffectStarted";
//         public static string TimedEffectEnded = "TimedEffectEnded";
//         public static string TimedEffectUpdated = "TimedEffectUpdated";
//     }
//     public struct Custom
//     {
//         public static string ToString() => "Custom";
//         public static string Event = "Event";
//         public static string CodeEvent = "CodeEvent";
//     }
//     public struct DonorDrive
//     {
//         public static string ToString() => "DonorDrive";
//         public static string Donation = "Donation"; public static string ProfileUpdated = "ProfileUpdated"; public static string Incentive = "Incentive";
//     }
//     public struct Elgato
//     {
//         public static string ToString() => "Elgato";

//         public static string WaveLinkOutputSwitched = "WaveLinkOutputSwitched";
//         public static string WaveLinkOutputVolumeChanged = "WaveLinkOutputVolumeChanged";
//         public static string WaveLinkOutputMuteChanged = "WaveLinkOutputMuteChanged";
//         public static string WaveLinkSelectedOutputChanged = "WaveLinkSelectedOutputChanged";
//         public static string WaveLinkInputVolumeChanged = "WaveLinkInputVolumeChanged";
//         public static string WaveLinkInputMuteChanged = "WaveLinkInputMuteChanged";
//         public static string WaveLinkInputNameChanged = "WaveLinkInputNameChanged";
//         public static string WaveLinkMicrophoneGainChanged = "WaveLinkMicrophoneGainChanged";
//         public static string WaveLinkMicrophoneOutputVolumeChanged = "WaveLinkMicrophoneOutputVolumeChanged";
//         public static string WaveLinkMicrophoneBalanceChanged = "WaveLinkMicrophoneBalanceChanged";
//         public static string WaveLinkMicrophoneMuteChanged = "WaveLinkMicrophoneMuteChanged";
//         public static string WaveLinkMicrophoneSettingChanged = "WaveLinkMicrophoneSettingChanged";
//         public static string WaveLinkFilterAdded = "WaveLinkFilterAdded";
//         public static string WaveLinkFilterChanged = "WaveLinkFilterChanged";
//         public static string WaveLinkFilterDeleted = "WaveLinkFilterDeleted";
//         public static string WaveLinkFilterBypassStateChanged = "WaveLinkFilterBypassStateChanged";
//         public static string WaveLinkConnected = "WaveLinkConnected";
//         public static string WaveLinkDisconnected = "WaveLinkDisconnected";
//         public static string WaveLinkInputLevelMeterChanged = "WaveLinkInputLevelMeterChanged";
//         public static string WaveLinkOutputLevelMeterChanged = "WaveLinkOutputLevelMeterChanged";
//     }
//     public struct FileTail
//     {
//         public static string ToString() => "FileTail";
//         public static string Changed = "Changed";
//     }
//     public struct FileWatcher
//     {
//         public static string ToString() => "FileWatcher";
//         public static string Changed = "Changed"; public static string Created = "Created"; public static string Deleted = "Deleted"; public static string Renamed = "Renamed";
//     }
//     public struct Fourthwall
//     {
//         public static string ToString() => "Fourthwall";

//         public static string ProductCreated = "ProductCreated";
//         public static string ProductUpdated = "ProductUpdated";
//         public static string GiftPurchase = "GiftPurchase";
//         public static string OrderPlaced = "OrderPlaced";
//         public static string OrderUpdated = "OrderUpdated";
//         public static string Donation = "Donation";
//         public static string SubscriptionPurchased = "SubscriptionPurchased";
//         public static string SubscriptionExpired = "SubscriptionExpired";
//         public static string SubscriptionChanged = "SubscriptionChanged";
//     }
//     public struct General
//     {
//         public static string ToString() => "General";

//         public static string Custom = "Custom";
//     }
//     public struct HotKey
//     {
//         public static string ToString() => "HotKey";

//         public static string Press = "Press";
//     }
//     public struct HypeRate
//     {
//         public static string ToString() => "HypeRate";

//         public static string HeartRatePulse = "HeartRatePulse";
//     }
//     public struct Kofi
//     {
//         public static string ToString() => "Kofi";

//         public static string Donation = "Donation";
//         public static string Subscription = "Subscription";
//         public static string Resubscription = "Resubscription";
//         public static string ShopOrder = "ShopOrder";
//         public static string Commission = "Commission";
//     }
//     public struct Midi
//     {
//         public static string ToString() => "Midi";

//         public static string Message = "Message";
//     }
//     public struct Misc
//     {
//         public static string ToString() => "Misc";

//         public static string TimedAction = "TimedAction";
//         public static string Test = "Test";
//         public static string ProcessStarted = "ProcessStarted";
//         public static string ProcessStopped = "ProcessStopped";
//         public static string ChatWindowAction = "ChatWindowAction";
//         public static string StreamerbotStarted = "StreamerbotStarted";
//         public static string StreamerbotExiting = "StreamerbotExiting";
//         public static string ToastActivation = "ToastActivation";
//         public static string GlobalVariableUpdated = "GlobalVariableUpdated";
//         public static string ApplicationImport = "ApplicationImport";
//     }
//     public struct Obs
//     {
//         public static string ToString() => "Obs";

//         public static string Connected = "Connected";
//         public static string Disconnected = "Disconnected";
//         public static string Event = "Event";
//         public static string SceneChanged = "SceneChanged";
//         public static string StreamingStarted = "StreamingStarted";
//         public static string StreamingStopped = "StreamingStopped";
//         public static string RecordingStarted = "RecordingStarted";
//         public static string RecordingStopped = "RecordingStopped";
//     }
//     public struct Patreon
//     {
//         public static string ToString() => "Patreon";

//         public static string FollowCreated = "FollowCreated";
//         public static string FollowDeleted = "FollowDeleted";
//         public static string PledgeCreated = "PledgeCreated";
//         public static string PledgeUpdated = "PledgeUpdated";
//         public static string PledgeDeleted = "PledgeDeleted";
//     }
//     public struct Pulsoid
//     {
//         public static string ToString() => "Pulsoid";

//         public static string HeartRatePulse = "HeartRatePulse";
//     }
//     public struct Quote
//     {
//         public static string ToString() => "Quote";

//         public static string Added = "Added";
//         public static string Show = "Show";
//     }
//     public struct Raw
//     {
//         public static string ToString() => "Raw";

//         public static string Action = "Action";
//         public static string SubAction = "SubAction";
//         public static string ActionCompleted = "ActionCompleted";
//     }
//     public struct Shopify
//     {
//         public static string ToString() => "Shopify";

//         public static string OrderCreated = "OrderCreated";
//         public static string OrderPaid = "OrderPaid";
//     }
//     public struct SpeakerBot
//     {
//         public static string ToString() => "SpeakerBot";

//         public static string Connected = "Connected";
//         public static string Disconnected = "Disconnected";
//     }
//     public struct SpeechToText
//     {
//         public static string ToString() => "SpeechToText";

//         public static string Dictation = "Dictation";
//         public static string Command = "Command";
//     }
//     public struct StreamDeck
//     {
//         public static string ToString() => "StreamDeck";

//         public static string Action = "Action";
//         public static string Connected = "Connected";
//         public static string Disconnected = "Disconnected";
//         public static string Info = "Info";
//     }
//     public struct StreamElements
//     {
//         public static string ToString() => "StreamElements";

//         public static string Tip = "Tip";
//         public static string Merch = "Merch";
//         public static string Connected = "Connected";
//         public static string Disconnected = "Disconnected";
//     }
//     public struct Streamlabs
//     {
//         public static string ToString() => "Streamlabs";

//         public static string Donation = "Donation";
//         public static string Merchandise = "Merchandise";
//         public static string Connected = "Connected";
//         public static string Disconnected = "Disconnected";
//     }
//     public struct StreamlabsDesktop
//     {
//         public static string ToString() => "StreamlabsDesktop";

//         public static string Connected = "Connected";
//         public static string Disconnected = "Disconnected";
//         public static string SceneChanged = "SceneChanged";
//         public static string StreamingStarted = "StreamingStarted";
//         public static string StreamingStopped = "StreamingStopped";
//         public static string RecordingStarted = "RecordingStarted";
//         public static string RecordingStopped = "RecordingStopped";
//     }
//     public struct ThrowingSystem
//     {
//         public static string ToString() => "ThrowingSystem";

//         public static string Connected = "Connected";
//         public static string WebsocketConnected = "WebsocketConnected";
//         public static string WebsocketDisconnected = "WebsocketDisconnected";
//         public static string EventsConnected = "EventsConnected";
//         public static string EventsDisconnected = "EventsDisconnected";
//         public static string ItemHit = "ItemHit";
//         public static string TriggerActivated = "TriggerActivated";
//         public static string TriggerEnded = "TriggerEnded";
//     }
//     public struct TipeeeStream
//     {
//         public static string ToString() => "TipeeeStream";
//         public static string Donation = "Donation";
//     }

//     public struct TreatStream
//     {
//         public static string ToString() => "TreatStream";
//         public static string Treat = "Treat";
//     }

//     public struct Trovo
//     {
//         public static string ToString() => "Trovo";

//         public static string BroadcasterAuthenticated = "BroadcasterAuthenticated";
//         public static string BroadcasterChatConnected = "BroadcasterChatConnected";
//         public static string BroadcasterChatDisconnected = "BroadcasterChatDisconnected";
//         public static string FirstWords = "FirstWords";
//         public static string PresentViewers = "PresentViewers";
//         public static string ChatMessage = "ChatMessage";
//         public static string Follow = "Follow";
//         public static string SpellCast = "SpellCast";
//         public static string CustomSpellCast = "CustomSpellCast";
//         public static string Raid = "Raid";
//         public static string Subscription = "Subscription";
//         public static string Resubscription = "Resubscription";
//         public static string GiftSubscription = "GiftSubscription";
//         public static string MassGiftSubscription = "MassGiftSubscription";
//         public static string StreamOnline = "StreamOnline";
//         public static string StreamOffline = "StreamOffline";
//     }
//     public struct VStream
//     {
//         public static string ToString() => "VStream";

//         public static string BroadcasterAuthenticated = "BroadcasterAuthenticated";
//         public static string BroadcasterChatConnected = "BroadcasterChatConnected";
//         public static string BroadcasterChatDisconnected = "BroadcasterChatDisconnected";
//         public static string FirstWords = "FirstWords";
//         public static string PresentViewers = "PresentViewers";
//         public static string ChatMessage = "ChatMessage";
//         public static string NewFollower = "NewFollower";
//         public static string StreamOnline = "StreamOnline";
//         public static string StreamOffline = "StreamOffline";
//     }
//     public struct VTubeStudio
//     {
//         public static string ToString() => "VTubeStudio";

//         public static string ModelLoaded = "ModelLoaded";
//         public static string ModelUnloaded = "ModelUnloaded";
//         public static string BackgroundChanged = "BackgroundChanged";
//         public static string ModelConfigChanged = "ModelConfigChanged";
//         public static string HotkeyTriggered = "HotkeyTriggered";
//         public static string ModelAnimation = "ModelAnimation";
//         public static string Connected = "Connected";
//         public static string Disconnected = "Disconnected";
//         public static string TrackingStatusChanged = "TrackingStatusChanged";
//     }
//     public struct WebsocketClient
//     {
//         public static string ToString() => "WebsocketClient";
//         public static string Open = "Open"; public static string Close = "Close"; public static string Message = "Message";
//     }

//     public struct WebsocketCustomServer
//     {
//         public static string ToString() => "WebsocketCustomServer";
//         public static string Open = "Open"; public static string Close = "Close"; public static string Message = "Message";
//     }

//     public struct YouTube
//     {
//         public static string ToString() => "YouTube";

//         public static string BroadcastStarted = "BroadcastStarted";
//         public static string BroadcastEnded = "BroadcastEnded";
//         public static string Message = "Message";
//         public static string MessageDeleted = "MessageDeleted";
//         public static string UserBanned = "UserBanned";
//         public static string SuperChat = "SuperChat";
//         public static string SuperSticker = "SuperSticker";
//         public static string NewSponsor = "NewSponsor";
//         public static string MemberMileStone = "MemberMileStone";
//         public static string NewSponsorOnlyStarted = "NewSponsorOnlyStarted";
//         public static string NewSponsorOnlyEnded = "NewSponsorOnlyEnded";
//         public static string StatisticsUpdated = "StatisticsUpdated";
//         public static string BroadcastUpdated = "BroadcastUpdated";
//         public static string MembershipGift = "MembershipGift";
//         public static string GiftMembershipReceived = "GiftMembershipReceived";
//         public static string FirstWords = "FirstWords";
//         public static string PresentViewers = "PresentViewers";
//         public static string NewSubscriber = "NewSubscriber";
//     }

// }

