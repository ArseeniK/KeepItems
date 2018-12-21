using Smod.TestPlugin;
using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;

namespace KeepItems
{
    [PluginDetails(
        author = "ArseeniK",
        name = "KeepItems",
        description = "Allow you keep items when escape",
        id = "arsk.keepitems",
        version = "1.0",
        SmodMajor = 3,
        SmodMinor = 2,
        SmodRevision = 0
        )]
    class KeepItems : Plugin
    {
        public override void OnDisable()
        {
        }

        public override void OnEnable()
        {
            this.Info("KeepItems loaded");
        }

        public override void Register()
        {
            // Register Events
            this.AddEventHandler(typeof(IEventHandlerSetRole), new EventHandler(this), Priority.Highest);
            this.AddEventHandler(typeof(IEventHandlerCheckEscape), new EventHandler(this), Priority.Highest);
        }
    }
}
