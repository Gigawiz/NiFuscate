using System; 
using System.IO; 
using System.Security.Cryptography;

namespace NiFuscate_Beta
{
    class FileEncryption
    {
         public void enc(string file, string keyfile)
        {
            string tmphst = @"C:\Program Files\NiCoding\hst.tmp";
            FileStream fsFileOut = File.Create(tmphst);
            // The chryptographic service provider we're going to use
            TripleDESCryptoServiceProvider cryptAlgorithm = new TripleDESCryptoServiceProvider();
            // This object links data streams to cryptographic values
            CryptoStream csEncrypt = new CryptoStream(fsFileOut, cryptAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
            // This stream writer will write the new file
            StreamWriter swEncStream = new StreamWriter(csEncrypt);
            // This stream reader will read the file to encrypt
            StreamReader srFile = new StreamReader(file);
            // Loop through the file to encrypt, line by line
            string currLine = srFile.ReadLine();
            while (currLine != null)
            {
                // Write to the encryption stream
                swEncStream.Write(currLine);
                currLine = srFile.ReadLine();
            }
            // Wrap things up
            srFile.Close();
            swEncStream.Flush();
            swEncStream.Close();
            // Create the key file
            FileStream fsFileKey = File.Create(@"C:\Program Files\NiCoding\RipLeech\"+keyfile+".rdat");
            BinaryWriter bwFile = new BinaryWriter(fsFileKey);
            bwFile.Write(cryptAlgorithm.Key);
            bwFile.Write(cryptAlgorithm.IV);
            bwFile.Flush();
            bwFile.Close();
            filecleanup(tmphst, file);
        }
        public void dec(string file, string keyfile)
        {
            string tmphst = @"C:\Program Files\NiCoding\hst.tmp";
            // The encrypted file
            FileStream fsFileIn = File.OpenRead(file);
            // The key
            FileStream fsKeyFile = File.OpenRead(@"C:\Program Files\NiCoding\RipLeech\" + keyfile + ".rdat");
            // The decrypted file
            FileStream fsFileOut = File.Create(tmphst);
            // Prepare the encryption algorithm and read the key from the key file
            TripleDESCryptoServiceProvider cryptAlgorithm = new TripleDESCryptoServiceProvider();
            BinaryReader brFile = new BinaryReader(fsKeyFile);
            cryptAlgorithm.Key = brFile.ReadBytes(24);
            cryptAlgorithm.IV = brFile.ReadBytes(8);
            // The cryptographic stream takes in the encrypted file
            CryptoStream csEncrypt = new CryptoStream(fsFileIn, cryptAlgorithm.CreateDecryptor(), CryptoStreamMode.Read);
            // Write the new unecrypted file
            StreamReader srCleanStream = new StreamReader(csEncrypt);
            StreamWriter swCleanStream = new StreamWriter(fsFileOut);
            swCleanStream.Write(srCleanStream.ReadToEnd());
            swCleanStream.Close();
            fsFileOut.Close();
            srCleanStream.Close();
            filecleanup(tmphst, file);
            File.Delete(@"C:\Program Files\NiCoding\RipLeech\" + keyfile + ".rdat");
        }
        void filecleanup(string newf, string oldf)
        {
            File.Delete(oldf);
            File.Copy(newf, oldf);
            File.Delete(newf);
        }
        public string genkey()
        {
            string nightlykey = genkeynam();
            return nightlykey;
        }
        private string genkeynam()
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[10];
            Random rd = new Random();

            for (int i = 0; i < 10; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
