﻿using Eddi;
using EddiDataDefinitions;
using EddiEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Utilities;

namespace EddiCargoMonitor
{
    /// <summary>
    /// A monitor that keeps track of cargo
    /// </summary>
    public class CargoMonitor : EDDIMonitor
    {
        // The file to log cargo
        public static readonly string CargoFile = Constants.DATA_DIR + @"\cargo.json";

        // The cargo
        private List<Cargo> cargo = new List<Cargo>();

        public string MonitorName()
        {
            return "Cargo monitor";
        }

        public string MonitorVersion()
        {
            return "1.0.0";
        }

        public string MonitorDescription()
        {
            return "Tracks your cargo and provides information to speech responder scripts.";
        }

        public CargoMonitor()
        {
            ReadCargo();
            Logging.Info("Initialised " + MonitorName() + " " + MonitorVersion());
        }

        public void Start()
        {
            // We don't actively do anything, just listen to events, so nothing to do here
        }

        public void Stop()
        {
        }

        public void Reload()
        {
            ReadCargo();
        }

        public UserControl ConfigurationTabItem()
        {
            return null;
        }

        public void Handle(Event @event)
        {
            Logging.Debug("Received event " + JsonConvert.SerializeObject(@event));
            // Handle the events that we care about

            if (@event is CargoInventoryEvent)
            {
                handleCargoInventoryEvent((CargoInventoryEvent)@event);
            }
        }

        private void handleCargoInventoryEvent(CargoInventoryEvent @event)
        {

        }

        public IDictionary<string, object> GetVariables()
        {
            IDictionary<string, object> variables = new Dictionary<string, object>();
            //variables["cargo"] = configuration.materials;

            return variables;
        }

        private void ReadCargo()
        {
            try
            {
                cargo = JsonConvert.DeserializeObject<List<Cargo>>(File.ReadAllText(CargoFile));
            }
            catch (Exception ex)
            {
                Logging.Warn("Failed to read cargo", ex);
            }
            if (cargo == null)
            {
                cargo = new List<Cargo>();
            }
        }

        private void WriteCargo()
        {
            try
            {
                File.WriteAllText(CargoFile, JsonConvert.SerializeObject(this, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Logging.Warn("Failed to write cargo", ex);
            }
        }
    }
}