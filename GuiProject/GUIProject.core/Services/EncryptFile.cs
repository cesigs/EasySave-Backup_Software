using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject
{
    public class EncryptFile
    {
        /// <summary>
        /// Launch the software cryptosoft to encrypt files using XOR
        /// Give the file to encrypt and the destination of this file to execute the encryption
        /// </summary>
        /// <param name="FileToEncrypt"></param>
        /// <param name="Destination"></param>
        /// <returns></returns>
        public async Task launchEncrypt(string FileToEncrypt, string Destination)
        {
            Process process = new Process();
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            process.StartInfo.FileName = Path.Combine(sCurrentDirectory, @"..\..\..\..\..\CryptoSoft\bin\Debug\net6.0\CryptoSoft.exe");
            process.StartInfo.ArgumentList.Add(FileToEncrypt);
            process.StartInfo.ArgumentList.Add(Destination);
            process.StartInfo.UseShellExecute = false;
            process.Start();
        }
    }
}