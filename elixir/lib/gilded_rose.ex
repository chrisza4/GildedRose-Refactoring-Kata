defmodule GildedRose do
  # Example
  # update_quality([%Item{name: "Backstage passes to a TAFKAL80ETC concert", days_remaining: 9, quality: 1}])
  # => [%Item{name: "Backstage passes to a TAFKAL80ETC concert", days_remaining: 8, quality: 3}]

  def update_quality(items) do
    Enum.map(items, &update_item/1)
  end

  def update_item(item) do
    item
    |> update_item_step_1()
    |> decrease_days_remaining()
    |> update_item_step_2()
  end

  defp update_item_step_1(%Item{name: "Aged Brie"} = item) do
    cond do
      item.quality >= 50 ->
        item

      true ->
        %{item | quality: item.quality + 1}
    end
  end

  defp update_item_step_1(%Item{name: "Backstage passes to a TAFKAL80ETC concert"} = item) do
    cond do
      item.quality >= 50 ->
        item

      item.days_remaining < 6 ->
        item
        |> inc_quality_promotionally()
        |> inc_quality_promotionally()
        |> inc_quality_promotionally()

      item.days_remaining < 11 ->
        item
        |> inc_quality_promotionally()
        |> inc_quality_promotionally()

      true ->
        inc_quality_promotionally(item)
    end
  end

  defp update_item_step_1(%Item{name: "Sulfuras, Hand of Ragnaros"} = item) do
    item
  end

  defp update_item_step_1(item) do
    %{item | quality: max(0, item.quality - 1)}
  end

  defp decrease_days_remaining(%Item{name: "Sulfuras, Hand of Ragnaros"} = item), do: item
  defp decrease_days_remaining(item), do: %{item | days_remaining: item.days_remaining - 1}

  # Step 2
  defp update_item_step_2(%Item{days_remaining: days_remaining} = item)
       when days_remaining >= 0 do
    item
  end

  defp update_item_step_2(%Item{name: "Aged Brie"} = item) do
    inc_quality_promotionally(item)
  end

  defp update_item_step_2(%Item{name: "Backstage passes to a TAFKAL80ETC concert"} = item) do
    %{item | quality: 0}
  end

  defp update_item_step_2(%Item{name: "Sulfuras, Hand of Ragnaros"} = item) do
    item
  end

  defp update_item_step_2(item) do
    %{item | quality: max(0, item.quality - 1)}
  end

  defp inc_quality_promotionally(%Item{} = item) do
    if item.quality >= 50 do
      item
    else
      %{item | quality: item.quality + 1}
    end
  end
end
