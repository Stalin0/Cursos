import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { environment } from "@environments/environment";
import type { GiphyResponse } from "../interface/giphy.interfaces";
import { Gif } from "../interface/gif.interface";
import { GifMapper } from "@gifs/mapper/gif.mapper";

@Injectable({ providedIn: "root" })
export class GifService {
    private http = inject(HttpClient);
    trendingGifs = signal<Gif[]>([]);
    trendingGifsLoading = signal(true);
  constructor() {
    this.loadTrendingGifs();
  }

  
  loadTrendingGifs() {
    this.http.get<GiphyResponse>(`${environment.giphyUrl}/gifs/trending`, {
      params: {
        api_key: environment.giphyApiKey,
        limit: 20,
      },
    }).subscribe((resp) => {
      const gifs = GifMapper.mapGiphyItemsToGifArray(resp.data);
      this.trendingGifs.set(gifs);
      this.trendingGifsLoading.set(false);
      console.log(gifs);
        console.log(resp.data[0].id);
        console.log(resp.data[0].title);
        console.log(resp.data[0].images.original.url);
    })
  }

  searchGifs(query: string) {
    this.http.get<GiphyResponse>(`${environment.searchUrl}/gifs/search`, {
      params: {
        api_key: environment.giphyApiKey,
        q: query,
        limit: 20,
      },
    }).subscribe((resp) => {
      const gifs = GifMapper.mapGiphyItemsToGifArray(resp.data);
      console.log({search: gifs});

    });
  }
}
