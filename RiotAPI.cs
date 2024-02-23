using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

public static class RiotAPI
{
    #region ENUMS

    public enum Region
    {
        americas,
        asia,
        esports,
        europe,
        sea
    }

    public enum Platform
    {
        br1,
        eun1,
        euw1,
        jp1,
        kr,
        la1,
        la2,
        na1,
        oc1,
        ph2,
        ru,
        sg2,
        th2,
        tr1,
        tw2,
        vn2
    }

    public enum Locale
    {
        cs_CZ,
        el_GR,
        pl_PL,
        ro_RO,
        hu_HU,
        en_GB,
        de_DE,
        es_ES,
        it_IT,
        fr_FR,
        ja_JP,
        ko_KR,
        es_MX,
        es_AR,
        pt_BR,
        en_US,
        en_AU,
        ru_RU,
        tr_TR,
        ms_MY,
        en_PH,
        en_SG,
        th_TH,
        vn_VN,
        id_ID,
        zh_MY,
        zh_CN,
        zh_TW
    }

    public enum Queue
    {
        RANKED_SOLO_5x5,
        RANKED_TFT,
        RANKED_FLEX_SR,
        RANKED_FLEX_TT
    }

    public enum Tier
    {
        IRON,
        BRONZE,
        SILVER,
        GOLD,
        PLATINUM,
        EMERALD,
        DIAMOND,
        MASTER,
        GRANDMASTER,
        CHALLENGER
    }

    public enum Division
    {
        I,
        II,
        III,
        IV
    }

    public enum ChallengeLevel
    {
        NONE,
        IRON,
        BRONZE,
        SILVER,
        GOLD,
        PLATINUM,
        DIAMOND,
        MASTER,
        GRANDMASTER,
        CHALLENGER,
        HIGHEST_NOT_LEADERBOARD_ONLY,
        HIGHEST,
        LOWEST
    }

    public enum State
    {
        DISABLED,
        HIDDEN,
        ENABLED,
        ARCHIVED
    }

    public enum Tracking
    {
        LIFETIME,
        SEASON
    }

    public enum Level
    {
        NONE,
        IRON,
        BRONZE,
        SILVER,
        GOLD,
        PLATINUM,
        DIAMOND,
        MASTER,
        GRANDMASTER,
        CHALLENGER,
    }

    public enum Type
    {
        None,
        ranked,
        normal,
        tourney,
        tutorial,
    }

    #endregion

    #region ClassVariables

    private static readonly HttpClient Client = new();
    public static string? APIKey = "";
    public static bool RetryFailedRequests = false;

    #endregion

    #region RequestMethod

    public static async Task<string> Request(string APIUrl, string? accessToken = null)
    {
        Client.DefaultRequestHeaders.Clear();

        if (accessToken != null)
            // Add an Authorisation header to the GET request containing the access token
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", accessToken);

        HttpResponseMessage httpResponseMessage = await Client.GetAsync(APIUrl);

        if (httpResponseMessage.IsSuccessStatusCode)
            return await httpResponseMessage.Content.ReadAsStringAsync();
        else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.TooManyRequests && RetryFailedRequests)
        {
            return await Request(APIUrl, accessToken);
        }
        else
        {
            throw new Exception(httpResponseMessage.ReasonPhrase);
        }
    }

    #endregion

    #region ConvertLocale

    public static string ConvertLocale(Locale locale)
    {
        return locale switch
        {
            Locale.cs_CZ => "cs-CZ",
            Locale.el_GR => "el-GR",
            Locale.pl_PL => "pl-PL",
            Locale.ro_RO => "ro-RO",
            Locale.hu_HU => "hu-HU",
            Locale.en_GB => "en-GB",
            Locale.de_DE => "de-DE",
            Locale.es_ES => "es-ES",
            Locale.it_IT => "it-IT",
            Locale.fr_FR => "fr-FR",
            Locale.ja_JP => "ja-JP",
            Locale.ko_KR => "ko-KR",
            Locale.es_MX => "es-MX",
            Locale.es_AR => "es-AR",
            Locale.pt_BR => "pt-BR",
            Locale.en_US => "en-US",
            Locale.en_AU => "en-AU",
            Locale.ru_RU => "ru-RU",
            Locale.tr_TR => "tr-TR",
            Locale.ms_MY => "ms-MY",
            Locale.en_PH => "en-PH",
            Locale.en_SG => "en-SG",
            Locale.th_TH => "th-TH",
            Locale.vn_VN => "vn-VN",
            Locale.id_ID => "id-ID",
            Locale.zh_MY => "zh-MY",
            Locale.zh_CN => "zh-CN",
            Locale.zh_TW => "zh-TW",
            _ => "en-US",
        };
    }

    #endregion

    #region Account-V1

    public static class AccountV1Async
    {
        public static async Task<AccountDTO?> GetAccountByPUUID(Region region, string puuid)
        {
            try
            {
                return JsonConvert.DeserializeObject<AccountDTO>(await Request($"https://{region}.api.riotgames.com/riot/account/v1/accounts/by-puuid/{puuid}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<AccountDTO?> GetAccountByRiotID(Region region, string gameName, string tagLine)
        {
            try
            {
                return JsonConvert.DeserializeObject<AccountDTO>(await Request($"https://{region}.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Not Needed Yet
        //public static async Task<string> GetActiveShardByPUUID(Region region, Game game, string puuid)
        //{
        //    return await Request($"https://{region}.api.riotgames.com/riot/account/v1/active-shards/by-game/{game}/by-puuid/{puuid}?api_key=");
        //}

        // Not Needed Yet
        //public static async Task<Models.Account> GetAccountByAccessToken(Region region, string accessToken)
        //{
        //    return JsonConvert.DeserializeObject<Models.Account>(await Request($"https://{region}.api.riotgames.com/riot/account/v1/accounts/me?api_key=", accessToken));
        //}

        public class AccountDTO
        {
            public string? Puuid;
            public string? GameName;
            public string? TagLine;
        }
    }

    #endregion

    #region Champion-Mastery-V4

    public static class ChampionMasteryV4Async
    {
        public static async Task<List<ChampionMasteryDTO>?> GetChampionMasteryByPuuid(Platform platform, string puuid)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<ChampionMasteryDTO>>(await Request($"https://{platform}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<ChampionMasteryDTO?> GetChampionMasteryByPuuidAndChampionId(Platform platform, string puuid, string championId)
        {
            try
            {
                return JsonConvert.DeserializeObject<ChampionMasteryDTO>(await Request($"https://{platform}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}/by-champion/{championId}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<ChampionMasteryDTO>?> GetChampionMasteryByPuuidTop(Platform platform, string puuid, int count = 0)
        {
            string countStr = "";

            if (count != 0)
                countStr = $"count={count}&";

            try
            {
                return JsonConvert.DeserializeObject<List<ChampionMasteryDTO>>(await Request($"https://{platform}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}/top?{countStr}api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<int?> GetChampionMasteryByPuuidTotalScore(Platform platform, string puuid)
        {
            try
            {
                return JsonConvert.DeserializeObject<int>(await Request($"https://{platform}.api.riotgames.com/lol/champion-mastery/v4/scores/by-puuid/{puuid}?api_key={APIKey}"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class ChampionMasteryDTO
        {
            public string? Puuid;
            public long ChampionPointsUntilNextLevel;
            public bool ChestGranted;
            public long ChampionID;
            public long LastPlayTime;
            public int ChampionLevel;
            public string? SummonerID;
            public int ChampionPoints;
            public long ChampionPointsSinceLastLevel;
            public int TokensEarned;
        }
    }

    #endregion

    #region Champion-V3

    public static class ChampionV3Async
    {
        public static async Task<ChampionInfo?> GetChampionRotation(Platform platform)
        {
            try
            {
                return JsonConvert.DeserializeObject<ChampionInfo>(await Request($"https://{platform}.api.riotgames.com/lol/platform/v3/champion-rotations?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class ChampionInfo
        {
            public int MaxNewPlayerLevel;
            public List<int>? FreeChampionIDsForNewPlayers;
            public List<int>? FreeChampionIDs;
        }
    }

    #endregion

    #region Clash-V1

    public static class ClashV1Async
    {
        public static async Task<List<PlayerDTO>?> GetClashPlayersBySummonerId(Platform platform, string summonerId)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<PlayerDTO>>(await Request($"https://{platform}.api.riotgames.com/lol/clash/v1/players/by-summoner/{summonerId}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<TeamDTO?> GetClashTeamByTeamId(Platform platform, string teamId)
        {
            try
            {
                return JsonConvert.DeserializeObject<TeamDTO>(await Request($"https://{platform}.api.riotgames.com/lol/clash/v1/teams/{teamId}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<TournamentDTO>?> GetClashTournaments(Platform platform)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<TournamentDTO>>(await Request($"https://{platform}.api.riotgames.com/lol/clash/v1/tournaments?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<TournamentDTO?> GetClashTournamentByTeamId(Platform platform, string teamId)
        {
            try
            {
                return JsonConvert.DeserializeObject<TournamentDTO>(await Request($"https://{platform}.api.riotgames.com/lol/clash/v1/tournaments/by-team/{teamId}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<TournamentDTO?> GetClashTournamentByTournamentId(Platform platform, string tournamentId)
        {
            try
            {
                return JsonConvert.DeserializeObject<TournamentDTO>(await Request($"https://{platform}.api.riotgames.com/lol/clash/v1/tournaments/{tournamentId}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class PlayerDTO
        {
            public string? SummonerID;
            public string? TeamID;
            public string? Position;
            public string? Role;
        }

        public class TeamDTO
        {
            public string? ID;
            public int TournamentID;
            public string? Name;
            public int IconID;
            public int Tier;
            public string? Captain;
            public string? Abbreviation;
            public List<PlayerDTO>? Players;
        }

        public class TournamentDTO
        {
            public int ID;
            public int ThemeID;
            public string? NameKey;
            public string? NameKeySecondary;
            public List<TournamentPhaseDTO>? Schedule;
        }

        public class TournamentPhaseDTO
        {
            public int ID;
            public long RegistrationTime;
            public long StartTime;
            public bool Cancelled;
        }
    }

    #endregion

    #region League-Exp-V4

    public static class LeagueExpV4Async
    {
        public static async Task<HashSet<LeagueEntryDTO>?> GetLeagueEntriesByQueueTierDivision(Platform platform, Queue queue, Tier tier, Division division, int page = 0)
        {
            string pageStr = "";

            if (page != 0)
                pageStr = $"page={page}&";

            try
            {
                return JsonConvert.DeserializeObject<HashSet<LeagueEntryDTO>>(await Request($"https://{platform}.api.riotgames.com/lol/league-exp/v4/entries/{queue}/{tier}/{division}?{pageStr}api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class LeagueEntryDTO
        {
            public string? LeagueID;
            public string? SummonerID;
            public string? SummonerName;
            public string? QueueType;
            public string? Tier;
            public string? Rank;
            public int LeaguePoints;
            public int Wins;
            public int Losses;
            public bool HotStreak;
            public bool Veteran;
            public bool FreshBlood;
            public bool Inactive;
            public MiniSeriesDTO? MiniSeries;
        }

        public class MiniSeriesDTO
        {
            public int Losses;
            public string? Progress;
            public int Target;
            public int Wins;
        }
    }

    #endregion

    #region League-V4

    public static class LeagueV4Async
    {
        public static async Task<LeagueListDTO?> GetChallengerLeagueByQueue(Platform platform, Queue queue)
        {
            if (queue == Queue.RANKED_TFT)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<LeagueListDTO>(await Request($"https://{platform}.api.riotgames.com/lol/league/v4/challengerleagues/by-queue/{queue}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<HashSet<LeagueEntryDTO>?> GetLeagueEntriesInQueuesBySummonerId(Platform platform, string summonerId)
        {
            try
            {
                return JsonConvert.DeserializeObject<HashSet<LeagueEntryDTO>>(await Request($"https://{platform}.api.riotgames.com/lol/league/v4/entries/by-summoner/{summonerId}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<HashSet<LeagueEntryDTO>?> GetLeagueEntriesByQueueTierDivision(Platform platform, Queue queue, Tier tier, Division division, int page = 0)
        {
            string pageStr = "";

            if (page != 0)
                pageStr = $"page={page}&";

            try
            {
                return JsonConvert.DeserializeObject<HashSet<LeagueEntryDTO>>(await Request($"https://{platform}.api.riotgames.com/lol/league/v4/entries/{queue}/{tier}/{division}?{pageStr}api_key={APIKey}"))!;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<LeagueListDTO?> GetGrandmasterLeagueByQueue(Platform platform, Queue queue)
        {
            if (queue == Queue.RANKED_TFT)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<LeagueListDTO>(await Request($"https://{platform}.api.riotgames.com/lol/league/v4/grandmasterleagues/by-queue/{queue}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<LeagueListDTO?> GetLeagueByLeagueId(Platform platform, string leagueId)
        {
            try
            {
                return JsonConvert.DeserializeObject<LeagueListDTO>(await Request($""))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<LeagueListDTO?> GetMasterLeagueByQueue(Platform platform, Queue queue)
        {
            if (queue == Queue.RANKED_TFT)
                return null;

            try
            {
                return JsonConvert.DeserializeObject<LeagueListDTO>(await Request($"https://{platform}.api.riotgames.com/lol/league/v4/masterleagues/by-queue/{queue}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class LeagueEntryDTO
        {
            public string? LeagueID;
            public string? SummonerID;
            public string? SummonerName;
            public string? QueueType;
            public string? Tier;
            public string? Rank;
            public int LeaguePoints;
            public int Wins;
            public int Losses;
            public bool HotStreak;
            public bool Veteran;
            public bool FreshBlood;
            public bool Inactive;
            public MiniSeriesDTO? MiniSeries;
        }
        public class MiniSeriesDTO
        {
            public int Losses;
            public string? Progress;
            public int Target;
            public int Wins;
        }

        public class LeagueListDTO
        {
            public string? LeagueID;
            public List<LeagueItemDTO>? Entries;
            public string? Tier;
            public string? Name;
            public string? Queue;
        }

        public class LeagueItemDTO
        {
            public bool FreshBlood;
            public int Wins;
            public string? SummonerName;
            public MiniSeriesDTO? MiniSeries;
            public bool Inactive;
            public bool Veteran;
            public bool HotStreak;
            public string? Rank;
            public int LeaguePoints;
            public int Losses;
            public string? SummonerID;
        }
    }

    #endregion

    #region LOL-CHALLENGES-V1

    public static class LolChallengesV1Async
    {
        public static async Task<List<ChallengeConfigInfoDTO>?> GetAllChallengeConfigurations(Platform platform)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<ChallengeConfigInfoDTO>>(await Request($"https://{platform}.api.riotgames.com/lol/challenges/v1/challenges/config?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<Dictionary<long, Dictionary<int, Dictionary<Level, double>>>?> GetChallengePercentiles(Platform platform)
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<long, Dictionary<int, Dictionary<Level, double>>>>(await Request($"https://{platform}.api.riotgames.com/lol/challenges/v1/challenges/percentiles?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<ChallengeConfigInfoDTO?> GetChallengeConfigurationByChallengeId(Platform platform, long challengeId)
        {
            try
            {
                return JsonConvert.DeserializeObject<ChallengeConfigInfoDTO>(await Request($"https://{platform}.api.riotgames.com/lol/challenges/v1/challenges/{challengeId}/config?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<List<ApexPlayerInfoDTO>?> GetTopPlayersForEachLevelByChallengeId(Platform platform, Level level, long challengeId, int limit = 0)
        {
            if (level == Level.CHALLENGER || level == Level.GRANDMASTER || level == Level.MASTER)
                return null;

            string limitStr = "";

            if (limit != 0)
                limitStr = $"limit={limit}&";

            try
            {
                return JsonConvert.DeserializeObject<List<ApexPlayerInfoDTO>>(await Request($"https://{platform}.api.riotgames.com/lol/challenges/v1/challenges/{challengeId}/leaderboards/by-level/{level}?{limitStr}api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<Dictionary<Level, double>?> GetPercentilesOfPlayersByChallengeId(Platform platform, long challengeId)
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<Level, double>>(await Request($"https://{platform}.api.riotgames.com/lol/challenges/v1/challenges/{challengeId}/percentiles?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<PlayerInfoDTO?> GetPlayerChallengerInfoByPuuid(Platform platform, string puuid)
        {
            try
            {
                return JsonConvert.DeserializeObject<PlayerInfoDTO>(await Request($"https://{platform}.api.riotgames.com/lol/challenges/v1/player-data/{puuid}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class ChallengeConfigInfoDTO
        {
            public long ID;
            public Dictionary<string, Dictionary<string, string>>? LocalizedNames;
            public State? State;
            public Tracking? Tracking;
            public long StartTimestamp;
            public long EndTimestamp;
            public bool Leaderboard;
            public Dictionary<string, double>? Thresholds;
        }

        public class ApexPlayerInfoDTO
        {
            public string? Puuid;
            public double Value;
            public int Position;
        }

        public class PlayerInfoDTO
        {
            public List<ChallengePoints>? Challenges;
            public PlayerClientPreferences? Preferences;
            public ChallengeInfo? TotalPoints;
            public Dictionary<string, ChallengeInfo>? CategoryPoints;
        }

        public class ChallengeInfo
        {
            public Level Level;
            public int Current;
            public int Max;
            public float Percentile;
        }

        public class PlayerClientPreferences
        {
            public string? BannerAccent;
            public string? Title;
            public List<int>? ChallengeIDs;
            public string? CrestBorder;
            public int PrestigeCrestBorderLevel;
        }

        public class ChallengePoints
        {
            public int ChallengeID;
            public float Percentile;
            public Level Level;
            public float Value;
            public long AchievedTime;
        }
    }

    #endregion

    #region LOL-STATUS-V4

    public static class LolStatusV4Async
    {
        public static async Task<PlatformDataDTO?> GetLeagueStatusForPlatform(Platform platform)
        {
            try
            {
                return JsonConvert.DeserializeObject<PlatformDataDTO>(await Request($"https://{platform}.api.riotgames.com/lol/status/v4/platform-data?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class PlatformDataDTO
        {
            public string? ID;
            public string? Name;
            public List<string>? Locales;
            public List<StatusDTO>? Maintenances;
            public List<StatusDTO>? Incidents;
        }

        public class StatusDTO
        {
            public int ID;
            public string? Maintenance_Status;
            public string? Incident_Severity;
            public List<ContentDTO>? Titles;
            public List<UpdateDTO>? Updates;
            public string? Created_At;
            public string? Archive_At;
            public string? Updated_At;
            public List<string>? Platforms;
        }

        public class ContentDTO
        {
            public string? Locale;
            public string? Content;
        }

        public class UpdateDTO
        {
            public int ID;
            public string? Author;
            public bool Publish;
            public List<string>? Publish_Locations;
            public List<ContentDTO>? Translations;
            public string? Created_At;
            public string? Updated_At;
        }
    }

    #endregion

    #region MATCH-V5

    public static class MatchV5Async
    {
        public static async Task<List<string>?> GetMatchIdsByPuuid(Region region, string puuid, long startTime = 0, long endTime = 0, int queue = 0, Type type = Type.None, int start = 0, int count = 20)
        {
            string startTimeStr = "";
            string endTimeStr = "";
            string queueStr = "";
            string typeStr = "";

            if (startTime != 0)
                startTimeStr = $"startTime={startTime}&";
            if (endTime != 0)
                endTimeStr = $"endTime={endTime}&";
            if (queue != 0)
                queueStr = $"queue={queue}&";
            if (type != Type.None)
                typeStr = $"type={type}&";
            if (start < 0)
                start = 0;
            if (count < 0 || count > 100)
                count = 20;

            try
            {
                return JsonConvert.DeserializeObject<List<string>>(await Request($"https://{region}.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?{startTimeStr}{endTimeStr}{queueStr}{typeStr}start={start}&count={count}&api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<MatchDTO?> GetMatchDataByMatchId(Region region, string matchId)
        {
            try
            {
                return JsonConvert.DeserializeObject<MatchDTO>(await Request($"https://{region}.api.riotgames.com/lol/match/v5/matches/{matchId}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<JObject?> GetMatchTimelineByMatchId(Region region, string matchId)
        {
            try
            {
                return JsonConvert.DeserializeObject<JObject>(await Request($"https://{region}.api.riotgames.com/lol/match/v5/matches/{matchId}/timeline?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class MatchDTO
        {
            public MetadataDTO? Metadata;
            public InfoDTO? Info;
        }

        public class MetadataDTO
        {
            public string? DataVersion;
            public string? MatchId;
            public List<string>? Participants;
        }

        public class InfoDTO
        {
            public long GameCreation;
            public long GameDuration;
            public long GameEndTimestamp;
            public long GameId;
            public string? GameMode;
            public string? GameName;
            public long GameStartTimestamp;
            public string? GameType;
            public string? GameVersion;
            public int MapId;
            public List<ParticipantDTO>? Participants;
            public string? PlatformId;
            public int QueueId;
            public List<TeamDTO>? Teams;
            public string? TournamentCode;
        }

        public class ParticipantDTO
        {
            public int Assists;
            public int BaronKills;
            public int BountyLevel;
            public int ChampExperience;
            public int ChampLevel;
            public int ChampionId;
            public string? ChampionName;
            public int ChampionTransform;
            public int ConsumablesPurchased;
            public int DamageDealtToBuildings;
            public int DamageDealtToObjectives;
            public int DamageDealtToTurrets;
            public int DamageSelfMitigated;
            public int Deaths;
            public int DetectorWardsPlaced;
            public int DoubleKills;
            public int DragonKills;
            public bool FirstBloodAssist;
            public bool FirstBloodKill;
            public bool FirstTowerAssist;
            public bool FirstTowerKill;
            public bool GameEndedInEarlySurrender;
            public bool GameEndedInSurrender;
            public int GoldEarned;
            public int GoldSpent;
            public string? IndividualPosition;
            public int InhibitorKills;
            public int InhibitorTakedowns;
            public int InhibitorsLost;
            public int Item0;
            public int Item1;
            public int Item2;
            public int Item3;
            public int Item4;
            public int Item5;
            public int Item6;
            public int ItemsPurchased;
            public int KillingSprees;
            public int Kills;
            public string? Lane;
            public int LargestCriticalStrike;
            public int LargestKillingSpree;
            public int LargestMultiKill;
            public int LongestTimeSpentLiving;
            public int MagicDamageDealt;
            public int MagicDamageDealtToChampions;
            public int MagicDamageTaken;
            public int NeutralMinionsKilled;
            public int NexusKills;
            public int NexusTakedowns;
            public int NexusLost;
            public int ObjectivesStolen;
            public int ObjectivesStolenAssists;
            public int ParticipantId;
            public int PentaKills;
            public PerksDTO? Perks;
            public int PhysicalDamageDealt;
            public int PhysicalDamageDealtToChampions;
            public int PhysicalDamageTaken;
            public int ProfileIcon;
            public string? Puuid;
            public int QuadraKills;
            public string? RiotIdName;
            public string? RiotIdTagline;
            public string? Role;
            public int SightWardsBoughtInGame;
            public int Spell1Casts;
            public int Spell2Casts;
            public int Spell3Casts;
            public int Spell4Casts;
            public int Summoner1Casts;
            public int Summoner1Id;
            public int Summoner2Casts;
            public int Summoner2Id;
            public string? SummonerId;
            public int SummonerLevel;
            public string? SummonerName;
            public bool TeamEarlySurrendered;
            public int TeamId;
            public string? TeamPosition;
            public int TimeCCingOthers;
            public int TimePlayed;
            public int TotalDamageDealt;
            public int TotalDamageDealtToChampions;
            public int TotalDamageShieldedOnTeammates;
            public int TotalDamageTaken;
            public int TotalHeal;
            public int TotalHealsOnTeammates;
            public int TotalMinionsKilled;
            public int TotalTimeCCDealt;
            public int TotalTimeSpentDead;
            public int TotalUnitsHealed;
            public int TripleKills;
            public int TrueDamageDealt;
            public int TrueDamageDealtToChampions;
            public int TrueDamageTaken;
            public int TurretKills;
            public int TurretTakedowns;
            public int TurretsLost;
            public int UnrealKills;
            public int VisionScore;
            public int VisionWardsBoughtInGame;
            public int WardsKilled;
            public int WardsPlaced;
            public bool Win;
        }

        public class TeamDTO
        {
            public List<BanDTO>? Bans;
            public ObjectivesDTO? Objectives;
            public int TeamId;
            public bool Win;
        }

        public class PerksDTO
        {
            public PerkStatsDTO? StatPerks;
            public List<PerkStyleDTO>? Styles;
        }

        public class BanDTO
        {
            public int ChampionId;
            public int PickTurn;
        }

        public class ObjectivesDTO
        {
            public ObjectiveDTO? Baron;
            public ObjectiveDTO? Champion;
            public ObjectiveDTO? Dragon;
            public ObjectiveDTO? Inhibitor;
            public ObjectiveDTO? RiftHerald;
            public ObjectiveDTO? Tower;
        }

        public class PerkStatsDTO
        {
            public int Defense;
            public int Flex;
            public int Offense;
        }

        public class PerkStyleDTO
        {
            public string? Description;
            public List<PerkStyleSelectionDTO>? Selections;
            public int Style;
        }

        public class ObjectiveDTO
        {
            public bool First;
            public int Kills;
        }

        public class PerkStyleSelectionDTO
        {
            public int Perk;
            public int Var1;
            public int Var2;
            public int Var3;
        }
    }

    #endregion

    #region SPECTATOR-V4

    public static class SpectatorV4Async
    {
        public static async Task<CurrentGameInfo?> GetCurrentGameInfoBySummonerId(Platform platform, string summonerId)
        {
            try
            {
                return JsonConvert.DeserializeObject<CurrentGameInfo>(await Request($"https://{platform}.api.riotgames.com/lol/spectator/v4/active-games/by-summoner/{summonerId}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<FeaturedGames?> GetListOfFeaturedGames(Platform platform)
        {
            try
            {
                return JsonConvert.DeserializeObject<FeaturedGames>(await Request($""))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class CurrentGameInfo
        {
            public long GameId;
            public string? GameType;
            public long GameStartTime;
            public long MapId;
            public long GameLength;
            public string? PlatformId;
            public string? GameMode;
            public List<BannedChampion>? BannedChampions;
            public long GameQueueConfigId;
            public Observer? Observers;
            public List<CurrentGameParticipant>? Participants;
        }

        public class BannedChampion
        {
            public int PickTurn;
            public long ChampionId;
            public long TeamId;
        }

        public class Observer
        {
            public string? EncryptionKey;
        }

        public class CurrentGameParticipant
        {
            public long ChampionId;
            public Perks? Perks;
            public long ProfileIconId;
            public bool Bot;
            public long TeamId;
            public string? SummonerName;
            public string? SummonerId;
            public string? Puuid;
            public long Spell1Id;
            public long Spell2Id;
            public List<GameCustomizationObject>? GameCustomizationObjects;
        }
        
        public class Perks
        {
            public List<long>? PerkIds;
            public long PerkStyle;
            public long PerkSubStyle;
        }

        public class GameCustomizationObject
        {
            public string? Category;
            public string? Content;
        }

        public class FeaturedGames
        {
            public List<FeaturedGameInfo>? GameList;
            public long ClientRefreshInterval;
        }

        public class FeaturedGameInfo
        {
            public string? GameMode;
            public long GameLength;
            public long MapId;
            public string? GameType;
            public List<BannedChampion>? BannedChampions;
            public long GameId;
            public Observer? Observers;
            public long GameQueueConfigId;
            public List<Participant>? Participants;
            public string? PlatformId;
        }

        public class Participant
        {
            public bool Bot;
            public long Spell1Id;
            public long Spell2Id;
            public long ProfileIconId;
            public string? SummonerName;
            public string? SummonerId;
            public string? puuid;
            public long ChampionId;
            public long TeamId;
        }
    }

    #endregion

    #region SUMMONER-V4

    public static class SummonerV4Async
    {
        public static async Task<SummonerDTO?> GetSummonerByRSOPuuid(Platform platform, string summonerId)
        {
            try
            {
                return JsonConvert.DeserializeObject<SummonerDTO>(await Request($"https://{platform}.api.riotgames.com/fulfillment/v1/summoners/by-puuid/{summonerId}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<SummonerDTO?> GetSummonerByAccountId(Platform platform, string accountId)
        {
            try
            {
                return JsonConvert.DeserializeObject<SummonerDTO>(await Request($"https://{platform}.api.riotgames.com/lol/summoner/v4/summoners/by-account/{accountId}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<SummonerDTO?> GetSummonerBySummonerName(Platform platform, string summonerName)
        {
            try
            {
                return JsonConvert.DeserializeObject<SummonerDTO>(await Request($"https://{platform}.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<SummonerDTO?> GetSummonerByPuuid(Platform platform, string puuid)
        {
            try
            {
                return JsonConvert.DeserializeObject<SummonerDTO>(await Request($"https://{platform}.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<SummonerDTO?> GetSummonerByAccessToken(Platform platform, string accessToken)
        {
            try
            {
                return JsonConvert.DeserializeObject<SummonerDTO>(await Request($"https://{platform}.api.riotgames.com/lol/summoner/v4/summoners/me?api_key={APIKey}", accessToken))!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<SummonerDTO?> GetSummonerBySummonerId(Platform platform, string summonerId)
        {
            try
            {
                return JsonConvert.DeserializeObject<SummonerDTO>(await Request($"https://{platform}.api.riotgames.com/lol/summoner/v4/summoners/{summonerId}?api_key={APIKey}"))!;
            }
            catch (Exception ex)
            {
                // Check Status Code

                throw new Exception(ex.Message);
            }
        }

        public class SummonerDTO
        {
            public string? AccountID;
            public int ProfileIconID;
            public long RevisionDate;
            public string? Name;
            public string? Id;
            public string? Puuid;
            public long SummonerLevel;
        }
    }

    #endregion
}