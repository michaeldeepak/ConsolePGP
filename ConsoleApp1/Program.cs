using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {


            using (Stream publicKeyStream = new FileStream(@"C:\inetpub\wwwroot\pgp\pgptest.asc", FileMode.Open))
            {
                PgpPublicKey pubKey = ReadPublicKey(publicKeyStream);
                var UserId = pubKey.GetUserIds();
                var KeyId = pubKey.KeyId.ToString("X");
                
            }

        }


        private static PgpPublicKey ReadPublicKey(Stream inputStream)
        {
            inputStream = PgpUtilities.GetDecoderStream(inputStream);
            PgpPublicKeyRingBundle pgpPub = new PgpPublicKeyRingBundle(inputStream);

            foreach (PgpPublicKeyRing keyRing in pgpPub.GetKeyRings())
            {
                foreach (PgpPublicKey key in keyRing.GetPublicKeys())
                {
                    if (key.IsEncryptionKey)
                    {
                        return key;
                    }

                    
                }
            }

            throw new ArgumentException("Can't find encryption key in key ring.");
        }
    }
}
