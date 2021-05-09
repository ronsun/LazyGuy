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
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="cert"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="assembly"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="fullName"/> is null or empty.
        /// </exception>
        public static X509Certificate2 Import(
            this X509Certificate2 cert,
            Assembly assembly,
            string fullName,
            string password = null,
            X509KeyStorageFlags keyStorageFlags = 0)
        {
            Argument.NotNull(cert, nameof(cert));
            Argument.NotNull(assembly, nameof(assembly));
            Argument.NotNullOrEmpty(fullName, nameof(fullName));

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

    // TODO: RSA
    // TODO: RSA 分段加解密
}
