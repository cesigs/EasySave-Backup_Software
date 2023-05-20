using System.IO;

class Program
{
    const byte Secret = 101;
    /// <summary>
    /// Take as args the source file and the destination of the file to encrypt
    /// Rewrite the file using the encryption method by verifying if each byte has been encrypted
    /// </summary>
    static void Main(string[] args)
    {
        var a = args[0];
        var b = args[1];
        using (var fin = new FileStream(a, FileMode.Open))
        using (var fout = new FileStream(b, FileMode.Create))
        {
            byte[] buffer = new byte[4096];
            while (true)
            {
                int bytesRead = fin.Read(buffer);
                if (bytesRead == 0)
                    break;
                EncryptBytes(buffer, bytesRead);
                fout.Write(buffer, 0, bytesRead);
            }
        }
    }
    

    /// <summary>
    /// Use a XOR method with a key that will encrypt every byte of the file with the key
    /// <summary>
    static void EncryptBytes(byte[] buffer, int count)
    {

        for (int i = 0; i < count; i++)
        {
            buffer[i] = (byte)(buffer[i] ^ Secret);
        }
    }
}