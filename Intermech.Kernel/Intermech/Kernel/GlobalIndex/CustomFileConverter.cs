// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GlobalIndex.CustomFileConverter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server.GlobalIndex;
using System.IO;


namespace Intermech.Kernel.GlobalIndex;

public abstract class CustomFileConverter : IIndexerFileConverter
{
  public virtual string[] SupportedFileExtensions => (string[]) null;

  protected string ReadFromStream(Stream strm)
  {
    strm.Seek(0L, SeekOrigin.Begin);
    using (StreamReader streamReader = new StreamReader(strm))
      return streamReader.ReadToEnd();
  }

  public virtual int Priority => 0;

  public abstract string Caption { get; }

  public virtual bool CanGetPlainText(IDBAttribute attribute)
  {
    string[] supportedFileExtensions = this.SupportedFileExtensions;
    if (supportedFileExtensions != null)
    {
      string upper = Path.GetExtension(attribute.AsString).ToUpper();
      for (int index = 0; index < supportedFileExtensions.Length; ++index)
      {
        if (upper == supportedFileExtensions[index])
          return true;
      }
    }
    return false;
  }

  public abstract string GetPlainText(IDBAttribute attribute);
}
