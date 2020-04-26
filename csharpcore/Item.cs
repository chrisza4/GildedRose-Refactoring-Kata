namespace csharpcore
{
    public class Item
    {
        private const int MAX_QUALITY = 50;
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        public void Update()
        {
            switch (this.Name)
            {
                case GildedRose.SpecialCase.AgedBrie:
                    new AgedBrieUpdater(this).Update();
                    return;
                case GildedRose.SpecialCase.Backstage:
                    new BackStageUpdater(this).Update();
                    return;
                case GildedRose.SpecialCase.Sulfurus:
                    new SulfurusUpdater(this).Update();
                    return;
                default:
                    new NormalUpdater(this).Update();
                    return;
            }
        }
    }

    public abstract class ItemUpdater
    {
        protected Item item;
        private const int MAX_QUALITY = 50;

        protected void promotionalIncrement()
        {
            if (item.Quality < MAX_QUALITY)
            {
                item.Quality++;
            }
        }

        protected void reset()
        {
            item.Quality = 0;
        }

        protected void decreaseQuality()
        {
            if (item.Quality <= 0)
            {
                return;
            }

            item.Quality--;
        }

        public ItemUpdater(Item item)
        {
            this.item = item;
        }

        public abstract Item Update();
    }

    public class SulfurusUpdater : ItemUpdater
    {
        public SulfurusUpdater(Item item) : base(item)
        {
        }

        public override Item Update()
        {
            return this.item;
        }
    }

    public class AgedBrieUpdater : ItemUpdater
    {
        public AgedBrieUpdater(Item item) : base(item) { }

        public override Item Update()
        {
            item.SellIn = item.SellIn - 1;

            this.promotionalIncrement();

            if (item.SellIn < 0)
            {
                this.promotionalIncrement();
            }

            return item;
        }
    }

    public class BackStageUpdater : ItemUpdater
    {
        public BackStageUpdater(Item item) : base(item) { }

        public override Item Update()
        {
            this.promotionalIncrement();

            if (item.SellIn < 6)
            {
                this.promotionalIncrement();
            }

            if (item.SellIn < 11)
            {
                this.promotionalIncrement();
            }

            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0)
            {
                this.reset();
            }

            return item;
        }
    }

    public class NormalUpdater : ItemUpdater
    {
        public NormalUpdater(Item item) : base(item) { }

        public override Item Update()
        {
            this.decreaseQuality();

            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0)
            {
                this.decreaseQuality();
            }

            return item;
        }

    }
}
