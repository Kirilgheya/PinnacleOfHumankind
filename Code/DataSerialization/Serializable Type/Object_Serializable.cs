using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSerialization.Serializable_Type
{
    abstract class Object_Serializable
    {
      
    
        public abstract void initFromOrig(Object _origData);

        public abstract void initIntoOrig(object _origData);

        public abstract string getCSVLine();
    }
}
