﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galileo.Vendor.Corel
{
    public class EndMarkedTextForTableOfContents: CharacterGroupFunction
    {

        public int level { get; set; }
        public string ToCName { get; set; }

        public EndMarkedTextForTableOfContents(WP6Document doc, int index)
            : base(doc, index)
        {
            level = nonDeletableInfo[0];
            ToCName = getWPWordString(nonDeletableInfo, 1);
            
        }
    }
}
