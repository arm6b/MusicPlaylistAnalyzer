using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylistAnalyzer
{
    

    class Song
    {
        public string name;
        public string artist;
        public string album;
        public string genre;
        public int size;
        public int time;
        public int year;
        public int plays;

        public Song(string name, string artist, string album, string genre, int size, int time, int year, int plays)
        {
            this.name = name;
            this.artist = artist;
            this.album = album;
            this.genre = genre;
            this.size = size;
            this.time = time;
            this.year = year;
            this.plays = plays;
        }
    }
}
