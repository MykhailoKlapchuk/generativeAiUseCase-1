﻿using System.Text.Json.Serialization;

namespace UC_1.Models
{
    /// <summary>
    /// Capital Information class.
    /// </summary>
    public class CapitalInformation
    {
        /// <summary>
        /// GPS coordinates of the capital. [latitude, longitude]
        /// </summary>
        [JsonPropertyName("latlng")]
        private string[]? Latlng { get; set; }
    }
}
