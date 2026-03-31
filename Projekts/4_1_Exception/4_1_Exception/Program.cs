namespace _4_1_Exception;

internal class Program
{
    static object locker = new object();
    static string path = "D:\\Users\\DotNet\\Modul_4\\Projekts\\4_1_Exception\\4_1_Exception\\TEST\\";
    static void Main(string[] args)
    {
        for (int i = 10; i <= 19; i++)
        {
            Thread thread = new Thread(Display);
            thread.Start(i);
        }
    }
    static void Display(object num)
    {

        int number = (int)num;
        for (int i = 0; i < number; i++)
        {
            lock (locker)
            {
                string newPath = Path.Combine(path, $"{i}_{number}");
                Directory.CreateDirectory(newPath);
                var file = File.Create(Path.Combine(newPath, $"_{i}_{number}.txt"));
                file.Close();   
                       File.WriteAllText(Path.Combine(newPath, $"_{i}_{number}.txt"), $"Salom{i}_{number}_");
                
            }

        }
    }
}
