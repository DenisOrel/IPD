// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.FileContent
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Text;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class FileContent
{
  private string fileName;
  private string content;
  private string[] lines;
  private static readonly string[] TextSplitPatterns = new string[3]
  {
    "\n\r",
    "\n",
    "\r"
  };

  public FileContent(string fileName, string content)
  {
    this.fileName = fileName;
    this.content = content;
    this.lines = this.content.Split(TextServices.TextLinesSplitPatterns, StringSplitOptions.RemoveEmptyEntries);
  }

  public string FileName => this.fileName;

  public string Content => this.content;

  public string[] Lines => this.lines;
}
