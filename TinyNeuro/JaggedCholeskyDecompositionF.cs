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
//
// Original work copyright © Lutz Roeder, 2000
//  Adapted from Mapack for .NET, September 2000
//  Adapted from Mapack for COM and Jama routines
//  http://www.aisto.com/roeder/dotnet

// Copyright © 2017 Adeptik, BestSoft

using System;
using System.Threading.Tasks;

namespace TinyNeuro
{
    public class JaggedCholeskyDecompositionF
    {
        private bool undefined;
        private Single[][] L;
        private Single[] D;
        private int n;
        private bool robust;
        private bool positiveDefinite;
        public JaggedCholeskyDecompositionF(Single[][] value, bool robust = false,
           bool inPlace = false)
        {
            if (value.GetLength(0) != value[0].GetLength(0))
                throw new Exception("Matrix is not square.");

            //if (!inPlace)
            //    value = value.Copy();

            this.n = value.GetLength(0);
            this.L = value; //value.ToUpperTriangular(UpperTriangular, result: value);//так как по умолчанию UpperTriangular, ничего не надо делать
            this.robust = robust;

            if (robust)
            {
                LDLt(); // Compute square-root free decomposition
            }
            else
            {
                //LLt(); // Compute standard Cholesky decomposition
            }
        }
        private void LDLt()
        {
            D = new Single[n];

            this.positiveDefinite = true;

            Single[] v = new Single[n];
            for (int i = 0; i < L.Length; i++)
            {
                for (int j = 0; j < i; j++)
                    v[j] = L[i][j] * D[j];

                Single d = 0;
                for (int k = 0; k < i; k++)
                    d += L[i][k] * v[k];

                d = D[i] = v[i] = L[i][i] - d;

                // Use a tolerance for positive-definiteness
                this.positiveDefinite &= (v[i] > (Single)1e-14 * Math.Abs(L[i][i]));

                // If one of the diagonal elements is zero, the 
                // decomposition (without pivoting) is undefined.
                if (v[i] == 0) { undefined = true; return; }

                Parallel.For(i + 1, L.Length, k =>
                {
                    Single s = 0;
                    for (int j = 0; j < i; j++)
                        s += L[k][j] * v[j];

                    L[k][i] = (L[i][k] - s) / d;
                });
            }

            for (int i = 0; i < L.Length; i++)
                L[i][i] = 1;
        }
        public Single[] Solve(Single[] value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (value.Length != n)
                throw new ArgumentException("Argument vector should have the same length as rows in the decomposed matrix.", "value");

            if (!robust && !positiveDefinite)
                throw new Exception("Decomposed matrix is not positive definite.");

            //   if (destroyed)
            //     throw new InvalidOperationException("The decomposition has been destroyed.");

            if (undefined)
                throw new InvalidOperationException("The decomposition is undefined (zero in diagonal).");

            var B = new Single[value.Length];
            value.CopyTo(B, 0);

            // Solve L*Y = B;
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < k; i++)
                    B[k] -= B[i] * L[k][i];
                B[k] /= L[k][k];
            }

            if (robust)
            {
                for (int k = 0; k < D.Length; k++)
                    B[k] /= D[k];
            }

            // Solve L'*X = Y;
            for (int k = n - 1; k >= 0; k--)
            {
                for (int i = k + 1; i < n; i++)
                    B[k] -= B[i] * L[i][k];
                B[k] /= L[k][k];
            }

            return B;
        }
        public bool IsUndefined
        {
            get { return this.undefined; }
        }
    }
}
