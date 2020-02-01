﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TotalNetCore.DDDAPISample.Application.Configuration.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
