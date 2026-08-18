
// Type: Intermech.Interfaces.ObjectSteps
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Структура, описывающая шаги ЖЦ объекта</summary>
    [Serializable]
    public struct ObjectSteps(int lcStep, string stepName, int atribute, byte[] icon)
    {
      public int LCStep = lcStep;
      public string StepName = stepName;
      public int Atribute = atribute;
      public byte[] Icon = icon;
    }
}
