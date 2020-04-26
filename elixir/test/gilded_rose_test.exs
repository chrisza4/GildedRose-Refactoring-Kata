defmodule GildedRoseTest do
  use ExUnit.Case

  test "Decrease amount of item by 1 for normal item" do
    item = %Item{
      name: "Random item",
      days_remaining: 11,
      quality: 1
    }

    expected = %Item{
      name: "Random item",
      days_remaining: 10,
      quality: 0
    }

    assert expected == GildedRose.update_item(item)
  end

  test "Don't decrease amount of item by 1 for normal item if quality reach 0" do
    item = %Item{
      name: "Random item",
      days_remaining: 11,
      quality: 0
    }

    expected = %Item{
      name: "Random item",
      days_remaining: 10,
      quality: 0
    }

    assert expected == GildedRose.update_item(item)
  end

  test "Decrease amount by 2 if days_remaing < 0" do
    item = %Item{
      name: "Random item",
      days_remaining: -1,
      quality: 3
    }

    expected = %Item{
      name: "Random item",
      days_remaining: -2,
      quality: 1
    }

    assert expected == GildedRose.update_item(item)
  end

  test "Do nothing for Sulfurus" do
    item = %Item{
      name: "Sulfuras, Hand of Ragnaros",
      days_remaining: 11,
      quality: 1
    }

    expected = %Item{
      name: "Sulfuras, Hand of Ragnaros",
      days_remaining: 11,
      quality: 1
    }

    assert expected == GildedRose.update_item(item)
  end

  test "Aged brie increase quality if original quality less than 50" do
    item = %Item{
      name: "Aged Brie",
      days_remaining: 11,
      quality: 1
    }

    updated = GildedRose.update_item(item)

    assert updated.quality == 2
  end

  test "Aged brie decreased days_remaining" do
    item = %Item{
      name: "Aged Brie",
      days_remaining: 11,
      quality: 1
    }

    updated = GildedRose.update_item(item)

    assert updated.days_remaining == 10
  end

  test "Backstage quality when days_remaing >= 11" do
    item = %Item{
      name: "Backstage passes to a TAFKAL80ETC concert",
      days_remaining: 11,
      quality: 1
    }

    updated = GildedRose.update_item(item)

    assert updated.days_remaining == 10
    assert updated.quality == 2
  end

  test "Backstage quality when days_remaing < 11" do
    item = %Item{
      name: "Backstage passes to a TAFKAL80ETC concert",
      days_remaining: 10,
      quality: 1
    }

    updated = GildedRose.update_item(item)

    assert updated.days_remaining == 9
    assert updated.quality == 3
  end

  test "Backstage quality when days_remaing < 11 and quality is 50" do
    item = %Item{
      name: "Backstage passes to a TAFKAL80ETC concert",
      days_remaining: 10,
      quality: 50
    }

    updated = GildedRose.update_item(item)

    assert updated.days_remaining == 9
    assert updated.quality == 50
  end

  test "Backstage quality when days_remaing < 6 and quality is 50" do
    item = %Item{
      name: "Backstage passes to a TAFKAL80ETC concert",
      days_remaining: 5,
      quality: 50
    }

    updated = GildedRose.update_item(item)

    assert updated.days_remaining == 4
    assert updated.quality == 50
  end

  test "Backstage quality when days_remaing < 6 and quality is 49" do
    item = %Item{
      name: "Backstage passes to a TAFKAL80ETC concert",
      days_remaining: 5,
      quality: 49
    }

    updated = GildedRose.update_item(item)

    assert updated.days_remaining == 4
    assert updated.quality == 50
  end

  test "Backstage quality when days_remaing < 6 and quality is 48" do
    item = %Item{
      name: "Backstage passes to a TAFKAL80ETC concert",
      days_remaining: 5,
      quality: 49
    }

    updated = GildedRose.update_item(item)

    assert updated.days_remaining == 4
    assert updated.quality == 50
  end

  test "Backstage quality when days_remaing < 6 and quality is 47" do
    item = %Item{
      name: "Backstage passes to a TAFKAL80ETC concert",
      days_remaining: 5,
      quality: 47
    }

    updated = GildedRose.update_item(item)

    assert updated.days_remaining == 4
    assert updated.quality == 50
  end

  test "Backstage quality when days_remaing < 6 and quality is 46" do
    item = %Item{
      name: "Backstage passes to a TAFKAL80ETC concert",
      days_remaining: 5,
      quality: 46
    }

    updated = GildedRose.update_item(item)

    assert updated.days_remaining == 4
    assert updated.quality == 49
  end
end
