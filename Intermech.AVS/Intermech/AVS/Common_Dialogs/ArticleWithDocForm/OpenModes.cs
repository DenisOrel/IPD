// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.OpenModes
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Режим открытия</summary>
internal enum OpenModes
{
  /// <summary>Создание объектов</summary>
  Create,
  /// <summary>Просмотр в диалоге</summary>
  View,
  /// <summary>Добавление существующего при  создании</summary>
  CreateAdd,
  /// <summary>Просмотр во вьюшке</summary>
  InView,
  /// <summary>Просмотр во вьюшке в режиме для чтения</summary>
  InViewReadOnly,
}
