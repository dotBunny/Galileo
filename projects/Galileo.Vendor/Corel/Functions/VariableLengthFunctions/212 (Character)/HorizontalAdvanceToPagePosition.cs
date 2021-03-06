﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galileo.Vendor.Corel
{
    public class HorizontalAdvanceToPagePosition: CharacterGroupFunction    
    {

        public bool absolute { get; set; }
        public short adjustment { get; set; }

      public HorizontalAdvanceToPagePosition(WP6Document doc, int index)
            : base(doc, index)
        {
            absolute = nonDeletableInfo[0] > 0;
            adjustment = BitConverter.ToInt16(nonDeletableInfo, 1);
        }
    }
}
