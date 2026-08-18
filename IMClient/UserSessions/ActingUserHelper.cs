
// Type: IMClient.UserSessions.ActingUserHelper




using Intermech.Interfaces;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


namespace IMClient.UserSessions
{
    internal sealed class ActingUserHelper
    {
      private static Regex _actingUserPattern = new Regex("ActingUser:(?<PackedData>.+)$", RegexOptions.IgnoreCase);

      public ActingUserInfo TryGetActingUserInfo()
      {
        string[] commandLineArgs = Environment.GetCommandLineArgs();
        for (int index = 1; index < commandLineArgs.Length; ++index)
        {
          Match match = ActingUserHelper._actingUserPattern.Match(commandLineArgs[index]);
          if (match.Success)
            return this.DecodeActingUserInfo(match.Groups["PackedData"].Value);
        }
        return (ActingUserInfo) null;
      }

      private ActingUserInfo DecodeActingUserInfo(string base64String)
      {
        using (MemoryStream input = new MemoryStream(Convert.FromBase64String(base64String)))
        {
          using (BinaryReader binaryReader = new BinaryReader((Stream) input, Encoding.UTF8, true))
            return new ActingUserInfo(binaryReader.ReadInt64(), binaryReader.ReadString(), TimeSpan.FromTicks(binaryReader.ReadInt64()), binaryReader.ReadInt32());
        }
      }
    }
}
