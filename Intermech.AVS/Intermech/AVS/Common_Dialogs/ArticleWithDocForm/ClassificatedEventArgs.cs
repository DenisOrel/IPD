// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.ClassificatedEventArgs
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Аргументы для передачи сообщения об классификации</summary>
internal class ClassificatedEventArgs
{
  public IObjectClassificator Classifier;
  public long ClassifierID;

  public ClassificatedEventArgs(IObjectClassificator oClassifier, long cID)
  {
    this.Classifier = oClassifier;
    this.ClassifierID = cID;
  }
}
