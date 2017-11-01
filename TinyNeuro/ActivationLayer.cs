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

    /// <summary>
    /// Activation layer.
    /// </summary>
    /// 
    /// <remarks>Activation layer is a layer of <see cref="ActivationNeuron">activation neurons</see>.
    /// The layer is usually used in multi-layer neural networks.</remarks>
    ///

    public class ActivationLayer : Layer
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivationLayer"/> class.
        /// </summary>
        /// 
        /// <param name="neuronsCount">Layer's neurons count.</param>
        /// <param name="inputsCount">Layer's inputs count.</param>
        /// <param name="function">Activation function of neurons of the layer.</param>
        /// 
        /// <remarks>The new layer is randomized (see <see cref="ActivationNeuron.Randomize"/>
        /// method) after it is created.</remarks>
        /// 
        public ActivationLayer(int neuronsCount, int inputsCount, IActivationFunction function)
            : base(neuronsCount, inputsCount)
        {
            // create each neuron
            for (int i = 0; i < neurons.Length; i++)
                neurons[i] = new ActivationNeuron(inputsCount, function);
        }
        private ActivationLayer() : base() { }
        /// <summary>
        /// Set new activation function for all neurons of the layer.
        /// </summary>
        /// 
        /// <param name="function">Activation function to set.</param>
        /// 
        /// <remarks><para>The methods sets new activation function for each neuron by setting
        /// their <see cref="ActivationNeuron.ActivationFunction"/> property.</para></remarks>
        /// 
        public void SetActivationFunction(IActivationFunction function)
        {
            for (int i = 0; i < neurons.Length; i++)
            {
                ((ActivationNeuron)neurons[i]).ActivationFunction = function;
            }
        }

        internal static JObject SerializeToJson(ActivationLayer layer)
        {
            JObject result = new JObject();
            result["InputsCount"] = layer.inputsCount;
            result["NeuronsCount"] = layer.neuronsCount;
            //  result["Outputs"] = new JArray(layer.output);
            JArray neurons = new JArray();
            for (int i = 0; i < layer.neurons.Length; i++)
            {
                ActivationNeuron neuron = (ActivationNeuron)layer.neurons[i];
                neurons.Add(ActivationNeuron.SerializeToJson(neuron));
            }
            result["Neurons"] = neurons;

            return result;
        }

        internal static ActivationLayer DeserializeFromJson(JObject jlayer)
        {
            ActivationLayer layer = new ActivationLayer();
            layer.inputsCount = jlayer["InputsCount"].ToObject<int>();
            layer.neuronsCount = jlayer["NeuronsCount"].ToObject<int>();
            //   layer.output = jlayer["Outputs"].ToObject<double[]>();
            layer.neurons = new Neuron[layer.neuronsCount];
            int counter = 0;
            foreach (JObject jneuron in jlayer["Neurons"].Children<JObject>())
            {
                layer.neurons[counter++] = ActivationNeuron.DeserializeFromJson(jneuron);
            }
            return layer;
        }
    }
}
