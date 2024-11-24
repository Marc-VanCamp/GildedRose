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
}