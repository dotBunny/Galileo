using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galileo.Vendor.Corel
{
    public class SetLineSpacing : ParagraphGroupFunction
    {

        public double lineSpacing { get; set; }

        public SetLineSpacing(WP6Document doc, int index)
            : base(doc, index)
        {
             
            lineSpacing  = convertWPSPtoDouble(BitConverter.ToInt32(nonDeletableInfo, 0));
        }

    }
}
