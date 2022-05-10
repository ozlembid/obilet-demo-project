﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Domain.Entities.Session
{
    public class SessionRequestModel
    {
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public string IpAdress { get; set; }
        public string Port { get; set; }
        public int Type { get; set; }
    }
}
