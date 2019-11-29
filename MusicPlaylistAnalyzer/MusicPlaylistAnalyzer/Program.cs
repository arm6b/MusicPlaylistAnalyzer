using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MusicPlaylistAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fields;
            int row = 1;
            List<Song> SongList = new List<Song>();
            int[] intfields = new int[4];
            string line;
            StreamReader reader;
            StreamWriter writer = null;
            const int NUM_FIELDS = 8;
            string[] names = { "Name", "Artist", "Album", "Genre", "Size", "Time", "Year", "Plays" };

            if (args.Length != 2)
            {
                Console.WriteLine("Usage: <program name> <playlist file> <playlist report file>");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Error: playlist file {0} could not be found.", args[0]);
                return;
            }
            else
            {
                reader = new StreamReader(args[0]);
            }
            reader.ReadLine();
            line = reader.ReadLine();

            while (line != null)
            {
                fields = line.Split('\t');
                if (fields.Length != NUM_FIELDS)
                {
                    Console.WriteLine("Row {0} contains {1} values. It should contain {2}", row, fields.Length, NUM_FIELDS);
                }
                else
                {

                    for (int i = 4; i < NUM_FIELDS; i++)
                    {
                        
                            if (!Int32.TryParse(fields[i], out intfields[i-4]))
                            {
                                Console.WriteLine(names[i] + "needs to be a number, you entered: " + fields[i]);
                               
                            }
                            else
                            {
                                intfields[i-4] = Int32.Parse(fields[i]);
                            }
                        
                    }

                    SongList.Add(new Song(fields[0], fields[1], fields[2], fields[3],
                                                      intfields[0], intfields[1], intfields[2],
                                                      intfields[3]));
                    row++;
                }

                line = reader.ReadLine();
            }
            reader.Close();
            if (SongList.Count == 0)
            {
                Console.WriteLine("Playlist file does not contain any rows of valid data, run the program again with a file that contains rows with valid data to see a report");
            }
            else
            {
                try
                {
                    writer = new StreamWriter(args[1]);
                }
                catch(System.IO.IOException ioe)
                {
                    Console.WriteLine("Error: {0} playlist report file could not be opened.", args[1]);
                }

                try
                {
                    var ans1 = from song in SongList where song.plays >= 200 select song;
                    writer.WriteLine("Songs that have 200 or more plays: ");
                    foreach (var item in ans1)
                    {
                        Song song = (Song)item;
                        writer.WriteLine("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", song.name, song.artist, song.album, song.genre, song.size, song.time, song.year, song.plays);
                    }

                    var ans2 = from song in SongList where song.genre == "Alternative" select song;
                    int ans2_total = ans2.Count();
                    writer.WriteLine("Number of Alternative songs: " + ans2_total);

                    var ans3 = from song in SongList where song.genre == "Hip-Hop/Rap" select song;
                    int ans3_total = ans3.Count();
                    writer.WriteLine("Number of Hip-Hop/Rap songs: " + ans3_total);

                    var ans4 = from song in SongList where song.album == "Welcome to the Fishbowl" select song;
                    writer.WriteLine("Songs from the album Welcome to the Fishbowl");
                    foreach (var item in ans4)
                    {
                        Song song = (Song)item;
                        writer.WriteLine("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", song.name, song.artist, song.album, song.genre, song.size, song.time, song.year, song.plays);
                    }

                    var ans5 = from song in SongList where song.year < 1970 select song;
                    writer.WriteLine("Songs from before 1970: ");
                    foreach (var item in ans5)
                    {
                        Song song = (Song)item;
                        writer.WriteLine("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", song.name, song.artist, song.album, song.genre, song.size, song.time, song.year, song.plays);
                    }

                    var ans6 = from song in SongList where song.name.Length > 85 select song.name;
                    writer.WriteLine("Song names longer than 85 characters: ");
                    writer.WriteLine(String.Join(",", ans6));

                    var ans7 = from song in SongList orderby song.time descending select song;
                    writer.WriteLine("Longest song: ");
                    Song ans7song = ans7.First();
                    writer.WriteLine("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", ans7song.name, ans7song.artist, ans7song.album, ans7song.genre, ans7song.size, ans7song.time, ans7song.year, ans7song.plays);
                    writer.Close();
                }
                catch(System.IO.IOException ioe)
                {
                    Console.WriteLine("An error occurred when writing to the playlist report file {0}.", args[1]);
                }
            }
        }
    }
}
