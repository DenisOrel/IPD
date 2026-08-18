// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.HelperClasses.DocNodeSelectedItemInfo
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS.HelperClasses;

internal struct DocNodeSelectedItemInfo(
  long id,
  long prjLinkID,
  int relationTypeID,
  string caption)
{
  public long Id = id;
  public long PrjLinkID = prjLinkID;
  public int RelationTypeID = relationTypeID;
  public string Caption = caption;
}
