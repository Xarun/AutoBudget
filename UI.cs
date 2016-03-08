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
  public partial class BudgetMod : IUserMod
  {
    #region Options menu
    private UISlider updateFrequencySlider;
    public void OnSettingsUI(UIHelperBase helper)
    {
      UIHelperBase group = helper.AddGroup("AutoBudget Settings");

      AddSlider(group, ItemClass.Service.Electricity);
      AddSlider(group, ItemClass.Service.Water);
      AddSlider(group, ItemClass.Service.Garbage);

      updateFrequencySlider = group.AddSlider("Update frequency (ms)", 0, 5000, 100, AutoBudgetSettings.instance.updateFrequency, OnFrequencyChange) as UISlider;
      group.AddCheckbox("Manage Garbage", AutoBudgetSettings.instance.manageGarbage, onGarbageCheckboxChange);
      group.AddCheckbox("Debug", AutoBudgetSettings.instance.debug, onDebugCheckboxChange);

      updateFrequencySlider.tooltip = AutoBudgetSettings.instance.updateFrequency.ToString();
    }

    private void AddSlider(UIHelperBase group, ItemClass.Service service)
    {
      var Slider = group.AddSlider("Budget buffer(" + service.ToString() + ")", 0, 50, 1, AutoBudgetSettings.instance.buffers[(int)service], EmptyCallback) as UISlider;
      var serviceNumber = (int)service;
      Slider.tooltip = Slider.value.ToString();
      Slider.eventValueChanged += delegate (UIComponent slider, float val)
      {
        slider.tooltip = val.ToString();
        AutoBudgetSettings.instance.buffers[serviceNumber] = (int)val;
        AutoBudgetSettings.instance.SaveSettings();
      };
    }

    private void EmptyCallback(float val)
    {
    }

    private void onDebugCheckboxChange(bool isChecked)
    {
      AutoBudgetSettings.instance.debug = isChecked;
      AutoBudgetSettings.instance.SaveSettings();
    }

    private void onGarbageCheckboxChange(bool isChecked)
    {
      AutoBudgetSettings.instance.manageGarbage = isChecked;
      AutoBudgetSettings.instance.SaveSettings();
    }

    private void OnFrequencyChange(float val)
    {
      AutoBudgetSettings.instance.updateFrequency = Mathf.RoundToInt(val);
      AutoBudgetSettings.instance.SaveSettings();
      updateFrequencySlider.tooltip = AutoBudgetSettings.instance.updateFrequency.ToString();
    }
    #endregion
  }
}
