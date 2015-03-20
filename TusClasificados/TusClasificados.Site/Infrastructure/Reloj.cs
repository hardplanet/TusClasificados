using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TusClasificados.Site.Infrastructure
{
    public class Reloj : IReloj
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}