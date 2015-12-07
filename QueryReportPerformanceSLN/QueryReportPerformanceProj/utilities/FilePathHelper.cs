using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using log4net;

namespace msrb.org.queryreportperf.utilities
{
    class FilePathHelper
    {
        private readonly ILog logger = LogManager.GetLogger("root");
        private const string PROJECT_NAME = "QueryReportPerformanceProj";
        public FileInfo FindFileInThisProject(string fileName)
        {
            return FindFile(FindProjectDir(Directory.GetCurrentDirectory()), fileName);
        }

        public string FindProjectDir(string dir)
        {
            if (dir.EndsWith(PROJECT_NAME) && Regex.Matches(dir, PROJECT_NAME).Count == 1)
            {
                return dir;
            }

            if (Regex.Matches(dir, PROJECT_NAME).Count >= 1)
            {
                return FindProjectDir(dir.Substring(0, dir.LastIndexOf(@"\", System.StringComparison.Ordinal)));
            }

            throw new Exception("Unexpected solution name");
        }

        public void FindCSProjFiles(string dir, List<FileInfo> fileList)
        {
            try
            {
                foreach (string fileName in Directory.GetFiles(dir))
                {
                    var fileInfo = new FileInfo(fileName);

                    if (fileInfo.Name.ToUpper().EndsWith("CSPROJ"))
                    {
                        fileList.Add(fileInfo);
                    }
                }

                foreach (string d in Directory.GetDirectories(dir))
                {
                    FindCSProjFiles(d, fileList);
                }

            }
            catch (System.Exception ex)
            {
                logger.WarnFormat("FilePathHelper: {0}", ex);
            }
        }

        public FileInfo FindFile(string dir, string fileNameToFind)
        {
            try
            {
                foreach (string fileName in Directory.GetFiles(dir))
                {
                    var fileInfo = new FileInfo(fileName);

                    if (fileInfo.Name.Equals(fileNameToFind))
                    {
                        return fileInfo;
                    }
                }

                foreach (string d in Directory.GetDirectories(dir))
                {
                    var foundFile = FindFile(d, fileNameToFind);
                    if (foundFile != null)
                    {
                        return foundFile;
                    }
                }

            }
            catch (System.Exception ex)
            {
                logger.WarnFormat("FilePathHelper: {0}", ex);
            }
            return null;
        }

        public DirectoryInfo FindFolder(string dir, string folderNameToFind)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(dir))
                {
                    var directoryInfo = new DirectoryInfo(d);
                    if (directoryInfo.Name.Equals(folderNameToFind))
                    {
                        return directoryInfo;
                    }

                    directoryInfo = FindFolder(d, folderNameToFind);
                    if (directoryInfo != null)
                    {
                        return directoryInfo;
                    }
                }
            }
            catch (System.Exception ex)
            {
                logger.WarnFormat("FilePathHelper: {0}", ex);
            }
            return null;
        }

        public void DirectoryCopy(string SourcePath, string DestinationPath)
        {
            if (Directory.Exists(DestinationPath))
            {
                try
                {
                    DeleteDirectory(DestinationPath);
                }
                catch (Exception e)
                {
                    logger.WarnFormat("Exception occurred deleting directory {0}.  {1}", DestinationPath, e);
                }
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(DestinationPath);
                }
                catch (Exception e)
                {
                    logger.WarnFormat("Exception occurred creating directory {0}.  {1}", DestinationPath, e);
                }
            }

            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
            {
                var newPathVar = newPath.Replace(SourcePath, DestinationPath);
                var parentDir = new FileInfo(newPathVar).DirectoryName;

                try
                {
                    Directory.CreateDirectory(parentDir);
                }
                catch (Exception e)
                {
                    logger.WarnFormat("Exception occurred creating directory {0}.  {1}", newPathVar, e);
                }

                try
                {
                    File.Copy(newPath, newPathVar);
                }
                catch (Exception e)
                {
                    logger.WarnFormat("Exception occurred creating file {0}.  {1}", newPath, e);
                }
            }
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        public void AddIntraDayIncludeToCsProjFile(FileInfo fileInfo)
        {

            try
            {
                var lines = ReadContents(fileInfo);
                SetIntraDayInclude(lines);
                Save(lines, fileInfo);
            }
            catch (Exception e)
            {
                logger.WarnFormat("An exception occurred writing to file {0}.  {1}", fileInfo.FullName, e);
            }
        }

        public List<string> ReadContents(FileInfo fileInfo)
        {
            return new List<string>(System.IO.File.ReadAllLines(fileInfo.FullName, Encoding.Default));
        }

        public void SetIntraDayInclude(List<string> fileContents)
        {
            var intraDayInclude = "<Compile Include=\"UnitTestTagger\\Attributes\\IntraDay.cs\" />";
            var indexOfInclude = -1;
            for (int index = 0; index < fileContents.Count; index++)
            {
                var line = fileContents[index];
                if (line.Contains("Compile") && line.Contains("Include"))
                {
                    indexOfInclude = index;
                    break;
                }
            }

            if (indexOfInclude == -1)
            {
                throw new Exception("Unable to set include for current file.");
            }

            fileContents.Insert(indexOfInclude, intraDayInclude);
        }

        public void Save(List<string> fileContents, FileInfo fileInfo)
        {
            System.IO.File.WriteAllLines(fileInfo.FullName, fileContents, Encoding.Default);
        }

        public List<FileInfo> FindFiles(string dir, string searchString)
        {
            var toReturn = new List<FileInfo>();
            FindFilesRecursive(dir, searchString, toReturn);
            return toReturn;
        }

        public void FindFilesRecursive(string dir, string searchString, List<FileInfo> foundFiles)
        {
            try
            {
                foreach (string fileName in Directory.GetFiles(dir))
                {
                    var fileInfo = new FileInfo(fileName);

                    if (fileInfo.Name.Contains(searchString))
                    {
                        foundFiles.Add(fileInfo);
                    }
                }

                foreach (string d in Directory.GetDirectories(dir))
                {
                    FindFilesRecursive(d, searchString, foundFiles);
                }
            }
            catch (System.Exception ex)
            {
                logger.WarnFormat(ex.Message);
            }
        }
    }
}
