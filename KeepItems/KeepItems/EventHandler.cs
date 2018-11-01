using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

namespace Smod.TestPlugin
{
    class EventHandler : IEventHandlerSetRole, IEventHandlerCheckEscape
    {
        private Plugin plugin;

        public EventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }

        public static Dictionary<int, List<int>> escapeItems = new Dictionary<int, List<int>>();

        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            if (ev.Role != Role.SCIENTIST && ev.Role != Role.CLASSD) { return; }
            int playerId = ev.Player.PlayerId;
            int count = ev.Items.Count;
            Vector pos = ev.Player.GetPosition();
            float x = pos.x; float y = pos.y; float z = pos.z;
            y = y + 5; Vector itemPos = new Vector(x, y, z);
            /*foreach (KeyValuePair<int, List<int>> kv in escapeItems)
            {
                plugin.Debug("[KeepItems] Items found for player " + kv.Key + " : " + kv.Value + "");
            }*/
            if (escapeItems.ContainsKey(ev.Player.PlayerId)) {
                plugin.Debug("Escape Items found for " + ev.Player.Name + " (" + ev.Player.PlayerId + "). Have currently " + count + " items");
                if (escapeItems.TryGetValue(ev.Player.PlayerId, out List<int> tmp))
                {
                    foreach (int number in tmp)
                    {
                        if (count >= 8) {
                            PluginManager.Manager.Server.Map.SpawnItem((ItemType)number, itemPos, pos);
                        }
                        else { ev.Items.Add((ItemType)number); }
                        count++;
                    }
                }
                else { plugin.Debug("Failed to get values for player " + ev.Player.Name + " (" + ev.Player.PlayerId + ")"); }
                escapeItems.Remove(ev.Player.PlayerId);
            }
        }
        public void OnCheckEscape(PlayerCheckEscapeEvent ev)
        {
            List<int> tmp = new List<int>();
            foreach (Item item in ev.Player.GetInventory())
            {
                tmp.Add((int)item.ItemType);
                plugin.Debug("Added item " + (int)item.ItemType + " for " + item.ItemType);
            }
            if (tmp.Count > 0) { escapeItems.Add(ev.Player.PlayerId, tmp); }
            foreach (KeyValuePair<int, List<int>> kv in escapeItems)
            {
                // plugin.Debug("INFO ITEMS 2: " + kv.Key + " : " + kv.Value + "");
            }
        }
    }
}
