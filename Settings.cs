using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AutoBudget
{
  #region settings
  [Serializable]
  public class AutoBudgetSettings
  {
    private static AutoBudgetSettings _instance;
    private static string settingsPath = Application.dataPath + "/../AutoBudgetSettings.xml";
    public int updateFrequency = 1000;
    public bool manageGarbage = false;
    public bool debug = false;
    public int[] buffers = new int[21];

    public static AutoBudgetSettings instance
    {
      get
      {
        if (_instance == null)
        {
          LoadSettings();
        }
        return _instance;
      }
    }

    private static void LoadSettings()
    {
      var serializer = new XmlSerializer(typeof(AutoBudgetSettings));
      if (!File.Exists(settingsPath))
      {
        _instance = new AutoBudgetSettings();
        return;
      }
      using (var stream = new FileStream(settingsPath, FileMode.Open))
      {
        var oldSettings = serializer.Deserialize(stream) as AutoBudgetSettings;
        if (oldSettings == null)
        {
          _instance = new AutoBudgetSettings();
          return;
        }
        else
        {
          _instance = oldSettings;
        }
      }
    }
    public void SaveSettings()
    {
      var serializer = new XmlSerializer(typeof(AutoBudgetSettings));
      using (var stream = new FileStream(settingsPath, FileMode.Create))
      {
        serializer.Serialize(stream, this);
      }
    }
  }
  #endregion
}
