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
        public static X509Certificate2 Import(this X509Certificate2 cert, Assembly assembly, string fullName)
        {
            if (cert == null)
            {
                string msg = string.Format(MessageTemplates.ArgumentNull, nameof(cert));
                throw new ArgumentNullException(msg);
            }

            if (assembly == null)
            {
                string msg = string.Format(MessageTemplates.ArgumentNull, nameof(assembly));
                throw new ArgumentNullException(msg);
            }

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

                cert.Import(rawBytes);
            }

            return cert;
        }
    }
}
