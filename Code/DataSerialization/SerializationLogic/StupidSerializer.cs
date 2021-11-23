using DataSerialization.Serializable_Type;
using MainGame.Applicazione.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSerialization.SerializationLogic
{
    class StupidSerializer
    {

        protected Object serializableData;
        public StupidSerializer(Object _toBeSerialized)
        {

            serializableData = _toBeSerialized;
        }

        public void serializeData()
        {

            switch(serializableData)
            {
                case ChemicalElement element:
                 /*   ChemicalElement_Serializable chemicalElement_Serializable = new ChemicalElement_Serializable();

                    chemicalElement_Serializable.initFromOrig(serializableData);

                    Console.WriteLine(chemicalElement_Serializable.getCSVLine());*/
                    break;
            }
        }
    }
}
