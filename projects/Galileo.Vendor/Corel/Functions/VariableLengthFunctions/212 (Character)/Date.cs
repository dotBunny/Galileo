﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galileo.Vendor.Corel
{
    public class Date : CharacterGroupFunction
    {

        public Date(WP6Document doc, int index)
            : base(doc, index)
        {
            // nothing to do, since packet ID is not documented
            // everything that is included between the functions is the text for the date.
        }
    }
}
