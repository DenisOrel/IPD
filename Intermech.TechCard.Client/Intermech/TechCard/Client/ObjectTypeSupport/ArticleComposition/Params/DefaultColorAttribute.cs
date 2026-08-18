// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params.DefaultColorAttribute
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;
using System.Drawing;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;

/// <summary>Custom attribute for object types</summary>
internal class DefaultColorAttribute : Attribute
{
  /// <summary>
  /// 
  /// </summary>
  private readonly string _argbCode;

  /// <summary>Конструктор</summary>
  /// <param name="argbCode"></param>
  public DefaultColorAttribute(string argbCode) => this._argbCode = argbCode;

  /// <summary>Цвет по умолчанию</summary>
  public Color Color => ColorTranslator.FromHtml(this._argbCode);
}
