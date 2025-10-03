import { Component, inject, OnInit, signal } from "@angular/core";
import { GifListComponent } from "@gifs/components/gif-list/gif-list.component";
import { Gif } from "@gifs/interface/gif.interface";
import { GifService } from "@gifs/services/gifs.service";

@Component({
  selector: "app-search-page",
  templateUrl: "./search-page.component.html",
  styleUrls: [],
  imports: [GifListComponent],
})
export default class SearchPageComponent {
  gifService = inject(GifService);
  gifs = signal<Gif[]>([]);

  onSearch(query: string) {
    this.gifService.searchGifs(query).subscribe((resp) => {
      this.gifs.set(resp);
    });
  }
}
