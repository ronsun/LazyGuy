using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using LazyGuy.Constants;

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
        public static X509Certificate2 Import(this X509Certificate2 cert,
            Assembly assembly,
            string fullName,
            string password = null,
            X509KeyStorageFlags keyStorageFlags = 0)
        {
            Argument.NotNull(cert, nameof(cert));
            Argument.NotNull(assembly, nameof(assembly));

            if (string.IsNullOrEmpty(fullName))
            {
                string msg = string.Format(MessageTemplates.ArgumentEmpty, nameof(fullName));
                throw new ArgumentOutOfRangeException(msg);
            }

            using (Stream certStream = assembly.GetManifestResourceStream(fullName))
            {
                byte[] rawBytes = new byte[certStream.Length];
                for (int index = 0; index < certStream.Length; index++)
                {
                    rawBytes[index] = (byte)certStream.ReadByte();
                }

                if (string.IsNullOrEmpty(password))
                {
                    cert.Import(rawBytes);
                }
                else
                {
                    cert.Import(rawBytes, password, keyStorageFlags);
                }
            }

            return cert;
        }
    }
}
