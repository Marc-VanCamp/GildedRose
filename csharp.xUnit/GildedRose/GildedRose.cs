using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using static GildedRoseKata.GildedRose;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }
    public enum ItemTypes
    {
        Common,
        Aged,
        Sulfuras,
        Backstage,
        Conjured
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            // Validate all items first
            item.ValidateAndGetType();
        }
        foreach (var item in Items)
        {
            var itemType = item.ValidateAndGetType();
            switch (itemType)
            {
                case ItemTypes.Sulfuras:
                    break;
                default:
                    item.SellIn = item.SellIn - 1;
                    break;
            }
            switch (itemType)
            {
                case ItemTypes.Common:
                    if (item.Quality > 0)
                    {
                        item.Quality = item.Quality - 1;
                    }
                    break;
                case ItemTypes.Aged:
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                    break;
                case ItemTypes.Sulfuras:
                    // Keeps quality (at 80)
                    break;
                case ItemTypes.Backstage:
                    if (item.SellIn > 10 || item.SellIn < 0)
                    {
                        item.Quality += 1;
                    } else if (item.SellIn > 5)
                    {
                        item.Quality += 2;
                    }
                    else if (item.SellIn >= 0)
                    {
                        item.Quality += 3;
                    }
                    item.Quality = Math.Min(50, item.Quality);
                    break;
                case ItemTypes.Conjured:
                    break;
            }
            continue;


            //if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            //{
            //    if (Items[i].Quality > 0)
            //    {
            //        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            //        {
            //            Items[i].Quality = Items[i].Quality - 1;
            //        }
            //    }
            //}
            //else
            //{
            //    if (Items[i].Quality < 50)
            //    {
            //        Items[i].Quality = Items[i].Quality + 1;

            //        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
            //        {
            //            if (Items[i].SellIn < 11)
            //            {
            //                if (Items[i].Quality < 50)
            //                {
            //                    Items[i].Quality = Items[i].Quality + 1;
            //                }
            //            }

            //            if (Items[i].SellIn < 6)
            //            {
            //                if (Items[i].Quality < 50)
            //                {
            //                    Items[i].Quality = Items[i].Quality + 1;
            //                }
            //            }
            //        }
            //    }
            //}

            //if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            //{
            //    Items[i].SellIn = Items[i].SellIn - 1;
            //}

            //if (Items[i].SellIn < 0)
            //{
            //    if (Items[i].Name != "Aged Brie")
            //    {
            //        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            //        {
            //            if (Items[i].Quality > 0)
            //            {
            //                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            //                {
            //                    Items[i].Quality = Items[i].Quality - 1;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            Items[i].Quality = Items[i].Quality - Items[i].Quality;
            //        }
            //    }
            //    else
            //    {
            //        if (Items[i].Quality < 50)
            //        {
            //            Items[i].Quality = Items[i].Quality + 1;
            //        }
            //    }
            //}
        }
    }
}

public static class ExtenstionsMethods
{
    public static ItemTypes ValidateAndGetType(this Item item)
    {
        var itemType = item.Name switch
        {
            var name when name.StartsWith("Aged") => GildedRose.ItemTypes.Aged,
            var name when name.StartsWith("Sulfuras") => GildedRose.ItemTypes.Sulfuras,
            var name when name.StartsWith("Backstage") => GildedRose.ItemTypes.Backstage,
            var name when name.StartsWith("Conjured") => GildedRose.ItemTypes.Conjured,
            _ => GildedRose.ItemTypes.Common
        };
        if (item.Quality < 0 ) throw new System.Exception($"{item.Name} Quality cannot be negative");
        switch (itemType)
        {
            case GildedRose.ItemTypes.Sulfuras:
                if (item.Quality != 80) throw new System.Exception($"{item.Name} Sulfuras quality must be 80");
                break;
            default:
                if (item.Quality > 50) throw new System.Exception($"{item.Name} Quality cannot be more than 50");
                break;
        }
        return itemType;
    }
}