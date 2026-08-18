// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.IAVSTemplatesViewsService
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Common_Dialogs;

#nullable disable
namespace Intermech.AVS;

internal interface IAVSTemplatesViewsService
{
  bool ShowAll { get; set; }

  bool ShowCommonTemplate { get; set; }

  DocumentType DocumentType { get; set; }

  long DocumetnTemplateId { get; }
}
