﻿using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System.Threading.Tasks;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            var resultMatrix = new Matrix(m1.RowCount, m2.ColCount);
            Parallel.For(0, m1.RowCount, i =>
            {
                for (int j = 0; j < m2.ColCount; j++)
                {
                    long temp = 0;
                    for (int k = 0; k < m1.ColCount; k++)
                    {
                        temp += m1.GetElement(i, k) * m2.GetElement(k, j);
                    }
                    resultMatrix.SetElement(i, j, temp);
                }
            }); 
            return resultMatrix;
        }
    }
}
