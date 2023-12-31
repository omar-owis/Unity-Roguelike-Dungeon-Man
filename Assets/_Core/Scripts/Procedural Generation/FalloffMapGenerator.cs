using UnityEngine;

namespace DungeonMan.Terrain
{
    public static class FalloffMapGenerator
    {
        public static float[,] GenerateFalloffMap(int chunkSize)
        {
            float[,] map = new float[chunkSize, chunkSize];

            for (int i = 0; i < chunkSize; i++)
            {
                for (int j = 0; j < chunkSize; j++)
                {
                    float x = i / (float)chunkSize * 2 - 1;
                    float y = j / (float)chunkSize * 2 - 1;

                    float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));

                    map[i, j] = Evaluate(value);
                }
            }

            return map;
        }

        static float Evaluate(float value)
        {
            float a = 3;
            float b = 2.2f;

            return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
        }
    }
}