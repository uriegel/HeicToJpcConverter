using System.Diagnostics;
using CsTools.Extensions;
using CsTools.Functional;
using ImageMagick;
using static System.Console;

WriteLine("Heic to jpg image converter...");

var inputPath = "/daten/Bilder/Fotos/Julia/Takeout/Google Fotos/Photos from 2023";
var outputPath = "/home/uwe/julia";

var stopWatch =
    new Stopwatch()
    .SideEffect(sw => sw.Start());

await Directory
    .GetFiles(inputPath, "*.HEIC")
    .Select(AddJob)
    .WaitAll();

WriteLine($"Heic to jpg image converter finished in {stopWatch.Elapsed}");    

Task AddJob(string input)
    => new FileInfo(input)
           .SideEffect(fi => WriteLine($"Converting {fi.Name}"))
           .Pipe(fi => Task.Run(() 
                => Convert(fi.FullName, outputPath.AppendPath(fi.Name[..^4] + "jpg"))
            ));

void Convert(string input, string output)
    => Using.Use(
        new MagickImage(input),
            mi => mi
                    .SideEffect(mi => mi.Format = MagickFormat.Jpg)
                    .Write(output)
    );



