using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3Application
{
    public class DataProvider
    {
        public class Student
        {
            public int Code { get; set; }
            public DateTime Birhday { get; set; }
            public string Surname { get; set; }
            public string Group { get; set; }
        }
        public class Rating
        {
            public int Code { get; set; }
            public string LessonName { get; set; }
            public int FirstModule { get; set; }
            public int SecondModule { get; set; }
        }

        const string DataDir = @"C:\Users\dungm\source\repos\Lab_3";
        const string errorsFileName = DataDir + "errors.log";

        static Dictionary<string, string> fileNames = new Dictionary<string, string>
        {
            {"Students", DataDir + "Students.txt"},
            {"Rating", DataDir + "Rating.txt"}
        };

        public static Dictionary<int, Student> Students = new Dictionary<int, Student>();
        public static List<Rating> Ratings = new List<Rating>();

        public static bool ReadData()
        {
            Students.Clear();
            Ratings.Clear();

            var errorMessage = new StringBuilder();

            foreach (var filename in fileNames.Values)
            {
                if(!File.Exists(filename))
                {
                    errorMessage.AppendLine($"File {filename} not found. Data reading process failed");
                    File.AppendAllText(errorsFileName, errorMessage);
                    return false;
                }
            }

            var fileName = fileNames["Students"];

            using (var reader = new StreamReader(DataDir + "students.txt"))
            {
                var lineNumber = 0;
                while (!reader.EndOfStream)
                {
                    var words = reader.ReadLine().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    lineNumber++;
                    var code = int.Parse(words[0]);
                    Students[code] = new Student { Code = code };
                }
            }
            return true;
        }
    }
}
