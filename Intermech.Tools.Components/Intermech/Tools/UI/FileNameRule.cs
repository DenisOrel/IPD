// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.UI.FileNameRule
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Tools.UI;

internal sealed class FileNameRule
{
  private readonly string ext;
  private readonly string namePattern;
  private readonly Regex nameRegex;
  private readonly string directory;
  private readonly LocalId<int> objectType;
  private string ruleName;

  public FileNameRule(string ext, LocalId<int> objectType)
    : this(ext, (string) null, (string) null, objectType)
  {
  }

  public FileNameRule(string ext, string namePattern, LocalId<int> objectType)
    : this(ext, namePattern, (string) null, objectType)
  {
  }

  public FileNameRule(string ext, string namePattern, string directory, LocalId<int> objectType)
  {
    if (string.IsNullOrEmpty(ext))
      throw new ArgumentException();
    if (objectType == null)
      throw new ArgumentNullException(nameof (objectType));
    this.ext = ext;
    this.namePattern = namePattern;
    this.nameRegex = string.IsNullOrEmpty(namePattern) ? (Regex) null : RegexHelper.ToRegex(namePattern, true);
    this.directory = directory;
    this.objectType = objectType;
  }

  public string Extension => this.ext;

  public string NamePattern => this.namePattern;

  public string Directory => this.directory;

  public LocalId<int> ObjectType => this.objectType;

  public bool IsMatch(string filePath)
  {
    if (!string.IsNullOrEmpty(this.directory) && !PathUtils.IsPlacedIn(filePath, this.directory))
      return false;
    bool flag = PathUtils.IsSamePath(Path.GetExtension(filePath), this.ext);
    if (flag && this.nameRegex != null && !this.nameRegex.IsMatch(Path.GetFileNameWithoutExtension(filePath)))
      flag = false;
    return flag;
  }

  public override string ToString()
  {
    if (this.ruleName == null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (!string.IsNullOrEmpty(this.namePattern))
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("SR_546"), (object) this.namePattern, (object) this.ext);
      else
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("SR_547"), (object) this.ext);
      if (!string.IsNullOrEmpty(this.directory))
      {
        stringBuilder.Append(' ');
        stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("SR_548"), (object) this.directory);
      }
      this.ruleName = stringBuilder.ToString();
    }
    return this.ruleName;
  }
}
