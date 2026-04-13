using Question.Api.Entiti;
using System.Text.Json;

namespace Question.Api.Repository
{
    public class QuestionRepository : IQuestionRepository
    {

        private readonly string FilePath;
        public QuestionRepository()
        {
            FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Question.json");
            var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");


            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }


            if (!File.Exists(FilePath))
            {

                var stream = File.Create(FilePath);
                stream.Close();
            }
        }

        public List<Questionn>? GetAll()
        {
            var json = File.ReadAllText(FilePath);

            if (string.IsNullOrEmpty(json))
            {
                return new List<Questionn>();
            }

            var users = JsonSerializer.Deserialize<List<Questionn>>(json);
            return users;
        }

        public void SaveAll(List<Questionn> users)
        {
            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(FilePath, json);
        }



    }
}
