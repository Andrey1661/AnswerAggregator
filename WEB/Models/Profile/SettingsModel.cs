using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models.Profile
{
    public class SettingsModel
    {
        public string SettingName { get; set; }
        public object Value { get; set; }
        
    }
}