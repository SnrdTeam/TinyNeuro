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

// Copyright © César Souza, 2009-2017
// cesarsouza at gmail.com

// Copyright © 2017 Adeptik, BestSoft

namespace TinyNeuro
{
    public interface IRandomNumberGenerator<T>
    {
        /// <summary>
        ///   Generates a random vector of observations from the current distribution.
        /// </summary>
        /// 
        /// <param name="samples">The number of samples to generate.</param>
        /// 
        /// <returns>A random vector of observations drawn from this distribution.</returns>
        /// 
        T[] Generate(int samples);

        /// <summary>
        ///   Generates a random vector of observations from the current distribution.
        /// </summary>
        /// 
        /// <param name="samples">The number of samples to generate.</param>
        /// <param name="result">The location where to store the samples.</param>
        /// 
        /// <returns>A random vector of observations drawn from this distribution.</returns>
        /// 
        T[] Generate(int samples, T[] result);

        /// <summary>
        ///   Generates a random observation from the current distribution.
        /// </summary>
        /// 
        /// <returns>A random observations drawn from this distribution.</returns>
        /// 
        T Generate();
    }
}
