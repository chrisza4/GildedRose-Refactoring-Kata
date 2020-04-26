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
            var updates = this.getItemUpdates();
            updates.Update();

            this.SellIn = updates.SellIn;
            this.Quality = updates.Quality;
        }

        private ItemUpdates getItemUpdates()
        {
            switch (this.Name)
            {
                case SpecialCase.AgedBrie:
                    return new AgedBrieUpdates(this);
                case SpecialCase.Backstage:
                    return new BackStageUpdates(this);
                case SpecialCase.Sulfurus:
                    return new SulfurusUpdates(this);
                default:
                    return new NormalUpdates(this);
            }
        }
    }

    public abstract class ItemUpdates
    {
        public int SellIn { get; set; }
        public int Quality { get; set; }
        private const int MAX_QUALITY = 50;

        protected void PromotionalIncrement()
        {
            if (this.Quality < MAX_QUALITY)
            {
                this.Quality++;
            }
        }

        protected void Reset()
        {
            this.Quality = 0;
        }

        protected void DecreaseQuality()
        {
            if (this.Quality <= 0)
            {
                return;
            }

            this.Quality--;
        }

        public ItemUpdates(Item item)
        {
            this.SellIn = item.SellIn;
            this.Quality = item.Quality;
        }

        public abstract void Update();
    }

    public class SulfurusUpdates : ItemUpdates
    {
        public SulfurusUpdates(Item item) : base(item) { }

        public override void Update()
        {
            // No-op
        }
    }

    public class AgedBrieUpdates : ItemUpdates
    {
        public AgedBrieUpdates(Item item) : base(item) { }

        public override void Update()
        {
            this.SellIn = this.SellIn - 1;

            this.PromotionalIncrement();

            if (this.SellIn < 0)
            {
                this.PromotionalIncrement();
            }
        }
    }

    public class BackStageUpdates : ItemUpdates
    {
        public BackStageUpdates(Item item) : base(item) { }

        public override void Update()
        {
            this.PromotionalIncrement();

            if (this.SellIn < 6)
            {
                this.PromotionalIncrement();
            }

            if (this.SellIn < 11)
            {
                this.PromotionalIncrement();
            }

            this.SellIn = this.SellIn - 1;

            if (this.SellIn < 0)
            {
                this.Reset();
            }
        }
    }

    public class NormalUpdates : ItemUpdates
    {
        public NormalUpdates(Item item) : base(item) { }

        public override void Update()
        {
            this.DecreaseQuality();

            this.SellIn = this.SellIn - 1;

            if (this.SellIn < 0)
            {
                this.DecreaseQuality();
            }
        }

    }
}
