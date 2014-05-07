using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;

namespace ALPRibbon
{
    class ALPGeneralUtils
    {
        // creates a new temporary directory for work to be done
        public static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            Directory.CreateDirectory(tempDirectory + "\\" + RibbonAddIn.EXPORT_DIR);
            return tempDirectory;
        }

        public static void CreateZipFile(String inputDir, String outputDir, String outputName)
        {
            String outputFile = Path.Combine(outputDir, outputName);
            // zip up the files
            string[] filenames = Directory.GetFiles(inputDir);

            ZipOutputStream s = new ZipOutputStream(File.Create(outputFile));
            s.SetLevel(4); // 0 - store only to 9 - means best compression

            byte[] buffer = new byte[4096];

            foreach (string zipfile in filenames)
            {
                ZipEntry entry = new ZipEntry(Path.GetFileName(zipfile));
                entry.DateTime = DateTime.Now;
                s.PutNextEntry(entry);

                using (FileStream fs = File.OpenRead(zipfile))
                {
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        s.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
            }
            s.Finish();
            s.Close();
        }

        // removes all files in a directory
        public static void ClearDirectory(string dirName)
        {
            DirectoryInfo di = new DirectoryInfo(RibbonAddIn.WORKING_DIR + "\\" + dirName);
            foreach (FileInfo fi in di.GetFiles())
            {
                fi.Delete();
            }
        }
    }
}
