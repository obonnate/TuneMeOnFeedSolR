using Newtonsoft.Json.Linq;
using System;

namespace FeedIndex
{
    internal class DeezerService
    {
        public string? Token { get; private set; }
        public readonly DeezerHelper _deezerHelper;
        private readonly string _applicationId;
        private readonly string _applicationSecretKey;

        public DeezerService(DeezerHelper deezerHelper, string applicationId, string applicationSecretKey)
        {
            _deezerHelper = deezerHelper;
            _applicationId = applicationId;
            _applicationSecretKey = applicationSecretKey;
            Token = RefreshToken();
        }

        private string? RefreshToken() => _deezerHelper.GetAccessToken(_applicationId, _applicationSecretKey)?.Result?.Token;

        public async Task<Artist?> GetArtist(string token, int artistId) => await _deezerHelper.Load<Artist>(token, $"/artist/{artistId}");
        public async Task<Album?> GetAlbum(string token, int albumId) => await _deezerHelper.Load<Album>(token, $"/album/{albumId}");
        public async Task<Track?> GetTrack(string token, int trackId) => await _deezerHelper.Load<Track>(token, $"/track/{trackId}");
        public async Task<Genre?> GetGenre(string token, int trackId) => await _deezerHelper.Load<Genre>(token, $"/genre/{trackId}");
        public async Task<Track[]?> GetTracksByGenre(int genreId)
        {
            var tracks = new List<Track>();
            Token ??= RefreshToken();
            if (Token is null)
                throw new NullReferenceException("Could not aquire Token");
            var artists = await _deezerHelper.LoadArray<Artist>(Token, $"/genre/{genreId}/artists");
            foreach (var artist in (artists ?? Array.Empty<Artist>()).Where(_ => _?.tracklist != null))
            {
                var topTracks = await _deezerHelper.LoadArray<Track>(Token, artist.tracklist!);
                if (topTracks != null)
                    tracks.AddRange(topTracks);
            }
            return tracks.ToArray();
        }
    }
}
