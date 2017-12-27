using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC2_names
{
    class Program
    {
        internal static bool shouldRename = false;
        internal static char[] allowedChars = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();

        private static void ReadNameFromOffset(string filePath, int offsetSize = 25)
        {
            try
            {
                if (Path.GetFileName(filePath).Contains("dmg")) { return; }
                string returnSTR = "";
                byte[] offsetRead = new byte[offsetSize];
                using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open)))
                {
                    reader.BaseStream.Seek(8, SeekOrigin.Begin);
                    reader.Read(offsetRead, 0, offsetSize);
                }
                string byteSTR = Encoding.Default.GetString(offsetRead);

                foreach (char offsetChar in byteSTR)
                {
                    if (allowedChars.Contains(offsetChar) || offsetChar.ToString() == "_")
                    {
                        returnSTR += offsetChar;
                    }
                }
                if (shouldRename)
                {
                    File.Move(filePath, Path.GetDirectoryName(filePath) + @"\" + returnSTR + ".meb");
                    Console.WriteLine(filePath + "     renamed to    " + returnSTR + " /end" + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine(filePath + "     returned    " + returnSTR + " /end" + Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            Console.Title = " /end marks the end of the name";
            if (args.Length > 0)
            {
                Console.WriteLine(Environment.NewLine + "Enter custom offset size / return for default (25)");
                string customSize = Console.ReadLine();
                if(customSize.Contains("rename "))
                {
                    shouldRename = true;
                    customSize = customSize.Replace("rename ", "");
                }
                FileAttributes attr = File.GetAttributes(args[0]);
                if (customSize == "")
                {
                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        foreach (string meb in Directory.GetFiles(args[0]))
                        {
                            if (meb.EndsWith(".meb"))
                            {
                                ReadNameFromOffset(meb);
                            }
                        }
                    }
                    else
                    {
                        ReadNameFromOffset(args[0]);
                    }
                }
                else
                {
                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        foreach (string meb in Directory.GetFiles(args[0]))
                        {
                            if (meb.EndsWith(".meb"))
                            {
                                ReadNameFromOffset(meb, Convert.ToInt16(customSize));
                            }
                        }
                    }
                    else
                    {
                        ReadNameFromOffset(args[0], Convert.ToInt16(customSize));
                    }
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Command line arguments were null (open by dragging a folder or file onto the exe)");
                Console.ReadLine();
            }
        }
    }
}
