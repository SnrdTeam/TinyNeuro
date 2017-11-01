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

// Copyright © Andrew Kirillov, 2005-2008
// andrew.kirillov@gmail.com

// Copyright © 2017 Adeptik, BestSoft

namespace TinyNeuro
{
    /// <summary>
    /// Activation function interface.
    /// </summary>
    /// 
    /// <remarks>All activation functions, which are supposed to be used with
    /// neurons, which calculate their output as a function of weighted sum of
    /// their inputs, should implement this interfaces.
    /// </remarks>
    /// 
    public interface IActivationFunction
    {
        /// <summary>
        /// Calculates function value.
        /// </summary>
        ///
        /// <param name="x">Function input value.</param>
        /// 
        /// <returns>Function output value, <i>f(x)</i>.</returns>
        ///
        /// <remarks>The method calculates function value at point <paramref name="x"/>.</remarks>
        ///
        double Function(double x);

        /// <summary>
        /// Calculates function derivative.
        /// </summary>
        /// 
        /// <param name="x">Function input value.</param>
        /// 
        /// <returns>Function derivative, <i>f'(x)</i>.</returns>
        /// 
        /// <remarks>The method calculates function derivative at point <paramref name="x"/>.</remarks>
        ///
        double Derivative(double x);

        /// <summary>
        /// Calculates function derivative.
        /// </summary>
        /// 
        /// <param name="y">Function output value - the value, which was obtained
        /// with the help of <see cref="Function"/> method.</param>
        /// 
        /// <returns>Function derivative, <i>f'(x)</i>.</returns>
        /// 
        /// <remarks><para>The method calculates the same derivative value as the
        /// <see cref="Derivative"/> method, but it takes not the input <b>x</b> value
        /// itself, but the function value, which was calculated previously with
        /// the help of <see cref="Function"/> method.</para>
        /// 
        /// <para><note>Some applications require as function value, as derivative value,
        /// so they can save the amount of calculations using this method to calculate derivative.</note></para>
        /// </remarks>
        /// 
        double Derivative2(double y);
    }
}
