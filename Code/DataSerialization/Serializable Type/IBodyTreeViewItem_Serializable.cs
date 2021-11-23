using GameUI.UI.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSerialization.Serializable_Type
{
    class IBodyTreeViewItem_Serializable : Object_Serializable
    {
        public override string getCSVLine()
        {
            throw new NotImplementedException();
        }

        public override void initFromOrig(object _origData)
        {
            switch(_origData)
            {

                case Planet planet:
                    break;
            }
        }

        public override void initIntoOrig(object _origData)
        {
            throw new NotImplementedException();
        }
    }
}
