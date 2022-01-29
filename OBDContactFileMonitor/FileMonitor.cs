using System;
using System.IO;
using System.Text;
using System.Threading;

namespace com.workflowconcepts.applications.omnixpress
{
    public class FileMonitor
    {
        private FileSystemWatcher fileSystemWatcher;
        private ApplicationSettings settings;
        private string folderToWatchFor = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "TestFolder";
        private SqlAccess sqlProcessor;// = new SqlAccess();
        public FileMonitor(ApplicationSettings applicationSettings)
        {
            fileSystemWatcher = new FileSystemWatcher(folderToWatchFor);
            fileSystemWatcher.EnableRaisingEvents = true;

            // Instruct the file system watcher to call the FileCreated method
            // when there are files created at the folder.
            fileSystemWatcher.Created += new FileSystemEventHandler(FileCreated);

        } // end FileMonitor()

        private void FileCreated(Object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Found a new file");
            if (e.Name == "TestFile.txt")
            {
                   ProcessFile(e.FullPath);
            } // end if

        } // end public void FileCreated(Object sender, FileSystemEventArgs e)

        private void ProcessFile(String fileName)
        {
            FileStream inputFileStream;

            while (true)
            {
                try
                {
                    inputFileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                    StreamReader reader = new StreamReader(inputFileStream);
                    string contents = reader.ReadToEnd();
                    reader.Close();
                    if ((contents != null) && (contents != ""))
                    {
                        Console.WriteLine("Data found...");
                        sqlProcessor.ParseFileData(contents);
                    }
                    else
                    {
                        Log.Instance.Debug("No data in file " + fileName);
                    }

                    // Break out from the endless loop
                    break;
                }
                catch (IOException)
                {
                    // Sleep for 3 seconds before re-trying (file may still be copying over)
                    Thread.Sleep(3000);
                } // end try
            } // end while(true)
        } // end private void ProcessFile(String fileName)

    } // end public class FileMonitor
}