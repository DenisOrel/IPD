// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Templates.TemplatesBody
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

#nullable disable
namespace Intermech.Imbase.Templates;

internal class TemplatesBody
{
  private string _body = string.Empty;
  private string _filter = string.Empty;
  private UseTemplate _useTemplate;

  internal TemplatesBody(string body, UseTemplate useTemplate)
  {
    this._body = body;
    this._useTemplate = useTemplate;
  }

  internal string Body
  {
    get => this._body;
    set => this._body = value;
  }

  internal string Filter
  {
    get => this._filter;
    set => this._filter = value;
  }

  private string GetTextForObject()
  {
    return this._body.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("\t", " ");
  }

  private string GetTextForRef() => this._filter;

  public override string ToString()
  {
    return this._useTemplate != UseTemplate.Obj ? this.GetTextForRef() : this.GetTextForObject();
  }
}
