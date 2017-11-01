/*
 * This file is part of TinyNeuro.
 * 
 *
 * TinyNeuro is free software: you can redistribute it and/or modify 
 * it under the terms of the GNU Lesser General Public License as 
 * published by the Free Software Foundation, either version 3 of 
 * the License, or (at your option) any later version.
 *
 * TinyNeuro is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty 
 * of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with TinyNeuro.  If not, see
 * <http://www.gnu.org/licenses/>.
 */

// Copyright © 2017 Adeptik, BestSoft

using System;

namespace TinyNeuro
{
    class SimpleRandomGenerator : IRandomNumberGenerator<double>
    {
        private Random random;
        public SimpleRandomGenerator()
        {
            random = new Random();
        }
        public double[] Generate(int samples)
        {
            double[] result = new double[samples];
            for (int i = 0; i < result.Length; i++)
                result[i] = random.NextDouble();
            return result;
        }

        public double[] Generate(int samples, double[] result)
        {
            double[] result2 = new double[samples];
            for (int i = 0; i < result.Length; i++)
                result2[i] = result[i] = random.NextDouble();
            return result2;
        }

        public double Generate()
        {
            return random.NextDouble();
        }
    }
}
