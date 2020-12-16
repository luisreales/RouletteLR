﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteLR.DTO
{
    public class BetRequest
    {
        /// <summary>
        /// position 0-36, and 37=> red, 38 => black
        /// </summary>
        [Range(0, 38)]
        public int position { get; set; }

        /// <summary>
        /// Money to bet
        /// </summary>
        [Range(0.5d, maximum: 10000)]
        public double money { get; set; }
    }
}
