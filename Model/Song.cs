using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1MusicPlayer.Model
{
    public class Song
    {
        public string SongName { get; set; }
        public string AudioFile { get; set; }
        public TimeSpan Length { get; set; }
        public Album Album { get; set; }

        public Song(string songName,string audioFile,Album album)
        {
            SongName = songName;
            AudioFile = audioFile;
            Album = album;
        }
    }
}
