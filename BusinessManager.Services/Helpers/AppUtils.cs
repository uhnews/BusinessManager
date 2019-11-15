using System.IO;

namespace BusinessManager.Services.Helpers
{
    public static class AppUtils
    {
        public static string getNextFileName(string directory, string fileName)
        {
            string result = "";
            if (Directory.Exists(directory))
            {
                if (File.Exists(directory + "\\" + fileName))
                {
                    string fileNamePrefix = fileName;
                    string fileType = "";
                    int charPos = fileName.LastIndexOf('.');
                    if (charPos > -1)
                    {
                        fileNamePrefix = fileName.Substring(0, charPos);
                        fileType = fileName.Substring(charPos);
                    }

                    int i = 2;
                    bool isNameAquired = false;
                    while (!isNameAquired)
                    {
                        if (!File.Exists(directory + "\\" + fileNamePrefix + " (" + i.ToString() + ")" + fileType))
                        {
                            result = fileNamePrefix + " (" + i.ToString() + ")" + fileType;
                            isNameAquired = true;
                        }
                        ++i;
                    }
                }
                else
                {
                    result = fileName;
                }
            }
            return result;
        }
    }
}