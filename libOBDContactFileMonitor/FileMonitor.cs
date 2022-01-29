using System;
using System.IO;
using System.Text;
using System.Threading;

namespace com.workflowconcepts.applications.filemonitor
{
    public class FileMonitor
    {
        private FileSystemWatcher fileSystemWatcher;
        private ApplicationSettings settings;
        private string folderToWatchFor;
        private SqlAccess _sqlProcessor;
        public FileMonitor(ApplicationSettings applicationSettings)
        {
            try
            {
                settings = applicationSettings;
                if (!Directory.Exists(settings.InputDirectory))
                {
                    Log.Instance.Debug("Creating input directory since it did not exist: " + settings.InputDirectory);
                    Directory.CreateDirectory(settings.InputDirectory);
                }
                folderToWatchFor = settings.InputDirectory;
                _sqlProcessor = new SqlAccess(applicationSettings);
                fileSystemWatcher = new FileSystemWatcher(folderToWatchFor);
                fileSystemWatcher.EnableRaisingEvents = true;

                // Instruct the file system watcher to call the FileCreated method
                // when there are files created at the folder.
                fileSystemWatcher.Created += new FileSystemEventHandler(FileCreated);
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Error instantiating FileMonitor: " + ex.Message.ToString());
            }
        } // end FileMonitor()

        public void Dispose()
        {
            if(_sqlProcessor != null)
            {
                _sqlProcessor.Dispose();
                _sqlProcessor = null;
            }
        }
        private void FileCreated(Object sender, FileSystemEventArgs e)
        {
            try
            {
                Log.Instance.Debug("Found a new file: " + e.FullPath);
                if (ProcessFile(e.FullPath))
                {
                    if (settings.ArchiveFiles)
                    {

                        if (!Directory.Exists(settings.ArchiveDirectory))
                        {
                            Log.Instance.Debug("Creating archive directory since it did not exist: " + settings.ArchiveDirectory);
                            Directory.CreateDirectory(settings.ArchiveDirectory);
                        }

                        if (File.Exists(settings.ArchiveDirectory + "\\" + e.Name + ".arc"))
                        {
                            Log.Instance.Debug("Deleting existing archive file since it already exists: " + settings.ArchiveDirectory + "\\" + e.Name + ".arc");
                            File.Delete(settings.ArchiveDirectory + "\\" + e.Name + ".arc");
                        }

                        File.Move(e.FullPath, settings.ArchiveDirectory + "\\" + e.Name + ".arc");
                    }
                    else
                    {
                        File.Delete(e.FullPath);
                    }                    
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Error while archiving file: " + e.FullPath + ": " + ex.Message.ToString());
            }
        } // end public void FileCreated(Object sender, FileSystemEventArgs e)

        private bool ProcessFile(String fileName)
        {
            try
            {
                bool success = false;
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
                            success = _sqlProcessor.ParseFileData(contents);
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
                return success;
            }
            catch (Exception ex)
            {
                Log.Instance.Error("Error while processing file: " + fileName + ": " + ex.Message.ToString());
                return false;
            }
        } // end private void ProcessFile(String fileName)

    } // end public class FileMonitor
}