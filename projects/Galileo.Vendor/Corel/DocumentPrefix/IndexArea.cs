﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galileo.Vendor.Corel
{
    public class IndexArea
    {

        public IndexHeader indexHeader { get; set; }
        public Index[] indexes { get; set; }


        public IndexArea(byte[] data, int startIndex)
        {
            indexHeader = new IndexHeader(data, startIndex);
            indexes = new Index[indexHeader.indexCount - 1];
            for (int i = 0; i < indexes.Length; i++)
            {
                startIndex += 14;
                indexes[i] = new Index(data, startIndex);
            }
        }

    }

    
}
