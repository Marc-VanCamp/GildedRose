using Xunit;
using System.Collections.Generic;
using GildedRoseKata;
using System;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void common()
    {
        // Unit test Common items: SellIn & quality
        // In the original code, the 'Common' items appear to decrease in quality by 2
        // Check with business how to handle this.
        // For now assume, 'Common' decrease Quality in a regular pattern by 1
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 1, Quality = 2 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("foo", Items[0].Name);
        Assert.Equal(0, Items[0].SellIn);
        Assert.Equal(1, Items[0].Quality);
        app.UpdateQuality();
        Assert.Equal(-1, Items[0].SellIn);
        Assert.Equal(0, Items[0].Quality);
        app.UpdateQuality();
        Assert.Equal(-2, Items[0].SellIn);
        Assert.Equal(0, Items[0].Quality);
    }
    [Fact]
    public void aged()
    {
        // Unit test Aged  items: SellIn & quality
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
        IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 80 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("Sulfuras, Hand of Ragnaros", Items[0].Name);
        Assert.Equal(1, Items[0].SellIn);
        Assert.Equal(80, Items[0].Quality);

    }
    [Fact]
    public void backstage()
    {
        // Unit test Backstage Passes items: SellIn & quality
        // The original code contained inconcistencies in the SellIn decrease and the bounderies '> or >= ' for quality seems not to be respected.
        // This feature has been leveled to a decrease of 1 for each product at the beginning of the Quality Update
        // This test needs to start at 12
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 12, Quality = 40 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("Backstage passes to a TAFKAL80ETC concert", Items[0].Name);
        Assert.Equal(11, Items[0].SellIn);
        Assert.Equal(41, Items[0].Quality);
        Items[0].Quality = 40;
        app.UpdateQuality();
        Assert.Equal(10, Items[0].SellIn);
        Assert.Equal(42, Items[0].Quality);
        // Fastforward
        Items[0].SellIn = 7;
        Items[0].Quality = 40;
        app.UpdateQuality();
        Assert.Equal(6, Items[0].SellIn);
        Assert.Equal(42, Items[0].Quality);
        Items[0].Quality = 40;
        app.UpdateQuality();
        Assert.Equal(5, Items[0].SellIn);
        Assert.Equal(43, Items[0].Quality);

        Items[0].SellIn = 1;
        Items[0].Quality = 40;
        app.UpdateQuality();
        Assert.Equal(0, Items[0].SellIn);
        Assert.Equal(43, Items[0].Quality);

        // Upper limit = 50
        Items[0].SellIn = 1;
        Items[0].Quality = 49;
        app.UpdateQuality();
        Assert.Equal(0, Items[0].SellIn);
        Assert.Equal(50, Items[0].Quality);

        app.UpdateQuality();
        Assert.Equal(-1, Items[0].SellIn);
        Assert.Equal(50, Items[0].Quality);
    }
    [Fact]
    public void itemValidations()
    {
        // Unit test item Validations quality

        IList<Item> Items = new List<Item> { new Item { Name = "foo, something", SellIn = 5, Quality = -1 } };
        var exc = Assert.Throws<Exception>(() => (new GildedRose(Items)).UpdateQuality());
        Assert.Contains("Quality cannot be negative", exc.Message);

        Items = new List<Item> { new Item { Name = "foo, something", SellIn = 5, Quality = 51 } };
        exc = Assert.Throws<Exception>(() => (new GildedRose(Items)).UpdateQuality());
        Assert.Contains("Quality cannot be more than 50", exc.Message);
        
        Items = new List<Item> { new Item { Name = "Sulfuras, something", SellIn = 5, Quality = 51 } };
        exc = Assert.Throws<Exception>(() => (new GildedRose(Items)).UpdateQuality());
        Assert.Contains("Sulfuras quality must be 80", exc.Message);

        Items = new List<Item> { new Item { Name = "Sulfuras, something", SellIn = 5, Quality = 0 } };
        exc = Assert.Throws<Exception>(() => (new GildedRose(Items)).UpdateQuality());
        Assert.Contains("Sulfuras quality must be 80", exc.Message);
        
        Items = new List<Item> { new Item { Name = "Aged, something", SellIn = 5, Quality = 100 } };
        exc = Assert.Throws<Exception>(() => (new GildedRose(Items)).UpdateQuality());
        Assert.Contains("Quality cannot be more than 50", exc.Message);

        Items = new List<Item> { new Item { Name = "Backstage, something", SellIn = 5, Quality = 75 } };
        exc = Assert.Throws<Exception>(() => (new GildedRose(Items)).UpdateQuality());
        Assert.Contains("Quality cannot be more than 50", exc.Message);

    }

    [Fact]
    public void conjured()
    {
        // Unit test Conjured items: SellIn & quality
        // Conjured Mana Cake, 3, 6
        //IList<Item> Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 } };
        //GildedRose app = new GildedRose(Items);
        //app.UpdateQuality();
        //Assert.Equal("Conjured Mana Cake", Items[0].Name);
        //Assert.Equal(2, Items[0].SellIn);
        //Assert.Equal(4, Items[0].Quality);

        IList<Item> Items = new List<Item> { new Item { Name = "Conjured, Elara the Enchantress", SellIn = 5, Quality = 11 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("Conjured, Elara the Enchantress", Items[0].Name);
        Assert.Equal(4, Items[0].SellIn);
        Assert.Equal(9, Items[0].Quality);

        app.UpdateQuality();
        Assert.Equal(3, Items[0].SellIn);
        Assert.Equal(7, Items[0].Quality);

        app.UpdateQuality();
        app.UpdateQuality();
        app.UpdateQuality();
        Assert.Equal(0, Items[0].SellIn);
        Assert.Equal(1, Items[0].Quality);

        app.UpdateQuality();
        Assert.Equal(-1, Items[0].SellIn);
        Assert.Equal(0, Items[0].Quality);

        app.UpdateQuality();
        Assert.Equal(-2, Items[0].SellIn);
        Assert.Equal(0, Items[0].Quality);
    }
}