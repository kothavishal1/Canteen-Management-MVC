﻿using CMApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMApp.Areas.Admin.Models
{
    public class Base
    {
        protected ElectricsOnlineEntities _ctx = new ElectricsOnlineEntities();
    }
}