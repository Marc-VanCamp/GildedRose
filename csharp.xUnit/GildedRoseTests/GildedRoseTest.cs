using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void foo()
    {
        // Unit test Common items: SellIn & quality
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 1, Quality = 5 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("foo", Items[0].Name);
        Assert.Equal(0, Items[0].SellIn);
        Assert.Equal(4, Items[0].Quality);
        app.UpdateQuality();
        Assert.Equal(-1, Items[0].SellIn);
        Assert.Equal(2, Items[0].Quality);
        app.UpdateQuality();
        Assert.Equal(-2, Items[0].SellIn);
        Assert.Equal(0, Items[0].Quality);
        app.UpdateQuality();
        Assert.Equal(-3, Items[0].SellIn);
        Assert.Equal(0, Items[0].Quality);
    }
    [Fact]
    public void brie()
    {
        // Unit test Aged Brie items: SellIn & quality
        IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 1, Quality = 48 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("Aged Brie", Items[0].Name);
        Assert.Equal(0, Items[0].SellIn);
        Assert.Equal(49, Items[0].Quality);
        app.UpdateQuality();
        Assert.Equal(-1, Items[0].SellIn);
        Assert.Equal(50, Items[0].Quality);
        app.UpdateQuality();
        Assert.Equal(-2, Items[0].SellIn);
        Assert.Equal(50, Items[0].Quality);
    }
    [Fact]
    public void sulfuras()
    {
        // Unit test Sulfuras items: SellIn & quality
        IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 25 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("Sulfuras, Hand of Ragnaros", Items[0].Name);
        Assert.Equal(1, Items[0].SellIn);
        Assert.Equal(25, Items[0].Quality);
        app.UpdateQuality();
    }
}