// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.HtmlTemplates
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

internal class HtmlTemplates
{
  private Regex regex = new Regex("{\\$(\\w+)}", RegexOptions.Compiled | RegexOptions.Singleline);
  public string TemplatesDir = "D:\\templates\\";
  private Dictionary<string, string> Data = new Dictionary<string, string>();
  private Dictionary<string, string> _cache = new Dictionary<string, string>();
  private TemplateInfo _firstTemplate;
  private Dictionary<string, TemplateInfo> _allTemplates;
  private TemplateInfo _current;
  private string _currentTemplateDir = "";

  public event HtmlTemplates.SubstitutionHandler OnSubstitute;

  public HtmlTemplates(string templatesDir)
  {
    this.TemplatesDir = FileFuncs.IncludeTrailingPathDelimiter(templatesDir);
  }

  public HtmlTemplates()
  {
  }

  private string GetTemplatePart(string template)
  {
    string templatePart = "";
    if (!this._cache.TryGetValue(template, out templatePart))
    {
      if (!Directory.Exists(this.CurrentTemplateDir))
        ResourceFuncs.ExtractResourcesFolder(typeof (Holder).Assembly, "templates", Holder.WorkflowTempPath);
      using (StreamReader streamReader = new StreamReader($"{this.CurrentTemplateDir}{template}.htm"))
      {
        templatePart = streamReader.ReadToEnd();
        this._cache.Add(template, templatePart);
      }
    }
    return templatePart;
  }

  private string MatchReplacer(Match m)
  {
    string lower = m.Groups[1].Value.ToLower();
    HtmlTemplates.SubstitutionHandler onSubstitute = this.OnSubstitute;
    if (onSubstitute != null)
      return onSubstitute(lower);
    string empty = string.Empty;
    return this.Data.TryGetValue(lower, out empty) ? empty : "???";
  }

  public string Parse(string part)
  {
    return this.regex.Replace(this.GetTemplatePart(part), new MatchEvaluator(this.MatchReplacer));
  }

  public void Assign(string name, object value)
  {
    name = name.ToLower();
    if (this.Data.ContainsKey(name))
      this.Data[name] = value.ToString();
    else
      this.Data.Add(name, value.ToString());
  }

  public Dictionary<string, TemplateInfo> AllTemplates
  {
    get
    {
      if (this._allTemplates == null)
      {
        this._allTemplates = new Dictionary<string, TemplateInfo>();
        if (!Directory.Exists(this.TemplatesDir))
          ResourceFuncs.ExtractResourcesFolder(typeof (Holder).Assembly, "templates", Holder.WorkflowTempPath);
        foreach (string directory in Directory.GetDirectories(this.TemplatesDir))
        {
          if (TemplateInfo.IsValidTemplate(directory))
          {
            TemplateInfo templateInfo = new TemplateInfo(directory);
            if (!this._allTemplates.ContainsKey(templateInfo.Name))
            {
              this._allTemplates.Add(templateInfo.Name, templateInfo);
              if (this._firstTemplate == null)
                this._firstTemplate = templateInfo;
            }
          }
        }
      }
      return this._allTemplates;
    }
  }

  public TemplateInfo Current => this._current;

  public string CurrentTemplateName
  {
    get => this.Current != null ? this.Current.Name : "";
    set
    {
      bool flag = this.Current == null;
      if (!this.AllTemplates.TryGetValue(value, out this._current) & flag)
        this._current = this._firstTemplate != null ? this._firstTemplate : throw new Exception($"Не найден шаблон для отображения почты по пути '{this.TemplatesDir}'!");
      if (this._current == null)
        return;
      this._currentTemplateDir = this._current.Directory;
      this._cache.Clear();
    }
  }

  public string CurrentTemplateDir => this._currentTemplateDir;

  public void SaveImage(ImageList il, int index, string fileName)
  {
    Color color = this.Current.ImgBGColor;
    PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
    if (color == Color.Transparent)
      color = Color.Fuchsia;
    else
      pixelFormat = PixelFormat.Format32bppRgb;
    Size imageSize = il.ImageSize;
    int width = imageSize.Width;
    imageSize = il.ImageSize;
    int height = imageSize.Height;
    int format = (int) pixelFormat;
    Bitmap bmp = new Bitmap(width, height, (PixelFormat) format);
    using (Graphics g = Graphics.FromImage((Image) bmp))
    {
      g.Clear(color);
      il.Draw(g, new Point(0, 0), index);
    }
    if (this.Current.ImgBGColor == Color.Transparent)
      bmp.MakeTransparent(Color.Fuchsia);
    using (Bitmap bitmap = BaseHolder.ImageTo16x16((Image) bmp) as Bitmap)
      bitmap.Save(fileName, ImageFormat.Png);
  }

  public delegate string SubstitutionHandler(string Tag);
}
