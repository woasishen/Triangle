using System.ComponentModel;

namespace TriangleTest.Entities
{
    class Matrix4X4
    {
        private readonly Vector4[] _data;

        public Vector4 this[int index]
        {
            get { return _data[index - 1]; }
            set { _data[index - 1] = value; }
        } 

        public Matrix4X4(bool init)
        {
            _data = new Vector4[4];
            for (var i = 1; i < 5; i++)
            {
                this[i] = new Vector4();
                if (init)
                {
                    this[i][i] = 1;
                }
            }
        }

        public Matrix4X4 Mul(Matrix4X4 matrix4X4)
        {
            var result = new Matrix4X4(false);
            for (var i = 1; i < 5; i++)
            {
                for (var j = 1; j < 5; j++)
                {
                    for (var k = 1; k < 5; k++)
                    {
                        result[i][j] += this[i][k]*matrix4X4[k][j];
                    }
                }
            }
            return result;
        }
    }
}
