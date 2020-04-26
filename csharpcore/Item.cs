namespace csharpcore
{
    public class Item
    {
        private const int MAX_QUALITY = 50;

        public static class SpecialCase
        {
            public const string AgedBrie = "Aged Brie";
            public const string Backstage = "Backstage passes to a TAFKAL80ETC concert";
            public const string Sulfurus = "Sulfuras, Hand of Ragnaros";
        }

        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        public void Update()
        {
            var updates = this.getUpdater().Update();
            this.SellIn = updates.SellIn;
            this.Quality = updates.Quality;
        }

        private ItemUpdater getUpdater()
        {
            switch (this.Name)
            {
                case SpecialCase.AgedBrie:
                    return new AgedBrieUpdater(this);
                case SpecialCase.Backstage:
                    return new BackStageUpdater(this);
                case SpecialCase.Sulfurus:
                    return new SulfurusUpdater(this);
                default:
                    return new NormalUpdater(this);
            }
        }
    }

    public class ItemUpdates
    {
        public int SellIn { get; set; }
        public int Quality { get; set; }
        private const int MAX_QUALITY = 50;

        public void PromotionalIncrement()
        {
            if (this.Quality < MAX_QUALITY)
            {
                this.Quality++;
            }
        }

        public void Reset()
        {
            this.Quality = 0;
        }

        public void DecreaseQuality()
        {
            if (this.Quality <= 0)
            {
                return;
            }

            this.Quality--;
        }
    }

    public abstract class ItemUpdater
    {
        protected ItemUpdates itemUpdates;
        private const int MAX_QUALITY = 50;

        public ItemUpdater(Item item)
        {
            this.itemUpdates = new ItemUpdates()
            {
                SellIn = item.SellIn,
                Quality = item.Quality
            };
        }

        public abstract ItemUpdates Update();
    }

    public class SulfurusUpdater : ItemUpdater
    {
        public SulfurusUpdater(Item item) : base(item) { }

        public override ItemUpdates Update()
        {
            return this.itemUpdates;
        }
    }

    public class AgedBrieUpdater : ItemUpdater
    {
        public AgedBrieUpdater(Item item) : base(item) { }

        public override ItemUpdates Update()
        {
            itemUpdates.SellIn = itemUpdates.SellIn - 1;

            itemUpdates.PromotionalIncrement();

            if (itemUpdates.SellIn < 0)
            {
                itemUpdates.PromotionalIncrement();
            }

            return itemUpdates;
        }
    }

    public class BackStageUpdater : ItemUpdater
    {
        public BackStageUpdater(Item item) : base(item) { }

        public override ItemUpdates Update()
        {
            itemUpdates.PromotionalIncrement();

            if (itemUpdates.SellIn < 6)
            {
                itemUpdates.PromotionalIncrement();
            }

            if (itemUpdates.SellIn < 11)
            {
                itemUpdates.PromotionalIncrement();
            }

            itemUpdates.SellIn = itemUpdates.SellIn - 1;

            if (itemUpdates.SellIn < 0)
            {
                itemUpdates.Reset();
            }

            return itemUpdates;
        }
    }

    public class NormalUpdater : ItemUpdater
    {
        public NormalUpdater(Item item) : base(item) { }

        public override ItemUpdates Update()
        {
            itemUpdates.DecreaseQuality();

            itemUpdates.SellIn = itemUpdates.SellIn - 1;

            if (itemUpdates.SellIn < 0)
            {
                itemUpdates.DecreaseQuality();
            }

            return itemUpdates;
        }

    }
}
