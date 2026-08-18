
// Type: Intermech.Navigator.SelectionView.ShowValueMode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.SelectionView;

/// <summary>Режим отображения элементов управления</summary>
[Flags]
public enum ShowValueMode
{
  svmNone = 0,
  svmObj = 1,
  svmString = 2,
  svmNumber = 4,
  svmDate = 8,
  svmBool = 16, // 0x00000010
  svmList = 32, // 0x00000020
  svmMulti = 64, // 0x00000040
  svmListMulti = 128, // 0x00000080
  svmFormula = 256, // 0x00000100
  svmInputObjectAttribute = 512, // 0x00000200
}
