namespace ENoticeBoard.Models
{
    public class FileCopy
    {
        public bool CopyDB()
        {
            string fileName = "spiceworks_prod.db";
            string sourcePath = "\\\\vapp01\\Spiceworks\\db";
            string targetPath = "\\\\vapp01\\Spiceworks\\db\\backup\\temp";

            

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            if (System.IO.File.Exists(sourceFile))
            {
                File.SetAttributes(sourcePath, FileAttributes.Normal);
                System.IO.File.Copy(sourceFile, destFile, true);
            }
            
            if (System.IO.File.Exists(destFile))
            {
                return true;
            }
            else
            {
                return false;
            }
        
            //// To copy all the files in one directory to another directory.
            //// Get the files in the source folder. 
           
            //if (System.IO.Directory.Exists(sourcePath))
            //{
            //    string[] files = System.IO.Directory.GetFiles(sourcePath);

            //    // Copy the files and overwrite destination files if they already exist.
            //    foreach (string s in files)
            //    {
            //        // Use static Path methods to extract only the file name from the path.
            //        fileName = System.IO.Path.GetFileName(s);
            //        destFile = System.IO.Path.Combine(targetPath, fileName);
            //        System.IO.File.Copy(s, destFile, true);
            //    }
            //}
            //else
            //{
            //    return false;
            //}

            
        }
    }
}