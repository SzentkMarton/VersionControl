﻿using Hatodik_MTA4NG.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatodik_MTA4NG.Entities
{
    public class BallFactory: IToyFactory
    {
        public Toy CreateNew()
        {
            return new Ball();
        }

       
    }
}
