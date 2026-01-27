import { Component, ElementRef, inject, signal, viewChild } from "@angular/core";
import { GifListComponent } from "@gifs/components/gif-list/gif-list.component";
import { GifService } from "../../services/gifs.service";

@Component({
  selector: "app-trending-page",
  // imports: [GifListComponent],
  templateUrl: "./trending-page.component.html",
})
export default class TrendingPageComponent {
  gifService = inject(GifService);

  scrollDivRef = viewChild<ElementRef<HTMLDivElement>>("groupDiv");
  onScroll(event: Event) {
    const scrollDiv = this.scrollDivRef()?.nativeElement;
    if (!scrollDiv) return;
    const scrollTop = scrollDiv.scrollTop;
    const clientHeignt = scrollDiv.clientHeight;
    const scrollHeight = scrollDiv.scrollHeight;
    // console.log({ scrollTop: scrollHeight + scrollTop, clientHeignt });
    const isAtBottom = scrollTop + clientHeignt + 300 >= scrollHeight;

    if (isAtBottom) {
      this.gifService.loadTrendingGifs();
    }
  }
}
