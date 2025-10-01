import { Gif } from "@gifs/interface/gif.interface";
import { GiphyItem } from "@gifs/interface/giphy.interfaces";

export class GifMapper {
    static mapGiphyItemToGif(item: GiphyItem): Gif {
        return {
            id: item.id,
            url: item.images.original.url,
            title: item.title,
        };
    }

    static mapGiphyItemsToGifArray(items: GiphyItem[]): Gif[] {
        return items.map(this.mapGiphyItemToGif);
    }
}