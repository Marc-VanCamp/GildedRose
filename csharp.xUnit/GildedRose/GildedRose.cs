﻿using System;
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
            item.Validate();
        }
        foreach (var item in Items)
        {
            var itemType = item.GetItemType();
            // Decreate SellIn for all items except Sulfuras
            switch (itemType)
            {
                case ItemTypes.Sulfuras:
                    break;
                default:
                    item.SellIn--;
                    break;
            }
            int? qualityDecrease = null;

            switch (itemType)
            {
                case ItemTypes.Common:
                    qualityDecrease = -1;
                    break;
                case ItemTypes.Aged:
                    qualityDecrease = 1;
                    break;
                case ItemTypes.Sulfuras:
                    // Keeps quality (at 80)
                    break;
                case ItemTypes.Backstage:
                    if (item.SellIn >= 10)
                    {
                        qualityDecrease = 1;
                    }
                    else if (item.SellIn >= 5)
                    {
                        qualityDecrease = 2;
                    }
                    else if (item.SellIn >= 0)
                    {
                        qualityDecrease = 3;
                    }
                    else
                    {
                        item.Quality = 0;
                    }
                    break;
                case ItemTypes.Conjured:
                    qualityDecrease = -2;
                    break;
            }
            if (qualityDecrease.HasValue)
            {
                // Double speed after sell date
                if (item.SellIn < 0) qualityDecrease = qualityDecrease.Value * 2;
                item.Quality = Math.Min(50,Math.Max(0, item.Quality += qualityDecrease.Value));
            }
        }
    }
}

public static class ExtenstionsMethods
{
    public static ItemTypes GetItemType(this Item item)
    {
        return item.Name switch
        {
            var name when name.StartsWith("Aged") => GildedRose.ItemTypes.Aged,
            var name when name.StartsWith("Sulfuras") => GildedRose.ItemTypes.Sulfuras,
            var name when name.StartsWith("Backstage") => GildedRose.ItemTypes.Backstage,
            var name when name.StartsWith("Conjured") => GildedRose.ItemTypes.Conjured,
            _ => GildedRose.ItemTypes.Common
        };
    }
    public static void Validate(this Item item)
    {
        if (item.Quality < 0) throw new System.Exception($"{item.Name} Quality cannot be negative");  
        var itemType = item.GetItemType();  
        switch (itemType)
        {
            case GildedRose.ItemTypes.Sulfuras:
                if (item.Quality != 80) throw new System.Exception($"{item.Name} Sulfuras quality must be 80");
                break;
            default:
                if (item.Quality > 50) throw new System.Exception($"{item.Name} Quality cannot be more than 50");
                break;
        }
        return;
    }
}