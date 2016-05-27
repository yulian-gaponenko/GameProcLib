using System.IO;
using GameGenLib.GameParser;

namespace ConsoleRunner {
    class Program {
        static void Main(string[] args) {
            GameXmlParser parser = new GameXmlParser(File.OpenText("../../RulesXO.xml"));
            parser.Parse();
        }
    }
}
