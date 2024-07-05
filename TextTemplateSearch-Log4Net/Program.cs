using log4net;
using log4net.Config;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace TextTemplateSearchApp
{
    class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            logger.Info("TextTemplateSearchApp started.");

            if (args.Length != 2)
            {
                Console.WriteLine("Usage: TextTemplateSearchApp <file-path> <template>");
                logger.Error("Invalid arguments. Expected <file-path> and <template>.");
                return;
            }

            string filePath = args[0];
            string template = args[1];

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                logger.Error($"File not found: {filePath}");
                return;
            }

            string fileContent = File.ReadAllText(filePath);
            string[] sentences = fileContent.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            var result = template switch
            {
                "small_letter" => sentences.Where(s => s.Any(char.IsLower)),
                "number" => sentences.Where(s => s.Any(char.IsDigit)),
                "capital_letter" => sentences.Where(s => s.Any(char.IsUpper)),
                _ => null
            };

            if (result == null)
            {
                Console.WriteLine("Invalid template.");
                logger.Error($"Invalid template: {template}");
                return;
            }

            foreach (var sentence in result)
            {
                Console.WriteLine(sentence.Trim() + ".");
            }

            logger.Info("TextTemplateSearchApp finished.");
        }
    }
}
