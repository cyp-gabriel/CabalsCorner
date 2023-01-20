using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CabalsCorner.Utilities
{
	/// <summary>
	/// Static class that manages a number of file i/o related operations.
	/// </summary>
    public static class FileOps
    {
		 #region Class Operations

		 /// <summary>
		 /// Copies 'sourceFilename' (full path) to 'destFilename' (full path).  Method gives option to overwrite existing file.
		 /// </summary>
		 /// <param name="sourceFilename">Full file path to file to be copied.</param>
		 /// <param name="destFilename">Full file path to destination filename.</param>
		 /// <param name="overwriteExistingFile">Allows you to decide whether or not to overwrite existing file.</param>
		 public static void Copy(string sourceFilename, string destFilename, bool overwriteExistingFile)
		 {
			 // To copy a file to another location and 
			 // overwrite the destination file if it already exists.
			 File.Copy(sourceFilename, destFilename, overwriteExistingFile);
		 }

		 /// <summary>
		 /// Copies all files in 'sourceFiles' (full path) to 'destFilename' (full path).  Gives option to overwrite existing file.
		 /// </summary>
		 /// <param name="sourceFiles">Source files (full filename).</param>
		 /// <param name="destFolder">Destination folder (full path).</param>
		 /// <param name="overwrite">Overwrite existing file?</param>
		 public static void CopyFiles(string[] sourceFiles, string destFolder, bool overwrite)
		 {
			 foreach (string file in sourceFiles)
			 {
				 string newFilename = Path.Combine(destFolder, Path.GetFileName(file));
				 File.Copy(file, newFilename, overwrite);
			 }
		 }

		 /// <summary>
		 /// Returns number of files in 'destFolder' (full folder path).
		 /// </summary>
		 /// <param name="destFolder">Full folder path to folder containing files to count.</param>
		 /// <returns>Number of files in 'destFolder'.</returns>
		 public static int NumberOfFilesInFolder(string destFolder)
		 {
			 string[] filePaths = Directory.GetFiles(destFolder);
			 return filePaths.Length;
		 }

		 /// <summary>
		 /// Deletes all files in 'destFolder' (full folder path).
		 /// </summary>
		 /// <param name="destFolder">Full folder path to destination folder from which to delete files.</param>
		 public static void ClearFolder(string destFolder)
		 {
			 string[] filePaths = Directory.GetFiles(destFolder);
			 foreach (string filePath in filePaths)
				 File.Delete(filePath);
		 }

		 #endregion
	 }
}
