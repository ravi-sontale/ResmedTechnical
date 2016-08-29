using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace Resmed.Stock.Service
{
    public abstract class ServiceBase
    {
        protected IUnityContainer UnityContainer { get; set; }
    }
}