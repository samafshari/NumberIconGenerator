using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace NumberIconGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            basePath = Path.Combine(basePath, "NumberIcons");
            Directory.CreateDirectory(basePath);

            Generator generator = new Generator
            {
                BackgroundPath = "circle (1).png",
                Suffix = "@3x",
                OutputDirectory = Path.Combine(basePath, "iOS"),
                Size = 128,
                FontSize = 42
            };
            generator.Generate();

            generator.Suffix = "";
            generator.OutputDirectory = Path.Combine(basePath, "Android");
            generator.Size = 64;
            generator.FontSize = 21;
            generator.Generate();
        }
    }
}
