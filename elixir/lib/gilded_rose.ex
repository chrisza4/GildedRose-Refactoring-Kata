defmodule GildedRose do
  # Example
  # update_quality([%Item{name: "Backstage passes to a TAFKAL80ETC concert", days_remaining: 9, quality: 1}])
  # => [%Item{name: "Backstage passes to a TAFKAL80ETC concert", days_remaining: 8, quality: 3}]

  def update_quality(items) do
    Enum.map(items, &update_item/1)
  end

  def update_item(item) do
    # Main
    item
    |> update_today_quality()
    |> decrease_days_remaining()
    |> update_quality_if_expired()
  end

  defp update_today_quality(%Item{name: "Aged Brie"} = item) do
    increase_quality(item)
  end

  defp update_today_quality(%Item{name: "Backstage passes to a TAFKAL80ETC concert"} = item) do
    cond do
      item.days_remaining < 6 ->
        item
        |> increase_quality()
        |> increase_quality()
        |> increase_quality()

      item.days_remaining < 11 ->
        item
        |> increase_quality()
        |> increase_quality()

      true ->
        increase_quality(item)
    end
  end

  defp update_today_quality(%Item{name: "Sulfuras, Hand of Ragnaros"} = item) do
    item
  end

  defp update_today_quality(item) do
    decrease_quality(item)
  end

  defp decrease_days_remaining(%Item{name: "Sulfuras, Hand of Ragnaros"} = item), do: item
  defp decrease_days_remaining(item), do: %{item | days_remaining: item.days_remaining - 1}

  # Step 2
  defp update_quality_if_expired(%Item{days_remaining: days_remaining} = item)
       when days_remaining >= 0 do
    # Not expired yet
    item
  end

  defp update_quality_if_expired(%Item{name: "Aged Brie"} = item) do
    increase_quality(item)
  end

  defp update_quality_if_expired(%Item{name: "Backstage passes to a TAFKAL80ETC concert"} = item) do
    %{item | quality: 0}
  end

  defp update_quality_if_expired(%Item{name: "Sulfuras, Hand of Ragnaros"} = item) do
    item
  end

  defp update_quality_if_expired(item) do
    decrease_quality(item)
  end

  defp increase_quality(%Item{} = item) do
    if item.quality >= 50 do
      item
    else
      %{item | quality: item.quality + 1}
    end
  end

  defp decrease_quality(item), do: %{item | quality: max(0, item.quality - 1)}
end
