// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.OpenPDFFile
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using Intermech.Tools.Integrators;
using iTextSharp.text.pdf;
using System;
using System.IO;

#nullable disable
namespace Intermech.Tools.PDF;

internal sealed class OpenPDFFile : IDisposable, IValueBagContainer, IOpenDocument
{
  private readonly string fileName;
  private PdfReader reader;
  private PdfStamper stamper;
  private string stamperFileName;

  public OpenPDFFile(string fileName)
  {
    this.fileName = !string.IsNullOrEmpty(fileName) ? fileName : throw new ArgumentException();
  }

  public void Dispose()
  {
    this.CloseStamper(true);
    this.CloseReader();
  }

  public string FileName => this.fileName;

  IValueBagContainer IOpenDocument.Properties => (IValueBagContainer) this;

  public PdfReader Reader
  {
    get
    {
      if (this.reader == null)
        this.reader = new PdfReader(this.FileName);
      return this.reader;
    }
  }

  private void CloseReader()
  {
    if (this.reader == null)
      return;
    this.reader.Dispose();
    this.reader = (PdfReader) null;
  }

  public PdfStamper Stamper
  {
    get
    {
      if (this.stamper == null)
      {
        this.stamperFileName = this.FileName + ".$$$";
        this.stamper = new PdfStamper(this.Reader, (Stream) new FileStream(this.stamperFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
        this.stamper.SetFullCompression();
      }
      return this.stamper;
    }
  }

  private void CloseStamper(bool deleteFile)
  {
    if (this.stamper == null)
      return;
    this.stamper.Dispose();
    this.stamper = (PdfStamper) null;
    if (deleteFile && File.Exists(this.stamperFileName))
    {
      File.SetAttributes(this.stamperFileName, FileAttributes.Normal);
      File.Delete(this.stamperFileName);
    }
    this.stamperFileName = (string) null;
  }

  public void FlushChanges()
  {
    if (this.stamperFileName == null)
      return;
    string stamperFileName = this.stamperFileName;
    string str = this.FileName + ".bak";
    this.CloseStamper(false);
    this.CloseReader();
    File.Delete(str);
    File.Move(this.FileName, str);
    try
    {
      File.Move(stamperFileName, this.FileName);
      File.Delete(str);
    }
    catch
    {
      if (File.Exists(str))
      {
        File.Delete(this.FileName);
        File.Move(str, this.FileName);
      }
      throw;
    }
  }
}
