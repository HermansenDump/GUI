using System.Collections.Generic;
using BabyNames03.Models;

namespace BabyNames03.Utility
{
    public class Utility
    {
        public static List<BabyName> ReadBabyNames(string filename)
        {
            var babyNames = new List<BabyName>();

            using(var reader = new System.IO.StreamReader(filename))
            {
                var input = reader.ReadLine();
                while (input != null)
                {
                    babyNames.Add(new BabyName(input));
                    input = reader.ReadLine();
                }
            }

            return babyNames;
        }
    }
}
