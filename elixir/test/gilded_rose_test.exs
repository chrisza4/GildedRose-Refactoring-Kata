defmodule GildedRoseTest do
  use ExUnit.Case

  test "Decrease amount of item by 1 for normal item" do
    item = %Item{
      name: "Random item",
      sell_in: 11,
      quality: 1
    }

    expected = %Item{
      name: "Random item",
      sell_in: 10,
      quality: 0
    }

    assert expected == GildedRose.update_item(item)
  end

  test "Do nothing for Sulfurus" do
    item = %Item{
      name: "Sulfuras, Hand of Ragnaros",
      sell_in: 11,
      quality: 1
    }

    expected = %Item{
      name: "Sulfuras, Hand of Ragnaros",
      sell_in: 11,
      quality: 1
    }

    assert expected == GildedRose.update_item(item)
  end
end
