using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xlns.BusBook.Core.Model
{
<<<<<<< HEAD
    public abstract class Allegato : ModelEntity
=======
    public class Allegato : ModelEntity
>>>>>>> 1e9dd669bb4aed4d8ebcaced2eae34fc49b52e2c
    {
        public virtual String NomeFile { get; set; }
        public virtual byte[] RawFile { get; set; }
    }
<<<<<<< HEAD

    public class AllegatoViaggio : Allegato
    {
        public virtual Viaggio Viaggio { get; set; }
    }
=======
>>>>>>> 1e9dd669bb4aed4d8ebcaced2eae34fc49b52e2c
   
}
