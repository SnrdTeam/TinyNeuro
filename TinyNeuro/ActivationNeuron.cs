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
    using Newtonsoft.Json.Linq;
    using System;

    /// <summary>
    /// Activation neuron.
    /// </summary>
    /// 
    /// <remarks><para>Activation neuron computes weighted sum of its inputs, adds
    /// threshold value and then applies <see cref="ActivationFunction">activation function</see>.
    /// The neuron isusually used in multi-layer neural networks.</para></remarks>
    /// 
    /// <seealso cref="IActivationFunction"/>
    /// 

    public class ActivationNeuron : Neuron
    {
        /// <summary>
        /// Threshold value.
        /// </summary>
        /// 
        /// <remarks>The value is added to inputs weighted sum before it is passed to activation
        /// function.</remarks>
        /// 
        protected double threshold = 0.0;

        /// <summary>
        /// Activation function.
        /// </summary>
        /// 
        /// <remarks>The function is applied to inputs weighted sum plus
        /// threshold value.</remarks>
        /// 
        protected IActivationFunction function = null;

        /// <summary>
        /// Threshold value.
        /// </summary>
        /// 
        /// <remarks>The value is added to inputs weighted sum before it is passed to activation
        /// function.</remarks>
        /// 
        public double Threshold
        {
            get { return threshold; }
            set { threshold = value; }
        }

        /// <summary>
        /// Neuron's activation function.
        /// </summary>
        /// 
        public IActivationFunction ActivationFunction
        {
            get { return function; }
            set { function = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivationNeuron"/> class.
        /// </summary>
        /// 
        /// <param name="inputs">Neuron's inputs count.</param>
        /// <param name="function">Neuron's activation function.</param>
        /// 
        public ActivationNeuron(int inputs, IActivationFunction function)
            : base(inputs)
        {
            this.function = function;
        }

        private ActivationNeuron() : base()
        {
        }

        /// <summary>
        /// Randomize neuron.
        /// </summary>
        /// 
        /// <remarks>Calls base class <see cref="Neuron.Randomize">Randomize</see> method
        /// to randomize neuron's weights and then randomizes threshold's value.</remarks>
        /// 
        public override void Randomize()
        {
            // randomize weights
            base.Randomize();

            // randomize threshold
            threshold = rand.Generate();
        }

        /// <summary>
        /// Computes output value of neuron.
        /// </summary>
        /// 
        /// <param name="input">Input vector.</param>
        /// 
        /// <returns>Returns neuron's output value.</returns>
        /// 
        /// <remarks><para>The output value of activation neuron is equal to value
        /// of nueron's activation function, which parameter is weighted sum
        /// of its inputs plus threshold value. The output value is also stored
        /// in <see cref="Neuron.Output">Output</see> property.</para>
        /// 
        /// <para><note>The method may be called safely from multiple threads to compute neuron's
        /// output value for the specified input values. However, the value of
        /// <see cref="Neuron.Output"/> property in multi-threaded environment is not predictable,
        /// since it may hold neuron's output computed from any of the caller threads. Multi-threaded
        /// access to the method is useful in those cases when it is required to improve performance
        /// by utilizing several threads and the computation is based on the immediate return value
        /// of the method, but not on neuron's output property.</note></para>
        /// </remarks>
        /// 
        /// <exception cref="ArgumentException">Wrong length of the input vector, which is not
        /// equal to the <see cref="Neuron.InputsCount">expected value</see>.</exception>
        /// 
        public override double Compute(double[] input)
        {
            // check for corrent input vector
            if (input.Length != inputsCount)
                throw new ArgumentException("Wrong length of the input vector.");

            // initial sum value
            double sum = 0.0;

            // compute weighted sum of inputs
            for (int i = 0; i < weights.Length; i++)
                sum += weights[i] * input[i];
            sum += threshold;

            // local variable to avoid mutlithreaded conflicts
            double output = function.Function(sum);

            // assign output property as well (works correctly for single threaded usage)
            this.output = output;

            return output;
        }

        internal static JObject SerializeToJson(ActivationNeuron neuron)
        {
            JObject result = new JObject();
            result["Threshold"] = neuron.threshold;
            result["InputsCount"] = neuron.inputsCount;
            result["Weights"] = new JArray(neuron.weights);
            //   result["Output"] = neuron.output;
            return result;
        }

        internal static ActivationNeuron DeserializeFromJson(JObject jneuron)
        {
            ActivationNeuron neuron = new ActivationNeuron();
            neuron.threshold = jneuron["Threshold"].ToObject<double>();
            neuron.inputsCount = jneuron["InputsCount"].ToObject<int>();
            neuron.weights = jneuron["Weights"].ToObject<double[]>();
            // neuron.output = jneuron["Output"].ToObject<double>();
            return neuron;
        }
    }
}
