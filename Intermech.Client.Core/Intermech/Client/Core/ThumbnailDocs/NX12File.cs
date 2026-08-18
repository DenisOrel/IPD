
// Type: Intermech.Client.Core.ThumbnailDocs.NX12File
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Intermech.Client.Core.ThumbnailDocs;

internal class NX12File : IDisposable
{
  private FileStream _fileStream;
  private BinaryReader _binaryReader;
  private const string SignatureName = "SPLMSSTR";
  private const string HeaderName = "HEADER";
  private const string FooterName = "FOOTER";
  private List<Tuple<string, long, long>> _headerSections = new List<Tuple<string, long, long>>();
  private List<Tuple<string, long, long>> _footerSections = new List<Tuple<string, long, long>>();

  /// <summary>Constructor</summary>
  /// <param name="fileName"></param>
  /// <param name="fileMode"></param>
  /// <param name="fileAccess"></param>
  public NX12File(string fileName)
  {
    this._fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
    this._binaryReader = new BinaryReader((Stream) this._fileStream);
    StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
    if (ordinalIgnoreCase.Compare(Encoding.ASCII.GetString(this._binaryReader.ReadBytes(8)), "SPLMSSTR") != 0)
      throw new Exception("Invalid signature");
    this._binaryReader.ReadBytes(9);
    long offset = this._binaryReader.ReadInt64();
    if (ordinalIgnoreCase.Compare(Encoding.ASCII.GetString(this._binaryReader.ReadBytes(6)), "HEADER") != 0)
      throw new Exception("Invalid header section name");
    int num1 = this._binaryReader.ReadInt32();
    for (int index = 0; index < num1; ++index)
      this._headerSections.Add(this.ReadSectionInfo());
    if (offset <= 0L)
      return;
    this._fileStream.Seek(offset, SeekOrigin.Begin);
    if (ordinalIgnoreCase.Compare(Encoding.Default.GetString(this._binaryReader.ReadBytes(6)), "FOOTER") != 0)
      throw new Exception("Invalid footer section name");
    int num2 = this._binaryReader.ReadInt32();
    for (int index = 0; index < num2; ++index)
      this._footerSections.Add(this.ReadSectionInfo());
  }

  private Tuple<string, long, long> ReadSectionInfo()
  {
    string str = Encoding.ASCII.GetString(this._binaryReader.ReadBytes(this._binaryReader.ReadInt32()));
    long num1 = this._binaryReader.ReadInt64();
    long num2 = this._binaryReader.ReadInt64();
    long num3 = num1;
    long num4 = num2;
    return new Tuple<string, long, long>(str, num3, num4);
  }

  public List<string> HeaderSections
  {
    get
    {
      return this._headerSections.Select<Tuple<string, long, long>, string>((Func<Tuple<string, long, long>, string>) (x => x.Item1)).ToList<string>();
    }
  }

  public List<string> FooterSections
  {
    get
    {
      return this._footerSections.Select<Tuple<string, long, long>, string>((Func<Tuple<string, long, long>, string>) (x => x.Item1)).ToList<string>();
    }
  }

  public byte[] GetSectionData(string sectionName)
  {
    if (this.FooterSections.Contains(sectionName))
    {
      Tuple<string, long, long> tuple = this._footerSections.FirstOrDefault<Tuple<string, long, long>>((Func<Tuple<string, long, long>, bool>) (x => x.Item1 == sectionName));
      if (tuple == null || tuple.Item2 == 0L || tuple.Item3 == 0L)
        return (byte[]) null;
      this._fileStream.Seek(tuple.Item2, SeekOrigin.Begin);
      return this._binaryReader.ReadBytes((int) tuple.Item3);
    }
    if (!this.HeaderSections.Contains(sectionName))
      return (byte[]) null;
    Tuple<string, long, long> tuple1 = this._headerSections.FirstOrDefault<Tuple<string, long, long>>((Func<Tuple<string, long, long>, bool>) (x => x.Item1 == sectionName));
    if (tuple1 == null || tuple1.Item2 == 0L || tuple1.Item3 == 0L)
      return (byte[]) null;
    this._fileStream.Seek(tuple1.Item2, SeekOrigin.Begin);
    return this._binaryReader.ReadBytes((int) tuple1.Item3);
  }

  public static bool IsNX12File(string fileName)
  {
    bool flag = false;
    try
    {
      using (FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          if (StringComparer.OrdinalIgnoreCase.Compare(Encoding.ASCII.GetString(binaryReader.ReadBytes(8)), "SPLMSSTR") == 0)
            flag = true;
          binaryReader.Close();
        }
        input.Close();
      }
    }
    catch
    {
    }
    return flag;
  }

  public void Dispose()
  {
    this._binaryReader.Close();
    this._binaryReader.Dispose();
    this._fileStream.Close();
    this._fileStream.Dispose();
  }
}
