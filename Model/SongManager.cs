using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1MusicPlayer.Model
{
    public static class SongManager
    {
        private static List<Song> getSongs()
        {
            var songs = new List<Song>();

            Album album1 = new Album("Alai", $"Assets/ImageFile/Alai.png");
            Album album2 = new Album("AEM", $"Assets/ImageFile/AEM.png");
        
            songs.Add(new Song("Alaipayuthey", "Alaipayuthey-Kanna.mp3", album1, new TimeSpan(0,3,41)));
            songs.Add(new Song("Endrendrum", "Endrendrum-Punnagai.mp3",album1, new TimeSpan(0,3,57)));

            songs.Add(new Song("IdhuNaal", "IdhuNaal.mp3", album2, new TimeSpan(0,3,39)));
            songs.Add(new Song("Rasaali", "Rasaali.mp3", album2, new TimeSpan(0,5,38)));

            return songs;
        }
        public static void GetAllSongs(ObservableCollection<Song> songs)
        {
            var allSongs = getSongs();
            songs.Clear();
            allSongs.ForEach(s => songs.Add(s));
        }
        public static void SearchSongbyName(ObservableCollection<Song> songs, string songName) // Search songs By Name
        {
            var allSongs = getSongs();
            songs.Clear();
            var filteredSongs = allSongs.Where(s => s.SongName.ToLower().Contains(songName.ToLower())).ToList();

            filteredSongs.ForEach(s => songs.Add(s));
        }
        public static void FilterSongAlbumName(ObservableCollection<Song> songs, string albumName) // Search songs By Name
        {
            var allSongs = getSongs();
            songs.Clear();
            var filteredSongs = allSongs.Where(s => s.Album.AlbumName.ToLower().Contains(albumName.ToLower())).ToList();

            filteredSongs.ForEach(s => songs.Add(s));
        }
    }
}
