﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN_Payer.Model
{
    public class ProcessPaymentModel
    {
        public static string Result { get; set; }
        public static string ErrNum { get; set; }
        public static string Receipt { get; set; }
        public static string AuthCode { get; set; }

    }
}
