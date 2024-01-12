﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing wearable item information.
    /// </summary>
    public class WearableItem
    {
        /// <summary>
        /// The ID of the wearable item.
        /// </summary>
        public readonly int ID;

        /// <summary>
        /// The style of the wearable item.
        /// </summary>
        public readonly int Style;

        internal WearableItem(int id = 0, int style = 0)
        {
            ID = id;
            Style = style;
        }

        public override string ToString()
        {
            return $"[" +
                $"ID: {ID}, " +
                $"Style: {Style}" +
                $"]";
        }
    }

    /// <summary>
    /// Class representing player wearables information.
    /// </summary>
    public class PlayerWearables : Node
    {
        /// <summary>
        /// The dictionary of player's wearable items.
        /// </summary>
        public readonly Dictionary<int, WearableItem> Wearables = new Dictionary<int, WearableItem>();

        private Regex _wearable_regex = new Regex(@"wearable(\d+)");
        private Regex _style_regex = new Regex(@"style(\d+)");

        internal PlayerWearables(JObject parsed_data = null) : base(parsed_data)
        {
            GetMatchingIntegers(parsed_data, _wearable_regex, (Match match, int value) =>
            {
                var wearable_index = Convert.ToInt32(match.Groups[1].Value);

                if (!Wearables.ContainsKey(wearable_index))
                {
                    Wearables.Add(wearable_index, new WearableItem(value));
                }
                else
                {
                    var existing_wearable = Wearables[wearable_index];
                    Wearables[wearable_index] = new WearableItem(value, existing_wearable.Style);
                }
            });

            GetMatchingIntegers(parsed_data, _style_regex, (Match match, int value) =>
            {
                var wearable_index = Convert.ToInt32(match.Groups[1].Value);

                if (!Wearables.ContainsKey(wearable_index))
                {
                    Wearables.Add(wearable_index, new WearableItem(0, value));
                }
                else
                {
                    var existing_wearable = Wearables[wearable_index];
                    Wearables[wearable_index] = new WearableItem(existing_wearable.ID, value);
                }
            });
        }

        public override string ToString()
        {
            return $"[" +
                $"Wearables: {Wearables}" +
                $"]";
        }
    }
}