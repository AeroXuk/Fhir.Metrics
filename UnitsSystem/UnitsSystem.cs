﻿/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fhir.UnitsSystem
{
    public class UnitsSystem
    {
        public Units Units = new Units();
        public Conversions Conversions = new Conversions();

        public void Add(string symbolfrom, string symbolto, ConversionMethod method)
        {
            Unit unitfrom = Units.FindUnit(symbolfrom);
            Unit unitto = Units.FindUnit(symbolto);
            Conversions.Add(unitfrom, unitto, method);
        }

        public Quantity Convert(Quantity quantity, Unit unit)
        {
            return Conversions.Convert(quantity, unit);
        }
        public Quantity Convert(Quantity quantity, Metric metric)
        {
            return Conversions.Convert(quantity, metric);
        }
        public Quantity ExpressionToQuantity(string expression)
        {
            Match matchValue = Regex.Match(expression, @"(\d+)");
            Match matchUnit = Regex.Match(expression, @"([^0-9]+)");
            
            string symbols = matchUnit.Value;
            decimal value = System.Convert.ToDecimal(matchValue.Value);
            Metric metric = Units.ParseMetric(symbols);
            
            Quantity quantity = new Quantity(value, metric);
            
            return quantity;
        }

        public string Convert(string expression, string metric)
        {
            Quantity q = ExpressionToQuantity(expression);
            Metric m = Units.ParseMetric(metric);

            Quantity output = this.Convert(q, m);
            return output.ToString();
            
        }
    }
}
