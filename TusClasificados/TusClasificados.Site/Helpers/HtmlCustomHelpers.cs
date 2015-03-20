using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TusClasificados.Site.Models;

namespace TusClasificados.Site.Helpers
{
    public static class HtmlCustomHelpers
    {
        public static String TipoCuentaColor(this HtmlHelper helper, TipoTicket tipo)
        {
            if (tipo == TipoTicket.Bronze)
                return "panel-default";
            else if (tipo == TipoTicket.Plata)
                return "panel-info";
            else if (tipo == TipoTicket.Oro)
                return "panel-primary";
            else
                return String.Empty;
        }
    }
}