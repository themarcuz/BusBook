using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xlns.BusBook.Core.Repository
{
    public class AllegatoRepository : CommonRepository
    {
        public Model.Allegato GetAllegatoById(int id) 
        {
            return base.getDomainObjectById<Model.Allegato>(id);
        }
    }
}
