using System.Net.Http.Headers;

#region ENUMS

public enum Platform
{
    BR1,
    EUN1,
    EUW1,
    JP1,
    KR,
    LA1,
    LA2,
    NA1,
    OC1,
    PH2,
    RU,
    SG2,
    TH2,
    TR1,
    TW2,
    VN2
}

public enum Region
{
    AMERICAS,
    ASIA,
    ESPORTS,
    EUROPE,
    SEA
}

public enum ValRegion
{
    AP,
    BR,
    ESPORTS,
    EU,
    KR,
    LATAM,
    NA
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

public enum Game
{
    val,
    lor
}

public enum Queue
{
    RANKED_SOLO_5x5,
    RANKED_TFT,
    RANKED_FLEX_SR,
    RANKED_FLEX_TT
}

public enum ValQueue
{
    competitive,
    unrated,
    spikerush,
    tournamentmode,
    deathmatch,
    onefa,
    ggteam
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

#endregion

public static class RiotAPI
{
    private static HttpClient Client = new HttpClient();
    private static HttpClient ClientWithAuth = new HttpClient();
    public static string? APIKey { get; set; }

    #region RequestMethod

    public static Task<string> Request(string APIUrl, string? accessToken = null)
    {
        if (accessToken == null)
            return Client.GetStringAsync(APIUrl + APIKey);
        else
        {
            // Add an Authorisation header to the GET request containing the access token
            ClientWithAuth.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", accessToken);
            return Client.GetStringAsync(APIUrl);
        }
    }

    #endregion

    #region ConvertLocale

    public static string ConvertLocale(Locale locale)
    {
        switch (locale)
        {
            case Locale.cs_CZ:
                return "cs-CZ";
            case Locale.el_GR:
                return "el-GR";
            case Locale.pl_PL:
                return "pl-PL";
            case Locale.ro_RO:
                return "ro-RO";
            case Locale.hu_HU:
                return "hu-HU";
            case Locale.en_GB:
                return "en-GB";
            case Locale.de_DE:
                return "de-DE";
            case Locale.es_ES:
                return "es-ES";
            case Locale.it_IT:
                return "it-IT";
            case Locale.fr_FR:
                return "fr-FR";
            case Locale.ja_JP:
                return "ja-JP";
            case Locale.ko_KR:
                return "ko-KR";
            case Locale.es_MX:
                return "es-MX";
            case Locale.es_AR:
                return "es-AR";
            case Locale.pt_BR:
                return "pt-BR";
            case Locale.en_US:
                return "en-US";
            case Locale.en_AU:
                return "en-AU";
            case Locale.ru_RU:
                return "ru-RU";
            case Locale.tr_TR:
                return "tr-TR";
            case Locale.ms_MY:
                return "ms-MY";
            case Locale.en_PH:
                return "en-PH";
            case Locale.en_SG:
                return "en-SG";
            case Locale.th_TH:
                return "th-TH";
            case Locale.vn_VN:
                return "vn-VN";
            case Locale.id_ID:
                return "id-ID";
            case Locale.zh_MY:
                return "zh-MY";
            case Locale.zh_CN:
                return "zh-CN";
            case Locale.zh_TW:
                return "zh-TW";
            default:
                return "en-US";
        }
    }

    #endregion

    #region Account-V1

    public static string GetAccountByPUUID(Region region, string puuid)
    {
        return Request($"https://{region}.api.riotgames.com/riot/account/v1/accounts/by-puuid/{puuid}?api_key=").Result;
    }

    public static string GetAccountByRiotID(Region region, string gameName, string tagLine)
    {
        return Request($"https://{region}.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}?api_key=").Result;
    }

    public static string GetAccountByAccessToken(Region region, string accessToken)
    {
        return Request($"https://{region}.api.riotgames.com/riot/account/v1/accounts/me?api_key=").Result;
    }

    public static string GetActiveByPUUID(Region region, Game game, string puuid)
    {
        return Request($"https://{region}.api.riotgames.com/riot/account/v1/active-shards/by-game/{game}/by-puuid/{puuid}?api_key=").Result;
    }

    #endregion

    #region Champion-Mastery-V4

    public static string GetChampionMasteryByPUUID(Platform region, string puuid)
    {
        return Request($"https://{region}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}?api_key=").Result;
    }

    public static string GetChampionMasteryByPUUIDForChampion(Platform region, string puuid, string cid)
    {
        return Request($"https://{region}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}/by-champion/{cid}?api_key=").Result;
    }

    public static string GetChampionMasteryByPUUIDTop(Platform region, string puuid, int count = 0)
    {
        if (count == 0)
            return Request($"https://{region}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}/top?api_key=").Result;
        return Request($"https://{region}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}/top?count={count}&api_key=").Result;
    }

    public static string GetChampionMasteryBySummonerID(Platform region, string sid)
    {
        return Request($"https://{region}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/{sid}?api_key=").Result;
    }

    public static string GetChampionMasteryBySummonerIDForChampion(Platform region, string sid, string cid)
    {
        return Request($"https://{region}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/{sid}/by-champion/{cid}?api_key=").Result;
    }

    public static string GetChampionMasteryBySummonerIDTop(Platform region, string sid, int count = 0)
    {
        if (count == 0)
            return Request($"https://{region}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/{sid}/top&api_key=").Result;
        return Request($"https://{region}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/{sid}/top?count={count}&api_key=").Result;
    }

    public static string GetChampionMasteryScoreByPUUID(Platform region, string PUUID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/champion-mastery/v4/scores/by-puuid/{PUUID}?api_key=").Result;
    }

    public static string GetChampionMasteryScoreBySummonerID(Platform region, string sid)
    {
        return Request($"https://{region}.api.riotgames.com/lol/champion-mastery/v4/scores/by-summoner/{sid}?api_key=").Result;
    }

    #endregion

    #region Champion-V3

    public static string GetChampionRotation(Platform region)
    {
        return Request($"https://{region}.api.riotgames.com/lol/platform/v3/champion-rotations?api_key=").Result;
    }

    #endregion

    #region Clash-V1

    public static string GetClashPlayersByPUUID(Platform region, string PUUID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/clash/v1/players/by-puuid/{PUUID}?api_key=").Result;
    }

    public static string GetClashPlayersBySummonerID(Platform region, string SID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/clash/v1/players/by-summoner/{SID}?api_key=").Result;
    }

    public static string GetClashTeamByTeamID(Platform region, string teamID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/clash/v1/teams/{teamID}?api_key=").Result;
    }

    public static string GetClashTournaments(Platform region)
    {
        return Request($"https://{region}.api.riotgames.com/lol/clash/v1/tournaments?api_key=").Result;
    }

    public static string GetClashTournamentsByTeamID(Platform region, string teamID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/clash/v1/tournaments/{teamID}?api_key=").Result;
    }

    public static string GetClashTournamentsByTournamentID(Platform region, string tournamentID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/clash/v1/tournaments/{tournamentID}?api_key=").Result;
    }

    #endregion

    #region League-Exp-V4

    public static string GetLeaguePlayersByQueueTierDivision(Platform region, Queue queue, Tier tier, Division division)
    {
        return Request($"https://{region}.api.riotgames.com/lol/league-exp/v4/entries/{queue}/{tier}/{division}?api_key=").Result;
    }

    #endregion

    #region League-V4

    public static string GetChallengerLeagueByQueue(Platform region, Queue queue)
    {
        return Request($"https://{region}.api.riotgames.com/lol/league/v4/challengerleagues/by-queue/{queue}?api_key=").Result;
    }

    public static string GetLeagueEntriesInAllQueuesBySummonerID(Platform region, string sid)
    {
        return Request($"https://{region}.api.riotgames.com/lol/league/v4/entries/by-summoner/{sid}?api_key=").Result;
    }

    public static string GetLeagueEntriesByQueueTierDivision(Platform region, Queue queue, Tier tier, Division division)
    {
        return Request($"https://{region}.api.riotgames.com/lol/league/v4/entries/{queue}/{tier}/{division}?api_key=").Result;
    }

    public static string GetGrandmasterLeagueByQueue(Platform region, Queue queue)
    {
        return Request($"https://{region}.api.riotgames.com/lol/league/v4/grandmasterleagues/by-queue/{queue}?api_key=").Result;
    }

    public static string GetLeagueByLeagueID(Platform region, string leagueID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/league/v4/leagues/{leagueID}?api_key=").Result;
    }

    public static string GetMasterLeagueByQueue(Platform region, Queue queue)
    {
        return Request($"https://{region}.api.riotgames.com/lol/league/v4/masterleagues/by-queue/{queue}?api_key=").Result;
    }

    #endregion

    #region LOL-CHALLENGES-V1

    public static string GetChallengesConfig(Platform region)
    {
        return Request($"https://{region}.api.riotgames.com/lol/challenges/v1/challenges/config?api_key=").Result;
    }

    public static string GetChallengesPercentiles(Platform region)
    {
        return Request($"https://{region}.api.riotgames.com/lol/challenges/v1/challenges/percentiles?api_key=").Result;
    }

    public static string GetSpecificChallengeConfig(Platform region, long challengeID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/challenges/v1/challenges/{challengeID}/config?api_key=").Result;
    }

    public static string GetChallengesTopPlayersByLevel(Platform region, long challengeID, ChallengeLevel level, int limit = 0)
    {
        if (limit == 0)
            return Request($"https://{region}.api.riotgames.com/lol/challenges/v1/challenges/{challengeID}/top/{level}?api_key=").Result;
        return Request($"https://{region}.api.riotgames.com/lol/challenges/v1/challenges/{challengeID}/top/{level}?limit={limit}&api_key=").Result;
    }

    public static string GetSpecificChallengePercentiles(Platform region, long challengeID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/challenges/v1/challenges/{challengeID}/percentiles?api_key=").Result;
    }

    public static string GetPlayerChallengesData(Platform region, string puuid)
    {
        return Request($"https://{region}.api.riotgames.com/lol/challenges/v1/player-data/{puuid}?api_key=").Result;
    }

    #endregion

    #region LOL-STATUS-V4

    public static string GetLeagueStatus(Platform region)
    {
        return Request($"https://{region}.api.riotgames.com/lol/status/v4/platform-data?api_key=").Result;
    }

    #endregion

    #region LOR-DECK-V1

    public static string GetDeckListByAccessToken(Region region, string accessToken)
    {
        return Request($"https://{region}.api.riotgames.com/lor/deck/v1/decks/me?api_key=", accessToken).Result;
    }

    // Unable to Test Create New Deck Endpoint, Unsure how to Implement

    #endregion

    #region LOR-INVENTORY-V1

    public static string GetLorInventoryByAccessToken(Region region, string accessToken)
    {
        return Request($"https://{region}.api.riotgames.com/lor/inventory/v1/players/me/inventory?api_key=", accessToken).Result;
    }

    #endregion

    #region LOR-MATCH-V1

    public static string GetLorMatchIDsByPUUID(Region region, string PUUID)
    {
        return Request($"https://{region}.api.riotgames.com/lor/match/v1/matches/by-puuid/{PUUID}/ids?api_key=").Result;
    }

    public static string GetLorMatchByMatchID(Region region, string matchID)
    {
        return Request($"https://{region}.api.riotgames.com/lor/match/v1/matches/{matchID}?api_key=").Result;
    }

    #endregion

    #region LOR-RANKED-V1

    public static string GetPlayersInMasterTier(Region region)
    {
        return Request($"https://{region}.api.riotgames.com/lor/ranked/v1/leaderboards?api_key=").Result;
    }

    #endregion

    #region LOR-STATUS-V1

    public static string GetLorStatus(Region region)
    {
        return Request($"https://{region}.api.riotgames.com/lor/status/v1/platform-data?api_key=").Result;
    }

    #endregion

    #region MATCH-V5

    public static string GetMatchesbyPUUID(Region region, string PUUID, long startTime, long endTime, int queue, string? type, int startingPoint, int numOfGames)
    {
        var APIUrl = $"https://{region}.api.riotgames.com/lol/match/v5/matches/by-puuid/{PUUID}/ids?";

        if (startTime != 0)
            APIUrl += $"startTime={startTime}&";
        if (endTime != 0)
            APIUrl += $"endTime={endTime}&";
        if (queue != 0)
            APIUrl += $"queue={queue}&";
        if (type != null)
            APIUrl += $"type={type}&";

        return Request(APIUrl + "&api_key=").Result;
    }

    public static string GetMatchesbyMatchID(Region region, string matchID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/match/v5/matches/{matchID}?api_key=").Result;
    }

    public static string GetMatchTimelinebyMatchID(Region region, string matchID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/match/v5/matches/{matchID}/timeline?api_key=").Result;
    }

    #endregion

    #region SPECTATOR-V4

    public static string GetCurrentGameInfoBySummonerID(Platform region, string SID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/spectator/v4/active-games/by-summoner/{SID}?api_key=").Result;
    }

    public static string GetFeaturedGames(Platform region)
    {
        return Request($"https://{region}.api.riotgames.com/lol/spectator/v4/featured-games?api_key=").Result;
    }

    #endregion

    #region SUMMONER-V4

    public static string GetSummonerByAccountID(Platform region, string accountID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-account/{accountID}?api_key=").Result;
    }

    public static string GetSummonerBySummonerName(Platform region, string summonerName)
    {
        return Request($"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName}?api_key=").Result;
    }

    public static string GetSummonerByPUUID(Platform region, string PUUID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{PUUID}?api_key=").Result;
    }

    public static string GetSummonerBySummonerID(Platform region, string summonerID)
    {
        return Request($"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/{summonerID}?api_key=").Result;
    }

    #endregion

    #region TFT-LEAGUE-V1

    public static string GetTFTChallengerLeague(Platform region)
    {
        return Request($"https://{region}.api.riotgames.com/tft/league/v1/challenger?api_key=").Result;
    }

    public static string GetTFTLeagueEntriesBySummonerID(Platform region, string SID)
    {
        return Request($"https://{region}.api.riotgames.com/tft/league/v1/entries/by-summoner/{SID}?api_key=").Result;
    }

    public static string GetTFTLeagueEntriesByTierDivision(Platform region, Tier tier, Division division)
    {
        return Request($"https://{region}.api.riotgames.com/tft/league/v1/entries/{tier}/{division}?api_key=").Result;
    }

    public static string GetTFTGrandmasterLeague(Platform region)
    {
        return Request($"https://{region}.api.riotgames.com/tft/league/v1/grandmaster?api_key=").Result;
    }

    public static string GetTFTLeagueByLeagueID(Platform region, string leagueID)
    {
        return Request($"https://{region}.api.riotgames.com/tft/league/v1/leagues/{leagueID}?api_key=").Result;
    }

    public static string GetTFTMasterLeague(Platform region)
    {
        return Request($"https://{region}.api.riotgames.com/tft/league/v1/master?api_key=").Result;
    }

    public static string GetTFTTopRatedLadderForQueue(Platform region, Queue queue)
    {
        return Request($"https://{region}.api.riotgames.com/tft/league/v1/rated-ladders/{queue}?api_key=").Result;
    }

    #endregion

    #region TFT-MATCH-V1

    public static string GetTFTMatchIDsByPUUID(Platform region, string PUUID, int count = 0)
    {
        if (count == 0)
            return Request($"https://{region}.api.riotgames.com/tft/match/v1/matches/by-puuid/{PUUID}/ids?api_key=").Result;
        return Request($"https://{region}.api.riotgames.com/tft/match/v1/matches/by-puuid/{PUUID}/ids?count={count}&api_key=").Result;
    }

    public static string GetTFTMatchByMatchID(Platform region, string matchID)
    {
        return Request($"https://{region}.api.riotgames.com/tft/match/v1/matches/{matchID}?api_key=").Result;
    }

    #endregion

    #region TFT-STATUS-V1

    public static string GetTFTStatus(Platform region)
    {
        return Request($"https://{region}.api.riotgames.com/tft/status/v1/platform-data?api_key=").Result;
    }

    #endregion

    #region TFT-SUMMONER-V1

    public static string GetTFTSummonerByAccountID(Platform region, string accountID)
    {
        return Request($"https://{region}.api.riotgames.com/tft/summoner/v1/summoners/by-account/{accountID}?api_key=").Result;
    }

    public static string GetTFTSummonerBySummonerName(Platform region, string summonerName)
    {
        return Request($"https://{region}.api.riotgames.com/tft/summoner/v1/summoners/by-name/{summonerName}?api_key=").Result;
    }

    public static string GetTFTSummonerByPUUID(Platform region, string PUUID)
    {
        return Request($"https://{region}.api.riotgames.com/tft/summoner/v1/summoners/by-puuid/{PUUID}?api_key=").Result;
    }

    public static string GetTFTSummonerByAccessToken(Platform region, string accessToken)
    {
        return Request($"https://{region}.api.riotgames.com/tft/summoner/v1/summoners/me?api_key=", accessToken).Result;
    }

    public static string GetTFTSummonerBySummonerID(Platform region, string summonerID)
    {
        return Request($"https://{region}.api.riotgames.com/tft/summoner/v1/summoners/{summonerID}?api_key=").Result;
    }

    #endregion

    #region TOURNAMENT-STUB-V4

    // Mock Endpoint

    #endregion

    #region TOURNAMENT-V4

    // Don't have Access

    #endregion

    #region VAL-CONTENT-V1

    public static string GetContentByLocale(ValRegion region, Locale locale)
    {
        return Request($"https://{region}.api.riotgames.com/val/content/v1/contents?locale={ConvertLocale(locale)}&api_key=").Result;
    }

    #endregion

    #region VAL-MATCH-V1

    public static string GetValMatchByMatchID(ValRegion region, string matchID)
    {
        return Request($"https://{region}.api.riotgames.com/val/match/v1/matches/{matchID}?api_key=").Result;
    }

    public static string GetValMatchlistByPUUID(ValRegion region, string puuid)
    {
        return Request($"https://{region}.api.riotgames.com/val/match/v1/matchlists/by-puuid/{puuid}?api_key=").Result;
    }

    public static string GetValRecentMatchesByQueue(ValRegion region, ValQueue queue)
    {
        return Request($"https://{region}.api.riotgames.com/val/match/v1/recent-matches/by-queue/{queue}?api_key=").Result;
    }

    #endregion

    #region VAL-RANKED-V1

    public static string GetValLeaderboardByAct(ValRegion region, string actID, int size = 200, int startIndex = 0)
    {
        return Request($"https://{region}.api.riotgames.com/val/ranked/v1/leaderboards/by-act/{actID}?size={size}&startIndex={startIndex}&api_key=").Result;
    }

    #endregion

    #region VAL-STATUS-V1

    public static string GetValStatus(ValRegion region)
    {
        return Request($"https://{region}.api.riotgames.com/val/status/v1/platform-data?api_key=").Result;
    }

    #endregion
}