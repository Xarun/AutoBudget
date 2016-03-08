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
    #region Implementation of IUserMod

    public string Name { get { return "AutoBudget V2"; } }
    public string Description { get { return "Automatically sets budgets to the lowest possible value to maintain a 'green' status for electricity and water/sewage."; } }

    #endregion Implementation of IUserMod
  }
}
