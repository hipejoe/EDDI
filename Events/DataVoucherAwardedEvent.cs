﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EddiEvents
{
    public class DataVoucherAwardedEvent : Event
    {
        public const string NAME = "Data voucher awarded";
        public const string DESCRIPTION = "Triggered when you are awarded a data voucher";
        public const string SAMPLE = "{\"timestamp\":\"2016-06-10T14:32:03Z\",\"event\":\"DatalinkVoucher\",\"Reward\":500,\"VictimFaction\":\"Jarildekald Public Industry\",\"PayeeFaction\":\"Lencali Freedom Party\"}";

        public static Dictionary<string, string> VARIABLES = new Dictionary<string, string>();

        static DataVoucherAwardedEvent()
        {
            VARIABLES.Add("reward", "The number of credits received");
            VARIABLES.Add("victimfaction", "The name of the faction whose data you scanned");
            VARIABLES.Add("payeefaction", "The name of the faction awarding the voucher");
        }

        [JsonProperty("reward")]
        public long reward { get; private set; }

        [JsonProperty("victimfaction")]
        public string victimfaction { get; private set; }

        [JsonProperty("payeefaction")]
        public string payeefaction { get; private set; }

        public DataVoucherAwardedEvent(DateTime timestamp, string payeefaction, string victimfaction, long reward) : base(timestamp, NAME)
        {
            this.reward = reward;
            this.victimfaction = victimfaction;
            this.payeefaction = payeefaction;
        }
    }

    public class Reward
    {
        public string faction { get; private set; }
        public long amount { get; private set; }

        public Reward(string faction, long amount)
        {
            this.faction = faction;
            this.amount = amount;
        }
    }
}
