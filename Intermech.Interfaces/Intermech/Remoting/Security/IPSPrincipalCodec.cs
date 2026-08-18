
// Type: Intermech.Remoting.Security.IPSPrincipalCodec
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Security;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;


namespace Intermech.Remoting.Security
{
    /// <summary>
    /// Реализует кодировщик объектов типа IPSPrincipal в base64 и обратно.
    /// Реализация является thread safe.
    /// </summary>
    internal sealed class IPSPrincipalCodec
    {
      private ConcurrentDictionary<IPSPrincipal, string> encodeCache;
      private ConcurrentDictionary<string, IPSPrincipal> decodeCache;

      public IPSPrincipalCodec()
      {
        this.encodeCache = new ConcurrentDictionary<IPSPrincipal, string>();
        this.decodeCache = new ConcurrentDictionary<string, IPSPrincipal>();
      }

      public string EncodeToBase64(IPSPrincipal principal)
      {
        return principal != null ? this.encodeCache.GetOrAdd(principal, new Func<IPSPrincipal, string>(this.EncodeToBase64Slow)) : throw new ArgumentNullException(nameof (principal));
      }

      private string EncodeToBase64Slow(IPSPrincipal principal)
      {
        long userId = principal.Identity.UserId;
        string userName = principal.Identity.UserName;
        byte role = (byte) principal.Role;
        Guid securityToken = principal.SecurityToken;
        using (MemoryStream output = new MemoryStream(160 /*0xA0*/))
        {
          using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output, Encoding.UTF8, true))
          {
            binaryWriter.Write((byte) 0);
            binaryWriter.Write(userId);
            binaryWriter.Write(userName);
            binaryWriter.Write(role);
            binaryWriter.Write(securityToken.ToByteArray());
            binaryWriter.Flush();
          }
          return Convert.ToBase64String(output.ToArray());
        }
      }

      public IPSPrincipal DecodeFromBase64(string encodedPrincipal)
      {
        return encodedPrincipal != null ? this.decodeCache.GetOrAdd(encodedPrincipal, new Func<string, IPSPrincipal>(this.DecodeFromBase64Slow)) : throw new ArgumentNullException(nameof (encodedPrincipal));
      }

      private IPSPrincipal DecodeFromBase64Slow(string encodedPrincipal)
      {
        using (MemoryStream input = new MemoryStream(Convert.FromBase64String(encodedPrincipal)))
        {
          using (BinaryReader binaryReader = new BinaryReader((Stream) input, Encoding.UTF8, true))
          {
            int num = (int) binaryReader.ReadByte();
            long userId = binaryReader.ReadInt64();
            string str = binaryReader.ReadString();
            IPSBuiltInRole role = (IPSBuiltInRole) binaryReader.ReadByte();
            Guid securityToken = new Guid(binaryReader.ReadBytes(16 /*0x10*/));
            string userName = str;
            return new IPSPrincipal(new IPSIdentity(userId, userName), securityToken, role);
          }
        }
      }

      private enum DataFormat
      {
        Default,
      }
    }
}
