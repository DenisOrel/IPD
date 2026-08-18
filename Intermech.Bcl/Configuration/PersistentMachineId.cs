
// Type: Intermech.Configuration.PersistentMachineId
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.IO;


namespace Intermech.Configuration
{
    /// <summary>
    /// Реализует механизм пометки и идентификации текущего компьютера.
    /// </summary>
    public static class PersistentMachineId
    {
      private static readonly object _lockGuard = new object();
      private static string _idFilePath;
      private static Guid _id;

      public static Guid GetId()
      {
        lock (PersistentMachineId._lockGuard)
        {
          if (PersistentMachineId._id == Guid.Empty)
          {
            if (File.Exists(PersistentMachineId.IdFilePath))
            {
              try
              {
                PersistentMachineId._id = PersistentMachineId.ReadCurrentId();
              }
              catch (ArgumentException ex)
              {
                PersistentMachineId._id = PersistentMachineId.MakeNewId();
              }
            }
            else
              PersistentMachineId._id = PersistentMachineId.MakeNewId();
          }
          return PersistentMachineId._id;
        }
      }

      private static Guid ReadCurrentId()
      {
        return new Guid(File.ReadAllBytes(PersistentMachineId.IdFilePath));
      }

      private static Guid MakeNewId()
      {
        Guid guid = Guid.NewGuid();
        string directoryName = Path.GetDirectoryName(PersistentMachineId.IdFilePath);
        if (!Directory.Exists(directoryName))
          Directory.CreateDirectory(directoryName);
        File.WriteAllBytes(PersistentMachineId.IdFilePath, guid.ToByteArray());
        return guid;
      }

      private static string IdFilePath
      {
        get
        {
          lock (PersistentMachineId._lockGuard)
          {
            if (string.IsNullOrEmpty(PersistentMachineId._idFilePath))
              PersistentMachineId._idFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Intermech\\machine_id.dat");
            return PersistentMachineId._idFilePath;
          }
        }
      }
    }
}
