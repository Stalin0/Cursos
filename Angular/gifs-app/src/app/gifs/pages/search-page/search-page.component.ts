import { Component, inject, OnInit } from "@angular/core";
import { GifListComponent } from "@gifs/components/gif-list/gif-list.component";
import { GifService } from "@gifs/services/gifs.service";

@Component({
  selector: "app-search-page",
  templateUrl: "./search-page.component.html",
  styleUrls: [],
  imports: [GifListComponent],
})
export default class SearchPageComponent {
  gifService = inject(GifService);
  onSearch(query: string) {
    this.gifService.searchGifs(query);
    console.log({ query });
  }
}
