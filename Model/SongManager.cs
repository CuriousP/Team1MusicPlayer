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

            Album album1 = new Album("Alai", "Alai.png");
            Album album2 = new Album("AEM", "AEM.png");
        
            songs.Add(new Song("Alaipayuthey", "Alaipayuthey-Kanna.mp3", album1));
            songs.Add(new Song("Endrendrum", "Endrendrum-Punnagai.mp3",album1));

            songs.Add(new Song("IdhuNaal", "IdhuNaal.mp3", album2));
            songs.Add(new Song("Rasaali", "Rasaali.mp3", album2));

            return songs;
        }
        public static void GetAllSongs(ObservableCollection<Song> songs)
        {
            var allSongs = getSongs();
            songs.Clear();
            allSongs.ForEach(s => songs.Add(s));
        }

        public static void SearchSongByName(ObservableCollection<Song> songs, string songName) // Search songs By Name
        {
            var allSongs = getSongs();
            songs.Clear();
            var filteredSongs = allSongs.Where(s => s.SongName.ToLower().Contains(songName.ToLower())).ToList();

            filteredSongs.ForEach(s => songs.Add(s));
        }
    }
}
