using System;
using System.Collections.Generic;

namespace csharpcore
{
    public class GildedRose
    {
        public static class SpecialCase
        {
            public const string AgedBrie = "Aged Brie";
            public const string Backstage = "Backstage passes to a TAFKAL80ETC concert";
            public const string Sulfurus = "Sulfuras, Hand of Ragnaros";
        }


        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                Items[i].Update();
            }
        }
    }
}
