﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IGDB.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestEase;

namespace IGDB
{
  [Header("Accept", "application/json")]
  public interface IGDBApi
  {
    /// <summary>
    /// Your secret, private IGDB API token
    /// </summary>
    /// <value></value>
    [Header("user-key")]
    string ApiKey { get; set; }

    /// <summary>
    /// Queries a standard IGDB endpoint with an APIcalypse query. See endpoints in <see cref="IGDB.Client.Endpoints" />.
    /// </summary>
    /// <param name="endpoint">The IGDB endpoint name to query, see <see cref="IGDB.Client.Endpoints" /></param>
    /// <param name="query">The APIcalypse query to send</param>
    /// <typeparam name="T">The IGDB.Models.* entity to deserialize the response for.</typeparam>
    /// <returns>Array of IGDB models of the specified type</returns>
    [Post("/{endpoint}")]
    Task<T[]> QueryAsync<T>([Path]string endpoint, [Body] string query = null);

    /// <summary>
    /// For authenticated requests, get authenticated user info
    /// </summary>
    /// <returns></returns>
    [Get("/" + Client.Endpoints.Private.Me)]
    Task<Me> GetPrivateMe();

    /// <summary>
    /// Returns your API key status with usage information
    /// </summary>
    /// <returns></returns>
    [Get("/api_status")]
    Task<ApiStatus[]> GetApiStatus();
  }

  public static class Client
  {

    public static JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings()
    {
      Converters = new List<JsonConverter>() {
          new IdentityConverter(),
          new UnixTimestampConverter()
        },
      ContractResolver = new DefaultContractResolver()
      {
        NamingStrategy = new SnakeCaseNamingStrategy()
      }
    };

    /// <summary>
    /// Create a default IGDB API client with specified API key
    /// </summary>
    /// <param name="apiKey">Your private IGDB API key. Keep it secret, keep it safe!</param>
    /// <returns></returns>
    public static IGDBApi Create(string apiKey)
    {
      return Create(apiKey, new RestClient("https://api-v3.igdb.com"));
    }

    /// <summary>
    /// Create a IGDB API client based on a custom-created RestEase client. Adds required
    /// JSON serializer settings on top of any existing settings.
    /// </summary>
    /// <param name="apiKey">Your private IGDB API key. Keep it secret, keep it safe!</param>
    /// <param name="client">A custom RestEase.RestClient</param>
    /// <returns></returns>
    public static IGDBApi Create(string apiKey, RestClient client)
    {
      client.JsonSerializerSettings = DefaultJsonSerializerSettings;

      var api = client.For<IGDBApi>();
      api.ApiKey = apiKey;
      return api;
    }

    public static class Endpoints
    {
      public const string Achievements = "achievements";
      public const string AchievementIcons = "achievement_icons";
      public const string AgeRating = "age_ratings";
      public const string AgeRatingContentDescriptions = "age_rating_content_descriptions";
      public const string AlternativeNames = "alternative_names";
      public const string Artworks = "artworks";
      public const string Characters = "characters";
      public const string CharacterMugShots = "character_mug_shots";
      public const string Collections = "collections";
      public const string Companies = "companies";
      public const string CompanyWebsites = "company_websites";
      public const string Covers = "covers";
      public const string ExternalGames = "external_games";
      public const string Feeds = "feeds";
      public const string Franchies = "franchises";
      public const string Games = "games";
      public const string GameEngines = "game_engines";
      public const string GameEngineLogos = "game_engine_logos";
      public const string GameVersions = "game_versions";
      public const string GameModes = "game_modes";
      public const string GameVersionFeatures = "game_version_features";
      public const string GameVersionFeatureValues = "game_version_feature_values";
      public const string GameVideos = "game_videos";
      public const string Genres = "genres";
      public const string InvolvedCompanies = "involved_companies";
      public const string Keywords = "keywords";
      public const string MultiplayerModes = "multiplayer_modes";
      public const string Pages = "pages";
      public const string PageBackgrounds = "page_backgrounds";
      public const string PageLogos = "page_logos";
      public const string Platforms = "platforms";
      public const string PlatformLogos = "platform_logos";
      public const string PlatformVersions = "platform_versions";
      public const string PlatformVersionCompanies = "platform_version_companies";
      public const string PlatformVersionReleaseDates = "platform_version_release_dates";
      public const string PlatformWebsites = "platform_websites";
      public const string PlayerPerspectives = "player_perspectives";
      public const string ProductFamilies = "product_families";
      public const string Pulses = "pulses";
      public const string PulseGroups = "pulse_groups";
      public const string PulseSources = "pulse_sources";
      public const string PulseUrls = "pulse_urls";
      public const string ReleaseDates = "release_dates";
      public const string Screenshots = "screenshots";
      public const string Search = "search";
      public const string Themes = "themes";
      public const string Titles = "titles";
      public const string TimeToBeats = "time_to_beats";
      public const string Websites = "websites";

      public static class Private
      {
        public const string FeedFollows = "private/feed_follows";
        public const string Follows = "private/follows";
        public const string Lists = "private/lists";
        public const string ListEntries = "private/list_entries";
        public const string Me = "private/me";
        public const string Rates = "private/rates";
        public const string Reviews = "private/reviews";
        public const string ReviewVideos = "private/review_videos";
      }
    }
  }
}
