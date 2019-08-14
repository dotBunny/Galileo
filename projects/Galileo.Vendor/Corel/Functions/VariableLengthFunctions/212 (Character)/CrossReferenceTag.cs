using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galileo.Vendor.Corel
{
    public class CrossReferenceTag: CharacterGroupFunction
    {
        public CrossReferenceTagPacket tag { get; set; }

       public CrossReferenceTag(WP6Document doc, int index)
            : base(doc, index)
        {
            tag = new CrossReferenceTagPacket(doc, prefixIds[0]);
        }


    }
}
