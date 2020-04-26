defmodule GildedRose do
  # Example
  # update_quality([%Item{name: "Backstage passes to a TAFKAL80ETC concert", days_remaining: 9, quality: 1}])
  # => [%Item{name: "Backstage passes to a TAFKAL80ETC concert", days_remaining: 8, quality: 3}]

  def update_quality(items) do
    Enum.map(items, &update_item/1)
  end

  def update_item(%Item{name: "Sulfuras, Hand of Ragnaros"} = item) do
    item
  end

  def update_item(item) do
    item = update_item_step_1(item)

    item = %{item | days_remaining: item.days_remaining - 1}

    cond do
      item.days_remaining < 0 ->
        cond do
          item.name != "Aged Brie" ->
            cond do
              item.name != "Backstage passes to a TAFKAL80ETC concert" ->
                cond do
                  item.quality > 0 ->
                    cond do
                      item.name != "Sulfuras, Hand of Ragnaros" ->
                        %{item | quality: item.quality - 1}

                      true ->
                        item
                    end

                  true ->
                    item
                end

              true ->
                %{item | quality: item.quality - item.quality}
            end

          true ->
            cond do
              item.quality < 50 ->
                %{item | quality: item.quality + 1}

              true ->
                item
            end
        end

      true ->
        item
    end
  end

  def update_item_step_1(%Item{name: "Aged Brie"} = item) do
    cond do
      item.quality >= 50 ->
        item

      true ->
        %{item | quality: item.quality + 1}
    end
  end

  def update_item_step_1(%Item{name: "Backstage passes to a TAFKAL80ETC concert"} = item) do
    cond do
      item.quality >= 50 ->
        item

      true ->
        item = %{item | quality: item.quality + 1}

        item =
          cond do
            item.days_remaining < 11 ->
              cond do
                item.quality < 50 ->
                  %{item | quality: item.quality + 1}

                true ->
                  item
              end

            true ->
              item
          end

        cond do
          item.days_remaining < 6 ->
            cond do
              item.quality < 50 ->
                %{item | quality: item.quality + 1}

              true ->
                item
            end

          true ->
            item
        end
    end
  end

  def update_item_step_1(item) do
    %{item | quality: max(0, item.quality - 1)}
  end

  # def update_item_step_2(%Item{name: name} = item)
  #     when name in ["Aged Brie", "Backstage passes to a TAFKAL80ETC concert"] do

  # end

  # def update_item_step_2(item) do
  # end

  defp increase_if_less_than_50(quality) do
    if quality >= 50 do
      quality
    else
      quality + 1
    end
  end
end
