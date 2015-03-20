using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TusClasificados.Site.Infrastructure
{
    public interface IReloj
    {
        DateTime Now { get; }
    }
}
