﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace EddiDataDefinitions
{
    /// <summary>
    /// Record defines the claims, fines and bounties associated with a faction, along with some additional data
    /// </summary>
    public class FactionRecord : INotifyPropertyChanged
    {
        // The faction associated with the claim, fine, or bounty
        [JsonIgnore]
        private string _faction;
        public string faction
        {
            get => _faction;
            set
            {
                if (_faction != value)
                {
                    _faction = value;
                    NotifyPropertyChanged("faction");
                }
            }
        }

        [JsonProperty("allegiance")]
        public string allegiance
        {

            get => Allegiance?.edname ?? Superpower.None.edname;
            set
            {
                Superpower aDef = Superpower.FromEDName(value);
                this.Allegiance = aDef;
            }
        }
        [JsonIgnore]
        private Superpower _Allegiance = Superpower.None;
        [JsonIgnore]
        public Superpower Allegiance
        {
            get { return _Allegiance; }
            set { _Allegiance = value; }
        }

        // The home system of the faction
        [JsonIgnore]
        private string _system;
        public string system
        {
            get => _system;
            set
            {
                if (_system != value)
                {
                    _system = value;
                    NotifyPropertyChanged("system");
                }
            }
        }

        // The home station of the faction
        [JsonIgnore]
        private string _station;
        public string station
        {
            get => _station;
            set
            {
                if (_station != value)
                {
                    _station = value;
                    NotifyPropertyChanged("station");
                }
            }
        }

        // The total credit value of claims
        [JsonIgnore]
        private long _claims;
        public long claims
        {
            get => _claims;
            set
            {
                if (_claims != value)
                {
                    _claims = value;
                    NotifyPropertyChanged("claims");
                }
            }
        }

        // The total credit value of fines
        [JsonIgnore]
        private long _fines;
        public long fines
        {
            get => _fines;
            set
            {
                if (_fines != value)
                {
                    _fines = value;
                    NotifyPropertyChanged("fines");
                }
            }
        }

        // The total credit value of bounties
        [JsonIgnore]
        private long _bounties;
        public long bounties
        {
            get => _bounties;
            set
            {
                if (_bounties != value)
                {
                    _bounties = value;
                    NotifyPropertyChanged("bounties");
                }
            }
        }

        public List<string> factionSystems { get; set; }
        public List<FactionReport> factionReports { get; set; }

        [JsonIgnore]
        // All bond claims, excluding the discrepancy report
        public long bondClaims => factionReports
            .Where(r => !r.bounty && r.crimeDef == Crime.None)
            .Sum(r => r.amount);

        [JsonIgnore]
        // All bounty claims, excluding the discrepancy report
        public long bountyClaims => factionReports
            .Where(r => r.bounty && r.crimeDef == Crime.None)
            .Sum(r => r.amount);

        [JsonIgnore]
        // All fine crimes, exlcuding the discrepancy report
        public long fineCrimes => factionReports
            .Where(r => !r.bounty && r.crimeDef != Crime.None && r.crimeDef != Crime.Fine)
            .Sum(r => r.amount);

        [JsonIgnore]
        // All bounty crimes, exlcuding the discrepancy report
        public long bountyCrimes => factionReports
            .Where(r => r.bounty && r.crimeDef != Crime.None && r.crimeDef != Crime.Bounty)
            .Sum(r => r.amount);

        // Default Constructor
        public FactionRecord() { }

        [JsonConstructor]
        public FactionRecord(string faction)
        {
            this.faction = faction;
            factionReports = new List<FactionReport>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}