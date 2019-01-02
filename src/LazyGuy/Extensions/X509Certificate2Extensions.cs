using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace LazyGuy.Extensions
{
    /// <summary>
    /// Extension methods for X509Certificate2
    /// </summary>
    public static class X509Certificate2Extensions
    {
        /// <summary>
        /// Populates an X509Certificate2 object
        /// with data from embedded resource in assembly.
        /// </summary>
        /// <param name="cert"></param>
        /// <param name="assembly">The assembly contain the target embedded resource. </param>
        /// <param name="fullName">Full name of target embedded resource. </param>
        /// <returns></returns>
        public static X509Certificate2 Import(this X509Certificate2 cert, Assembly assembly, string fullName)
        {
            using (Stream certStream = assembly.GetManifestResourceStream(fullName))
            {
                byte[] rawBytes = new byte[certStream.Length];
                for (int index = 0; index < certStream.Length; index++)
                {
                    rawBytes[index] = (byte)certStream.ReadByte();
                }

                cert.Import(rawBytes);
            }

            return cert;
        }
    }
}
