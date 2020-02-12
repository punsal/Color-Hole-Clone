using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Utilities.Extensions
{
    public static class CustomMath
    {
        public static List<int> Randomize(int itemCount, int expectedRandomCount)
        {
            if (itemCount < expectedRandomCount)
            {
                throw new ArgumentOutOfRangeException();
            }
        
            var items = new List<int>();
            for (var i = 0; i < itemCount; i++)
            {
                items.Add(i);
            }
        
            var result = new List<int>();
            for (var i = 0; i < expectedRandomCount; i++)
            {
                var randomIndex = Random.Range(0, items.Count);
                result.Add(items[randomIndex]);
                items.RemoveAt(randomIndex);
            }

            return result;
        } 
    }
}