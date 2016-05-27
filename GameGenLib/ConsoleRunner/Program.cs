using System;
using System.IO;
using GameGenLib;
using GameGenLib.GameParser;

namespace ConsoleRunner {
    class Program {
        static void Main(string[] args) {
            GameXmlParser parser = new GameXmlParser(File.OpenText("../../RulesXO.xml"));
            GameContext context = parser.Parse();
            context.StartGame();
            context.SelectPossibleMove(0, 0);
            context.SelectPossibleMove(7, 0);
            context.SelectPossibleMove(1, 0);
            context.SelectPossibleMove(8, 0);
            context.SelectPossibleMove(2, 0);
            context.SelectPossibleMove(9, 0);
            context.SelectPossibleMove(3, 0);
            context.SelectPossibleMove(10, 0);
            context.SelectPossibleMove(5, 0);
            context.SelectPossibleMove(11, 0);
            Console.WriteLine(context.GetEndOfGameStatus());
//            context.SelectPossibleMove(11, 0);
//            context.SelectPossibleMove(5, 0);
//            context.SelectPossibleMove(12, 0);



//            context.SelectPossibleMove(11, 10);
//            context.SelectPossibleMove(5, 9);
//            context.SelectPossibleMove(12, 10);
//            context.SelectPossibleMove(5, 20);
//            context.SelectPossibleMove(13, 10);
//            context.SelectPossibleMove(5, 21);
//            context.SelectPossibleMove(14, 10);
//            Console.WriteLine(context.GetEndOfGameStatus());
//            context.SelectPossibleMove(5, 22);
//            Console.WriteLine(context.GetEndOfGameStatus());
//            context.SelectPossibleMove(15, 10);
//            Console.WriteLine(context.GetEndOfGameStatus());
//            context.SelectPossibleMove(5, 5);
//            context.SelectPossibleMove(16, 10);
        }
    }
}
