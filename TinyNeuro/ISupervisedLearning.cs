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

// Copyright © 2007-2012 AForge.NET
// contacts@aforgenet.com

// Copyright © 2017 Adeptik, BestSoft

namespace TinyNeuro
{
    /// <summary>
    /// Supervised learning interface.
    /// </summary>
    /// 
    /// <remarks><para>The interface describes methods, which should be implemented
    /// by all supervised learning algorithms. Supervised learning is such
    /// type of learning algorithms, where system's desired output is known on
    /// the learning stage. So, given sample input values and desired outputs,
    /// system should adopt its internals to produce correct (or close to correct)
    /// result after the learning step is complete.</para></remarks>
    /// 
    public interface ISupervisedLearning
    {
        /// <summary>
        /// Runs learning iteration.
        /// </summary>
        /// 
        /// <param name="input">Input vector.</param>
        /// <param name="output">Desired output vector.</param>
        /// 
        /// <returns>Returns learning error.</returns>
        /// 
        double Run(double[] input, double[] output);

        /// <summary>
        /// Runs learning epoch.
        /// </summary>
        /// 
        /// <param name="input">Array of input vectors.</param>
        /// <param name="output">Array of output vectors.</param>
        /// 
        /// <returns>Returns sum of learning errors.</returns>
        /// 
        double RunEpoch(double[][] input, double[][] output);
    }
}
