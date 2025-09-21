using System;
using System.Drawing.Printing;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC._2016;

public class Day04 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc4.txt");
        long res_1 = 0, res_2 = 0;

        var encrypted_rooms = File.ReadAllLines(file_name);

        foreach (var encrypted_room in encrypted_rooms)
        {
            var split_encrypted_room = encrypted_room.Split('[');

            var split_rooms = split_encrypted_room[0].Split('-');

            var rooms = split_rooms[..^1];
            var sector_id = int.Parse(split_rooms[^1]);
            var checksum = split_encrypted_room[1][..^1];

            Dictionary<char, int> freq = new();
            StringBuilder sb = new();

            foreach (var room in rooms)
            {
                int offset = sector_id % 26;

                foreach (var letter in room)
                {
                    if (!freq.TryAdd(letter, 1))
                        freq[letter]++;

                    int new_letter = (letter - 'a' + offset) % 26;
                    sb.Append((char)(new_letter + 'a'));
                }
                sb.Append(' ');
            }

            var checksum_arr = freq
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .Take(5)
                .Select(x => x.Key)
                .ToArray();

            var check = new string(checksum_arr);

            if (check.Equals(checksum))
                res_1 += sector_id;

            if (sb.ToString().Contains("north"))
                res_2 = sector_id;
        }

        return (res_1, res_2);
    }
}