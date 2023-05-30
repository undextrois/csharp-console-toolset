/*
To run the program:

Open a text editor and copy the code into a new file.
Save the file with a .cs extension, for example, ImageRenamer.cs.
Open Terminal on your Mac and navigate to the directory where you saved the .cs file.
Compile the program using the following command: mcs ImageRenamer.cs.
Run the compiled program using: mono ImageRenamer.exe.


*/
using System;
using System.IO;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the directory path containing .heic files:");
        string directoryPath = Console.ReadLine();

        try
        {
            // Get all .heic files in the directory
            string[] heicFiles = Directory.GetFiles(directoryPath, "*.heic");

            Console.WriteLine("Renaming image files...");
            Console.CursorVisible = false;

            for (int i = 0; i < heicFiles.Length; i++)
            {
                string heicFile = heicFiles[i];
                string newFileName = Path.ChangeExtension(heicFile, ".jpg");
                File.Move(heicFile, newFileName);

                // Update progress bar
                Console.Write("\r[{0}] {1}/{2}", GetProgressBar(i + 1, heicFiles.Length), i + 1, heicFiles.Length);
                Thread.Sleep(100); // Simulate some processing time
            }

            Console.WriteLine("\nImage files successfully renamed to .jpg!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nError occurred during file renaming: " + ex.Message);
        }

        Console.CursorVisible = true;
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    // Function to generate the progress bar string
    static string GetProgressBar(int currentProgress, int totalProgress)
    {
        const int progressBarWidth = 30;
        int filledWidth = (int)Math.Round((double)(currentProgress * progressBarWidth) / totalProgress);

        string progressBar = new string('#', filledWidth) + new string('-', progressBarWidth - filledWidth);

        // Add spinning animation
        char[] animationFrames = { '|', '/', '-', '\\' };
        char spinningFrame = animationFrames[currentProgress % animationFrames.Length];

        progressBar = string.Format("[{0}] {1}", progressBar, spinningFrame);

        return progressBar;
    }
}
