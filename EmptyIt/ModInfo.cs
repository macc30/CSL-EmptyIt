﻿using ICities;
using System;

namespace EmptyIt
{
    public class ModInfo : IUserMod
    {
        public string Name => "Empty It!";
        public string Description => "Allows to automate the emptying of service buildings.";

        private static readonly string[] IntervalLabels =
        {
            "End of Day",
            "End of Month",
            "End of Year",
            "Every 5 seconds",
            "Every 10 seconds",
            "Every 30 seconds"
        };

        private static readonly int[] IntervalValues =
        {
            1,
            2,
            3,
            4,
            5,
            6
        };

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group;
            bool selected;
            int selectedIndex;
            int selectedValue;
            int result;

            group = helper.AddGroup(Name);

            selectedIndex = GetSelectedOptionIndex(IntervalValues, ModConfig.Instance.Interval);
            group.AddDropdown("Interval", IntervalLabels, selectedIndex, sel =>
            {
                ModConfig.Instance.Interval = IntervalValues[sel];
                ModConfig.Instance.Save();
            });

            group = helper.AddGroup("Landfill Sites");

            selected = ModConfig.Instance.EmptyLandfillSites;
            group.AddCheckbox("Empty Landfill Sites", selected, sel =>
            {
                ModConfig.Instance.EmptyLandfillSites = sel;
                ModConfig.Instance.Save();
            });

            selectedValue = ModConfig.Instance.ThresholdLandfillSites;
            group.AddTextfield("Threshold (percentage)", selectedValue.ToString(), sel =>
            {
                int.TryParse(sel, out result);
                ModConfig.Instance.ThresholdLandfillSites = result;
                ModConfig.Instance.Save();
            });

            group = helper.AddGroup("Cemeteries");

            selected = ModConfig.Instance.EmptyCemeteries;
            group.AddCheckbox("Empty Cemeteries", selected, sel =>
            {
                ModConfig.Instance.EmptyCemeteries = sel;
                ModConfig.Instance.Save();
            });

            selectedValue = ModConfig.Instance.ThresholdCemeteries;
            group.AddTextfield("Threshold (percentage)", selectedValue.ToString(), sel =>
            {
                int.TryParse(sel, out result);
                ModConfig.Instance.ThresholdCemeteries = result;
                ModConfig.Instance.Save();
            });

            group = helper.AddGroup("Snow Dumps");

            selected = ModConfig.Instance.EmptySnowDumps;
            group.AddCheckbox("Empty Snow Dumps", selected, sel =>
            {
                ModConfig.Instance.EmptySnowDumps = sel;
                ModConfig.Instance.Save();
            });

            selectedValue = ModConfig.Instance.ThresholdSnowDumps;
            group.AddTextfield("Threshold (percentage)", selectedValue.ToString(), sel =>
            {
                int.TryParse(sel, out result);
                ModConfig.Instance.ThresholdSnowDumps = result;
                ModConfig.Instance.Save();
            });
        }

        private int GetSelectedOptionIndex(int[] option, int value)
        {
            int index = Array.IndexOf(option, value);
            if (index < 0) index = 0;

            return index;
        }
    }
}