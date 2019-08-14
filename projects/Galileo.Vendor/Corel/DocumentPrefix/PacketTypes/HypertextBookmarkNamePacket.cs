﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galileo.Vendor.Corel
{
    public class HypertextBookmarkNamePacket: PacketData
    {
        public string bookmarkName { get; set; }

        public HypertextBookmarkNamePacket(WP6Document document, int prefixID):
            base(document, prefixID)
        {
            if (prefixID > 0)
            {
                bookmarkName = getWPWordString();
            }

        }

    }
}
