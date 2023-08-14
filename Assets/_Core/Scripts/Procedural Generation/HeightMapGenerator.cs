﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMan.Terrain
{
    public static class HeightMapGenerator
    {

        public static HeightMap GenerateHeightMap(int width, int height, HeightMapObject settings, Vector2 sampleCentre)
        {
            AnimationCurve heightCurve_threadsafe = new AnimationCurve(settings.heightCurve.keys);

            float[,] values = Noise.GenerateNoiseMap(width, height, settings.noiseSettings, sampleCentre);
            float[,] falloff = FalloffMapGenerator.GenerateFalloffMap(width);


            float minValue = float.MaxValue;
            float maxValue = float.MinValue;


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {


                    if (settings.useFalloff)
                    {
                        values[i, j] = Mathf.Clamp(values[i, j] - falloff[i, j], -1, 1);
                    }

                    values[i, j] *= heightCurve_threadsafe.Evaluate(values[i, j]) * settings.heightMultiplier;

                    if (values[i, j] > maxValue)
                    {
                        maxValue = values[i, j];
                    }
                    if (values[i, j] < minValue)
                    {
                        minValue = values[i, j];
                    }
                }
            }

            return new HeightMap(values, minValue, maxValue);
        }

    }

    public struct HeightMap
    {
        public readonly float[,] values;
        public readonly float minValue;
        public readonly float maxValue;

        public HeightMap(float[,] values, float minValue, float maxValue)
        {
            this.values = values;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }
}