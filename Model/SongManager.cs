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
            //Create list of songs 
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
    }
}
