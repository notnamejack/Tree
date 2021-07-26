using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {            
            new Program().Start(args);

        }

        public void Start(string [] args)
        {
            string helpText = @"
Справка:
- справка по использованию (-? или --help)
- задают глубину вложенности (-d или --depth)
- показывают размер объектов (-s или --size)
- удобный для восприятия вид(-h или --human-readable)
- путь указывается последним 
";
            int depth = 0;
            bool size = false;
            bool human = false;

            Console.WriteLine( args.Length);
            for ( int i = 0; i < args.Length; i++)
            {
                Console.WriteLine( args[i]);
                if(args[i] == "-?" || args[i] == "--help")
                    Console.WriteLine(helpText);

                if (args[i] == "-d" || args[i] == "--depth")
                {
                    depth = int.Parse(args[i+1]);
                }

                if (args[i] == "-s" || args[i] == "--size")
                    size = true;

                if (args[i] == "-h" || args[i] == "--human-readable")
                    human = true;
                
            }
            new Tree() { Depth = depth, Size = size, Human = human}.Start(args.LastOrDefault(),0);


           

            
        }
    }
}
