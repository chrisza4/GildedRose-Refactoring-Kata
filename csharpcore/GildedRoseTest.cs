using System.Collections.Generic;
using Xunit;

namespace csharpcore
{
    public class GildedRoseTest
    {
        [Fact]
        public void NormalQualityMoreThan0()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal("foo", Items[0].Name);
            Assert.Equal(-1, Items[0].SellIn);
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void NormalQualityLessThan0()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 1, Quality = 1 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal("foo", Items[0].Name);
            Assert.Equal(0, Items[0].SellIn);
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void NormalSellinLessThan0()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = -1, Quality = 4 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal("foo", Items[0].Name);
            Assert.Equal(-2, Items[0].SellIn);
            Assert.Equal(2, Items[0].Quality);
        }

        [Fact]
        public void AgedBreidLessThan50()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.SpecialCase.AgedBrie, SellIn = 3, Quality = 3 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.AgedBrie, Items[0].Name);
            Assert.Equal(2, Items[0].SellIn);
            Assert.Equal(4, Items[0].Quality);
        }

        [Fact]
        public void AgedBrieMoreThan50SellInMoreThan0()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.SpecialCase.AgedBrie, SellIn = 3, Quality = 51 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.AgedBrie, Items[0].Name);
            Assert.Equal(2, Items[0].SellIn);
            Assert.Equal(51, Items[0].Quality);
        }

        [Fact]
        public void AgedBrieMoreThan50SellInLessThan0()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.SpecialCase.AgedBrie, SellIn = -1, Quality = 51 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.AgedBrie, Items[0].Name);
            Assert.Equal(-2, Items[0].SellIn);
            Assert.Equal(51, Items[0].Quality);
        }

        [Fact]
        public void Sulfurus()
        {
            IList<Item> Items = new List<Item> {
                new Item {
                    Name = GildedRose.SpecialCase.Sulfurus,
                    SellIn = 1,
                    Quality = 1
                }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.Sulfurus, Items[0].Name);
            Assert.Equal(1, Items[0].SellIn);
            Assert.Equal(1, Items[0].Quality);
        }

        [Fact]
        public void Sulfurus2()
        {
            IList<Item> Items = new List<Item> {
                new Item {
                    Name = GildedRose.SpecialCase.Sulfurus,
                    SellIn = -1,
                    Quality = -1
                }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.Sulfurus, Items[0].Name);
            Assert.Equal(-1, Items[0].SellIn);
            Assert.Equal(-1, Items[0].Quality);
        }

        [Fact]
        public void Sulfurus3()
        {
            IList<Item> Items = new List<Item> {
                new Item {
                    Name = GildedRose.SpecialCase.Sulfurus,
                    SellIn = 0,
                    Quality = 50
                }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.Sulfurus, Items[0].Name);
            Assert.Equal(0, Items[0].SellIn);
            Assert.Equal(50, Items[0].Quality);
        }

        [Fact]
        public void BackStageSellinLessOrEqualThanZeroResetQuality1()
        {
            IList<Item> Items = new List<Item> {
                new Item {
                    Name = GildedRose.SpecialCase.Backstage,
                    SellIn = 0,
                    Quality = 4
                }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.Backstage, Items[0].Name);
            Assert.Equal(-1, Items[0].SellIn);
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void BackStageSellinLessOrEqualThanZeroResetQuality2()
        {
            IList<Item> Items = new List<Item> {
                new Item {
                    Name = GildedRose.SpecialCase.Backstage,
                    SellIn = -3,
                    Quality = 20
                }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.Backstage, Items[0].Name);
            Assert.Equal(-4, Items[0].SellIn);
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void BackStageSellinMoreThanZeroLessThan6PlusThreeToQuality()
        {
            IList<Item> Items = new List<Item> {
                new Item {
                    Name = GildedRose.SpecialCase.Backstage,
                    SellIn = 4,
                    Quality = 20
                }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.Backstage, Items[0].Name);
            Assert.Equal(3, Items[0].SellIn);
            Assert.Equal(23, Items[0].Quality);
        }

        [Fact]
        public void BackStageSellinMoreThan6LessThan11PlusTwoToQuality()
        {
            IList<Item> Items = new List<Item> {
                new Item {
                    Name = GildedRose.SpecialCase.Backstage,
                    SellIn = 8,
                    Quality = 20
                }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.Backstage, Items[0].Name);
            Assert.Equal(7, Items[0].SellIn);
            Assert.Equal(22, Items[0].Quality);
        }

        [Fact]
        public void BackStageSellinMoreThan11PlusOneToQuality()
        {
            IList<Item> Items = new List<Item> {
                new Item {
                    Name = GildedRose.SpecialCase.Backstage,
                    SellIn = 11,
                    Quality = 20
                }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.Backstage, Items[0].Name);
            Assert.Equal(10, Items[0].SellIn);
            Assert.Equal(21, Items[0].Quality);
        }

        [Fact]
        public void BackStageSellinMoreThan11Quality50()
        {
            IList<Item> Items = new List<Item> {
                new Item {
                    Name = GildedRose.SpecialCase.Backstage,
                    SellIn = 4,
                    Quality = 50
                }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.Backstage, Items[0].Name);
            Assert.Equal(3, Items[0].SellIn);
            Assert.Equal(50, Items[0].Quality);
        }

        [Fact]
        public void BackStageSellinMoreThan11Quality49()
        {
            IList<Item> Items = new List<Item> {
                new Item {
                    Name = GildedRose.SpecialCase.Backstage,
                    SellIn = 4,
                    Quality = 49
                }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.Equal(GildedRose.SpecialCase.Backstage, Items[0].Name);
            Assert.Equal(3, Items[0].SellIn);
            Assert.Equal(50, Items[0].Quality);
        }
    }
}
