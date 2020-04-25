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
                new ItemUpdater(Items[i]).Update();
            }
        }
    }

    public class ItemUpdater
    {
        private Item item;

        public ItemUpdater(Item item)
        {
            this.item = item;
        }

        public Item Update()
        {
            if (item.Name == GildedRose.SpecialCase.AgedBrie)
            {
                item.SellIn = item.SellIn - 1;

                if (item.Quality < 50)
                {
                    item.Quality = item.Quality + 1;
                }

                if (item.SellIn < 0 && item.Quality < 50)
                {
                    item.Quality = item.Quality + 1;
                }

                return item;
            }

            if (item.Name == GildedRose.SpecialCase.Backstage)
            {
                if (item.Quality < 50)
                {
                    item.Quality = item.Quality + 1;
                    if (item.SellIn < 11)
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }

                    if (item.SellIn < 6)
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }
                }

                item.SellIn = item.SellIn - 1;

                if (item.SellIn < 0)
                {
                    item.Quality = item.Quality - item.Quality;
                }

                return item;
            }

            if (item.Name == GildedRose.SpecialCase.Sulfurus)
            {
                return item;
            }

            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }

            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0)
            {
                if (item.Quality > 0)
                {
                    item.Quality = item.Quality - 1;
                }
            }

            return item;
        }
    }
}
